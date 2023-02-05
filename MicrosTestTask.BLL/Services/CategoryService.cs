using Microsoft.EntityFrameworkCore;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;

namespace MicrosTestTask.BLL.Services;

public class CategoryService : ICategoryService
{
	private readonly IRepository<Category> _categoryRepository;

	public CategoryService(IRepository<Category> categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	public IEnumerable<CategoryModel> GetCategories()
	{
		var categories = _categoryRepository.GetAll()
			.AsEnumerable()
			.Select(x => new CategoryModel { Id = x.Id, Name = x.Name, CategoryType = x.CategoryType });
		return categories;
	}

    public async Task CreateAsync(CategoryModel model)
    {
        var category = new Category { Name = model.Name, CategoryType = model.CategoryType };

        await _categoryRepository.CreateAsync(category);
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
