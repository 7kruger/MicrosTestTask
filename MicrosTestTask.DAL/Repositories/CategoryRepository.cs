using Microsoft.EntityFrameworkCore;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;

namespace MicrosTestTask.DAL.Repositories;

public class CategoryRepository : IRepository<Category>
{
	private readonly ApplicationDbContext _db;

	public CategoryRepository(ApplicationDbContext db)
	{
		_db = db;
	}

	public IQueryable<Category> GetAll()
	{
		return _db.Categories;
	}

	public async Task CreateAsync(Category entity)
	{
		await _db.Categories.AddAsync(entity);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateAsync(Category entity)
	{
		_db.Categories.Update(entity);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateRangeAsync(IEnumerable<Category> entities)
	{
		_db.Categories.UpdateRange(entities);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Category entity)
	{		
		_db.Categories.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteRangeAsync(IEnumerable<Category> entities)
	{
		_db.Categories.RemoveRange(entities);
		await _db.SaveChangesAsync();
	}
}
