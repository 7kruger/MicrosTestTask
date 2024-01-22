using Microsoft.EntityFrameworkCore;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;
using MicrosTestTask.Domain.Enums;

namespace MicrosTestTask.BLL.Services;

public class CategoryService : ICategoryService
{
	private readonly IRepository<Category> _categoryRepository;
	private readonly IOperationService _operationService;

	public CategoryService(IRepository<Category> categoryRepository, IOperationService operationService)
	{
		_categoryRepository = categoryRepository;
		_operationService = operationService;
	}

	public async Task<IEnumerable<CategoryModel>> GetCategories()
	{
		var operations = await _operationService.GetAll();
		var categories = (await _categoryRepository.GetAll().ToListAsync())
			.Select(x => new CategoryModel
			{
				Id = x.Id,
				Name = x.Name,
				CategoryType = x.CategoryType,
				Operations = operations.Where(i => i.CategoryId == x.Id)
			});
		return categories;
	}

    public async Task<bool> CreateAsync(CategoryModel model)
    {
		try
		{
			var category = new Category { Name = model.Name, CategoryType = model.CategoryType };
			await _categoryRepository.CreateAsync(category);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
    }

    public async Task<bool> UpdateAsync(CategoryModel model)
	{
		try
		{
			var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);

			category.Name = model.Name;
			category.CategoryType = model.CategoryType;

			await _categoryRepository.UpdateAsync(category);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public async Task<bool> DeleteAsync(int id)
	{
		try
		{
			var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

			category.Operations.ForEach(x => x.CategoryId = category.CategoryType == CategoryType.Income ? 1 : 2 );

			await _categoryRepository.DeleteAsync(category);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

    public async Task<bool> CategoryExists(string name)
    {
		try
		{
			var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Name == name);

			return category != null;
		}
		catch (Exception)
		{
			return false;
		}
    }
}
