using AutoMapper;
using HardwareStore.Core.Dto;
using HardwareStore.Db.Models;
using System.IO;

namespace HardwareStore.Core.AutoMapper
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<UserDto, User>();
			CreateMap<UserInfoDto, UserInfo>().ReverseMap();
			CreateMap<UserDto, RegisteredUser>();
			CreateMap<CategoryDto, Category>().ReverseMap();
			CreateMap<SubcategoryDto, Subcategory>().ReverseMap();
			CreateMap<Category, CategoryFullDto>();
			CreateMap<Subcategory, SubcategoryFullDto>().ReverseMap();
			CreateMap<Product, ProductFullDto>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
				.ForMember(dest => dest.SubcategoryId, opt => opt.MapFrom(src => src.Subcategory.Id));
		}
	}
}
