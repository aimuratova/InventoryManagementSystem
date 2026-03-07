using AutoMapper;
using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.BLL.Services;
using InventoryManagementSystem.DAL.Models;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Managers
{
    public class InventoryValueManager
    {
        private readonly IMapper _mapper;
        private readonly IInventoryValueService _inventoryValueService;
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryFieldService _inventoryFieldService;

        public InventoryValueManager(IMapper mapper, IInventoryValueService inventoryValueService, IInventoryService inventoryService, IInventoryFieldService inventoryFieldService)
        {
            _mapper = mapper;
            _inventoryValueService = inventoryValueService;
            _inventoryService = inventoryService;
            _inventoryFieldService = inventoryFieldService;
        }

        public async Task<ResultModel> AddValue(List<ValueViewModel> values, string userId)
        {
            var result = new ResultModel();
            var errors = new List<string>();

            var insertModel = new InventoryItemValueModel();
            insertModel.CreatedAt = DateTime.Now;
            insertModel.CreatedBy = userId;

            foreach (var row in values)
            {
                if (row.FieldId == 0 || row.InventoryId == 0)
                {
                    errors.Add("Unable to add new value: not all parameter were provided");
                }

                if (row.Value == null || string.IsNullOrEmpty(row.Value.ToString()))
                {
                    errors.Add("Unable to add new value: value were not provided");
                }

                insertModel.InventoryId = row.InventoryId;
                insertModel.RowNum = row.RowNum;

                switch (row.TypeId)
                {
                    case (int)DataTypeEnum.Singleline1:
                        insertModel.Singleline1 = row.Value.ToString(); break;
                    case (int)DataTypeEnum.Singleline2:
                        insertModel.Singleline2 = row.Value.ToString(); break;
                    case (int)DataTypeEnum.Singleline3:
                        insertModel.Singleline3 = row.Value.ToString(); break;
                    case (int)DataTypeEnum.Multiline1:
                        insertModel.Multiline1 = row.Value.ToString(); break;
                    case (int)DataTypeEnum.Multiline2:
                        insertModel.Multiline2 = row.Value.ToString(); break;
                    case (int)DataTypeEnum.Multiline3:
                        insertModel.Multiline3 = row.Value.ToString(); break;
                    case (int)DataTypeEnum.Num1:
                        insertModel.Num1 = Convert.ToInt32(row.Value.ToString()); break;
                    case (int)DataTypeEnum.Num2:
                        insertModel.Num2 = Convert.ToInt32(row.Value.ToString()); break;
                    case (int)DataTypeEnum.Num3:
                        insertModel.Num3 = Convert.ToInt32(row.Value.ToString()); break;
                    case (int)DataTypeEnum.Check1:
                        insertModel.Check1 = Convert.ToBoolean(row.Value.ToString()); break;
                    case (int)DataTypeEnum.Check2:
                        insertModel.Check2 = Convert.ToBoolean(row.Value.ToString()); break;
                    case (int)DataTypeEnum.Check3:
                        insertModel.Check3 = Convert.ToBoolean(row.Value.ToString()); break;
                    case (int)DataTypeEnum.Datetime1:
                        insertModel.Datetime1 = Convert.ToDateTime(row.Value.ToString()); break;
                    case (int)DataTypeEnum.Datetime2:
                        insertModel.Datetime2 = Convert.ToDateTime(row.Value.ToString()); break;
                    case (int)DataTypeEnum.Datetime3:
                        insertModel.Datetime3 = Convert.ToDateTime(row.Value.ToString()); break;

                }
            }

            if (errors.Count == 0)
            {
                try
                {
                    await _inventoryValueService.AddValues(insertModel);

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


        public async Task<InventoryValueViewModel> GetInventoryValueInfo(int valueId, int inventoryId)
        {
            var result = new InventoryValueViewModel();
            var inventoryItem = await _inventoryService.GetInventoryItemById(inventoryId);
            var inventoryFields = (await _inventoryFieldService.GetInventoryItemFieldsById(inventoryId)).OrderBy(x => x.OrderNum);
            var inventoryValue = await _inventoryValueService.GetInventoryValueById(valueId);

            result.BasicInfo = _mapper.Map<InventoryItemViewModel>(inventoryItem);
            result.Fields = _mapper.Map<List<FieldVM>>(inventoryFields);
            result.MainInfo = _mapper.Map<RowValueViewModel>(inventoryValue);

            return result;
        }
    }
}
