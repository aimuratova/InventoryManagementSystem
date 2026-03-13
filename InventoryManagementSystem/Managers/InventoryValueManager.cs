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
        private readonly IInventoryCustomIdService _inventoryCustomIdService;

        private readonly CloudinaryUploaderService _cloudinaryUploaderService;

        public InventoryValueManager(IMapper mapper, IInventoryValueService inventoryValueService, 
            IInventoryService inventoryService, IInventoryFieldService inventoryFieldService, 
            IInventoryCustomIdService inventoryCustomIdService, CloudinaryUploaderService cloudinaryUploaderService)
        {
            _mapper = mapper;
            _inventoryValueService = inventoryValueService;
            _inventoryService = inventoryService;
            _inventoryFieldService = inventoryFieldService;
            _inventoryCustomIdService = inventoryCustomIdService;
            _cloudinaryUploaderService = cloudinaryUploaderService;
        }

        public async Task<ResultModel> AddValue(List<ValueViewModel>? values, string? userId, List<IFormFile>? files)
        {
            var result = new ResultModel();
            var errors = new List<string>();

            var insertModel = new InventoryItemValueModel();
            insertModel.CreatedAt = DateTime.Now;
            insertModel.CreatedBy = userId;

            try
            {                
                int fileId = 0;

                foreach (var row in values)
                {
                    if (row.FieldId == 0 || row.InventoryId == 0)
                    {
                        errors.Add("Unable to add new value: not all parameter were provided");
                    }

                    if ((row.TypeId != (int)DataTypeEnum.ImageUrl1 && row.TypeId != (int)DataTypeEnum.ImageUrl2 && row.TypeId != (int)DataTypeEnum.ImageUrl3) &&
                        (row.Value == null || string.IsNullOrEmpty(row.Value.ToString())))
                    {
                        errors.Add("Unable to add new value: value were not provided");
                    }

                    insertModel.InventoryId = row.InventoryId;
                    insertModel.RowNum = row.RowNum;

                    //check that we have necessary files
                    if (row.TypeId == (int)DataTypeEnum.ImageUrl1 || 
                        row.TypeId == (int)DataTypeEnum.ImageUrl2 || 
                        row.TypeId == (int)DataTypeEnum.ImageUrl3)
                    {
                        if (files == null || files.Count < fileId || files.Count == 0)
                        {
                            errors.Add("File not chosen");
                            throw new Exception("Not all parameters were specified");
                        }
                    }

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
                        case (int)DataTypeEnum.ImageUrl1:
                            insertModel.ImageUrl1 = await _cloudinaryUploaderService.UploadImage(files[fileId]);
                            fileId++;
                            break;
                        case (int)DataTypeEnum.ImageUrl2:
                            insertModel.ImageUrl2 = await _cloudinaryUploaderService.UploadImage(files[fileId]);
                            fileId++;
                            break;
                        case (int)DataTypeEnum.ImageUrl3:
                            insertModel.ImageUrl3 = await _cloudinaryUploaderService.UploadImage(files[fileId]);
                            fileId++;
                            break;
                    }
                }

                // check if there is custom id spec in fields
                var fields = await _inventoryFieldService.GetInventoryItemFieldsById(insertModel.InventoryId);
                if (fields.Any(x => x.TypeId == (int)DataTypeEnum.CustomId))
                {
                    // using generator service generate customid field
                    insertModel.CustomId = await _inventoryCustomIdService.GenerateId(insertModel.InventoryId);
                }

                if (errors.Count == 0)
                {
                    await _inventoryValueService.AddValues(insertModel);
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Error occured while creating new value";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
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

        public async Task<ResultModel> Delete(int valueId)
        {
            return await _inventoryValueService.Delete(valueId);
        }

        public async Task<ResultModel> Update(RowValueViewModel rowValue)
        {
            var updateModel = _mapper.Map<InventoryItemValueModel>(rowValue);
            var result = await _inventoryValueService.Update(updateModel);

            return result;
        }
    }
}
