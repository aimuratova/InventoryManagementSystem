
using AutoMapper;
using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Models;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Configuration;
using System.Data;

namespace InventoryManagementSystem.Managers
{
    public class InventoryManager
    {
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryFieldService _inventoryFieldService;
        private readonly IInventoryValueService _inventoryValueService;
        private readonly IUserService _userService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IInventoryUserService _inventoryUserService;

        private readonly IMapper _mapper;

        public InventoryManager(IInventoryService inventoryService, IInventoryFieldService inventoryFieldService, 
            IInventoryValueService valueService, IMapper mapper, 
            IUserService userService, IDictionaryService dictionaryService, IInventoryUserService inventoryUserService)
        {
            _inventoryService = inventoryService;
            _inventoryFieldService = inventoryFieldService;
            _inventoryValueService = valueService;
            _mapper = mapper;
            _userService = userService;
            _dictionaryService = dictionaryService;
            _inventoryUserService = inventoryUserService;
        }

        public async Task<List<InventoryItemViewModel>> GetAllItemsAsync(string? userId = null, 
            int? categoryId = null, string? searchText = null, string? inventoryType = null)
        {
            var items = new List<InventoryItemViewModel>();

            var inventoryItems = await _inventoryService.GetInventoryItems(userId, categoryId, searchText, inventoryType);

            //var inventoryIds = inventoryItems.Data?.Select(x => x.InventoryItemId).ToList();
            //var dataSet = await _inventoryService.GetInventoryValues(inventoryIds);

            if (inventoryItems == null || inventoryItems.Data == null)
            {
                return items;
            }

            items = _mapper.Map<List<InventoryItemViewModel>>(inventoryItems.Data);

            int i = 1;
            items.ForEach(x =>
            {
                x.Number = i++;
            });

            //if(dataSet == null || dataSet.Data == null)
            //{
            //    return items;
            //}

            //foreach (DataTable dataTable in dataSet.Data.Tables)
            //{
            //    if (dataTable.Rows.Count > 0)
            //    {
            //        var itemId = dataTable.Rows[0]["ItemId"].ToString();
            //        var item = items.FirstOrDefault(i => i.Id.ToString() == itemId);
            //        if (item != null)
            //        {
            //            item.ValuesDT = dataTable;
            //        }
            //    }
            //}

            return items;
        }

        public async Task<InventoryItemDetailVM> GetInventoryInfoByIdAsync(int id)
        {
            var inventoryItem = await _inventoryService.GetInventoryItemById(id);
            var inventoryFields = (await _inventoryFieldService.GetInventoryItemFieldsById(id)).OrderBy(x=>x.OrderNum);
            var inventoryValues = await _inventoryValueService.GetInventoryValueDTById(id);

            var result = new InventoryItemDetailVM();
            result = _mapper.Map<InventoryItemDetailVM>(inventoryItem);
            result.Fields = _mapper.Map<List<FieldVM>>(inventoryFields);
            result.ValuesDT = inventoryValues;

            return result;
        }

        public async Task<InventoryEditViewModel> GetInventoryInfoByIdForEdit(int id)
        {
            var inventoryItem = await _inventoryService.GetInventoryItemById(id);
            var inventoryFields = (await _inventoryFieldService.GetInventoryItemFieldsById(id)).OrderBy(x => x.OrderNum);
            var inventoryValues = await _inventoryValueService.GetInventoryValueDTById(id);
            
            //selected user ids
            var inventoryUsers = await _inventoryUserService.GetInventoryUsersByInventoryId(id);

            // dictionaries
            var allUsers = (await _userService.ListUsers()).Data;
            var categories = (await _dictionaryService.GetCategoriesAsync());
            var fieldTypes = (await _dictionaryService.GetFieldTypesAsync());

            var result = new InventoryEditViewModel();
            result = _mapper.Map<InventoryEditViewModel>(inventoryItem);
            result.Fields = _mapper.Map<List<FieldVM>>(inventoryFields);
            result.ValuesDT = inventoryValues;

            result.SelectedUserIds = new List<string>();
            result.SelectedUserIds.AddRange(inventoryUsers);

            result.RegisteredUsers = _mapper.Map<List<UserViewModel>>(allUsers);
            result.Categories = categories;
            result.FieldTypeOptions = _mapper.Map<List<FieldTypeViewModel>>(fieldTypes);

            return result;
        }

        public async Task<ResultModel> Save(InventoryEditViewModel inventory)
        {
            var inventoryModel = _mapper.Map<InventoryItemModel>(inventory);
            var fieldListModel = _mapper.Map<List<InventoryFieldModel>>(inventory.Fields);

            var result = await _inventoryService.UpdateInventory(inventoryModel, inventory.SelectedUserIds, fieldListModel);

            return result;
        }

        public async Task<ResultModel> Delete(int inventoryId)
        {
            return await _inventoryService.DeleteInventory(inventoryId);
        }

        public async Task<ResultModel<int>> Add(InventoryItemViewModel inventory)
        {
            var inventoryModel = _mapper.Map<InventoryItemModel>(inventory);
            return await _inventoryService.AddInventory(inventoryModel);
        }

        public async Task<ResultModel> AddValue(List<ValueViewModel> values)
        {
            var result = new ResultModel();
            var errors = new List<string>();

            foreach (var row in values)
            {
                if(row.FieldId == 0 || row.InventoryId == 0)
                {
                    errors.Add("Unable to add new value: not all parameter were provided");
                }

                if(row.Value == null || string.IsNullOrEmpty(row.Value.ToString()))
                {
                    errors.Add("Unable to add new value: value were not provided");
                }
            }

            if (errors.Count == 0)
            {
                var fieldsModel = _mapper.Map<List<InventoryItemValueModel>>(values);

                try
                {
                    await _inventoryValueService.AddValues(fieldsModel);

                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }
            }
            
            result.Errors = errors;
            return result;
        }
    }
}
