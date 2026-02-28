using AutoMapper;
using InventoryManagementSystem.DAL.Models;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Managers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InventoryItemModel, InventoryItemDetailVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InventoryItemId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.InventoryItemTitle))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByEmail));

            CreateMap<InventoryFieldModel, FieldVM>();

            CreateMap<InventoryItemModel, InventoryItemViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InventoryItemId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.InventoryItemTitle))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByEmail));

            CreateMap<InventoryItemModel, InventoryEditViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InventoryItemId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.InventoryItemTitle))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByEmail));

            CreateMap<UsersModel, UserViewModel>();
        }
    }
}
