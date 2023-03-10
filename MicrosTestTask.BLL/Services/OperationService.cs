using Microsoft.EntityFrameworkCore;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;

namespace MicrosTestTask.BLL.Services;

public class OperationService : IOperationService
{
    private readonly IRepository<Operation> _operationRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<User> _userRepository;

    public OperationService(IRepository<Operation> operationRepository,
                            IRepository<Category> categoryRepository,
                            IRepository<User> userRepository)
    {
        _operationRepository = operationRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public IEnumerable<OperationModel> GetAll(string username)
    {
        try
        {
            var operations = _operationRepository.GetAll().Where(x => x.User.Name == username)
                .OrderByDescending(c => c.Id)
                .AsEnumerable()
                .Select(x => new OperationModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Sum = x.Sum,
                    Comment = x.Comment,
                    CategoryId = x.Category.Id,
                    CategoryModel = new CategoryModel { Id = x.Category.Id, Name = x.Category.Name, CategoryType = x.Category.CategoryType }
                });

            return operations;
        }
        catch (Exception)
        {
            return null;
        }
    }

	public async Task<bool> Create(OperationModel model, string username)
	{
        try
        {
			var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.CategoryId);
			var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == username);

			var operation = new Operation
			{
				Date = model.Date,
				Sum = model.Sum,
				Comment = model.Comment,
				CategoryId = category.Id,
				Category = category,
				UserId = user.Id,
				User = user
			};

			await _operationRepository.CreateAsync(operation);

            return true;
		}
        catch (Exception)
        {
            return false;
        }		
	}

	public async Task Update(OperationModel model)
    {
        
    }

    public async Task Delete(int id)
    {

    }
}
