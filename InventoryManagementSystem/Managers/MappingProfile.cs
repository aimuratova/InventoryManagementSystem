using AutoMapper;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Models;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Managers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InventoryItemModel, InventoryItemViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InventoryItemId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.InventoryItemTitle))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByEmail));

            CreateMap<InventoryFieldModel, FieldVM>().ReverseMap();

            CreateMap<InventoryItemModel, InventoryItemViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InventoryItemId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.InventoryItemTitle))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByEmail));

            CreateMap<InventoryEditViewModel, InventoryItemModel>()
                .ForMember(dest => dest.InventoryItemId, opt => opt.MapFrom(src => src.BasicInfo.Id))
                .ForMember(dest => dest.InventoryItemTitle, opt => opt.MapFrom(src => src.BasicInfo.Title));

            CreateMap<UsersModel, UserViewModel>();

            CreateMap<InventoryItemViewModel, InventoryItemModel>()
                .ForMember(dest => dest.InventoryItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InventoryItemTitle, opt => opt.MapFrom(src => src.Title));

            CreateMap<FieldTypeModel, FieldTypeViewModel>();

            CreateMap<InventoryItemValueModel, RowValueViewModel>()
                .ReverseMap();

            CreateMap<InventoryCustomIdValueModel, CustomIdSelectedTypeViewModel>()                
                .ForMember(dest => dest.PatternValue, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.CustomTypeId, opt => opt.MapFrom(src => src.CustomIdType));

            CreateMap<CustomIdSelectedTypeViewModel, InventoryCustomIdValueModel>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.PatternValue))
                .ForMember(dest => dest.CustomIdType, opt => opt.MapFrom(src => src.CustomTypeId))
                .ForMember(dest => dest.InventoryId, opt => opt.Ignore());

            CreateMap<InventoryCustomTypeModel, CustomIdTypeViewModel>();

            CreateMap<UsersModel, UserInfoViewModel>()
                .ForMember(dest => dest.Inventories, opt => opt.Ignore())
                .ForMember(dest => dest.SelectedInventories, opt => opt.Ignore());

            CreateMap<UsersModel, UserViewModel>()
                .ForMember(dest => dest.Inventories, opt => opt.Ignore());

            CreateMap<SupportTicketViewModel, SupportTicketModel>();
        }
    }
}
