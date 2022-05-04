using AutoMapper;
using HardwareStore.Core.Dto;
using HardwareStore.Db.Models;

namespace HardwareStore.Core.AutoMapper
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<UserDto, User>();
			CreateMap<UserInfoDto, UserInfo>().ReverseMap();
			CreateMap<UserDto, RegisteredUser>();
		}
	}
}
