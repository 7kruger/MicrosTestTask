using MicrosTestTask.BLL.Models;

namespace MicrosTestTask.BLL.Interfaces;

public interface ICategoryService
{
	Task<IEnumerable<CategoryModel>> GetCategories();
	Task<bool> CreateAsync(CategoryModel model);
	Task<bool> UpdateAsync(CategoryModel model);
	Task<bool> DeleteAsync(int id);
	Task<bool> CategoryExists(string name);
}
