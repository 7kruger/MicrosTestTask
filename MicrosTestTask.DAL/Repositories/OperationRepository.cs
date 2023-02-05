using Microsoft.EntityFrameworkCore;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;

namespace MicrosTestTask.DAL.Repositories;

public class OperationRepository : IRepository<Operation>
{
	private readonly ApplicationDbContext _db;

	public OperationRepository(ApplicationDbContext db)
	{
		_db = db;
	}

	public IQueryable<Operation> GetAll()
	{
		return _db.Operations.Include(x => x.Category)
							 .Include(x => x.User);
	}

	public async Task CreateAsync(Operation entity)
	{
		await _db.Operations.AddAsync(entity);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateAsync(Operation entity)
	{
		_db.Update(entity);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateRangeAsync(IEnumerable<Operation> entities)
	{
		_db.Operations.UpdateRange(entities);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Operation entity)
	{
		_db.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteRangeAsync(IEnumerable<Operation> entities)
	{
		_db.Operations.RemoveRange(entities);
		await _db.SaveChangesAsync();
	}
}
