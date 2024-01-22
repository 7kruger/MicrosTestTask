using MicrosTestTask.BLL.Models;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.ViewModels.Admin;
using MicrosTestTask.ViewModels.Manage;

namespace MicrosTestTask;

public class AppMappingProfile : AutoMapper.Profile
{
	public AppMappingProfile()
	{
		CreateMap<CategoryModel, Category>().ReverseMap();
		CreateMap<CategoryModel, CategoryViewModel>().ReverseMap();

		CreateMap<OperationModel, Operation>().ReverseMap();
		CreateMap<OperationModel, OperationViewModel>().ReverseMap();

		CreateMap<UserModel, User>().ReverseMap();
		CreateMap<UserModel, UserViewModel>().ReverseMap();
	}
}
