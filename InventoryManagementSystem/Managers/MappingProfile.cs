using AutoMapper;
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

            //CreateMap<InventoryItemModel, InventoryEditViewModel>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InventoryItemId))
            //    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.InventoryItemTitle))
            //    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByEmail));

            CreateMap<InventoryEditViewModel, InventoryItemModel>()
                .ForMember(dest => dest.InventoryItemId, opt => opt.MapFrom(src => src.BasicInfo.Id))
                .ForMember(dest => dest.InventoryItemTitle, opt => opt.MapFrom(src => src.BasicInfo.Title));

            CreateMap<UsersModel, UserViewModel>();

            CreateMap<InventoryItemViewModel, InventoryItemModel>()
                .ForMember(dest => dest.InventoryItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InventoryItemTitle, opt => opt.MapFrom(src => src.Title));

            CreateMap<FieldTypeModel, FieldTypeViewModel>();

            CreateMap<InventoryItemValueModel, RowValueViewModel>();
        }
    }
}
