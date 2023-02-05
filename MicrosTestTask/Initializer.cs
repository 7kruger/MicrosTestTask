using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Services;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;
using MicrosTestTask.DAL.Repositories;
using MicrosTestTask.Services.Implementations;
using MicrosTestTask.Services.Interfaces;

namespace MicrosTestTask;

public static class Initializer
{
	public static void InitializeRepositories(this IServiceCollection services)
	{
		services.AddScoped<IRepository<User>, UserRepository>();
		services.AddScoped<IRepository<Operation>, OperationRepository>();
		services.AddScoped<IRepository<Category>, CategoryRepository>();
	}

	public static void InitializeServices(this IServiceCollection services)
	{
		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<IProfileService, ProfileService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<ICategoryService, CategoryService>();
		services.AddScoped<IOperationService, OperationService>();
		services.AddScoped<IManageService, ManageService>();
	}
}
