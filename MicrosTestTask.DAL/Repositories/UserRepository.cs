using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;

namespace MicrosTestTask.DAL.Repositories;

public class UserRepository : IRepository<User>
{
	private readonly ApplicationDbContext _db;

	public UserRepository(ApplicationDbContext db)
	{
		_db = db;
	}

	public IQueryable<User> GetAll()
	{
		return _db.Users;
	}

	public async Task CreateAsync(User entity)
	{
		await _db.Users.AddAsync(entity);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateAsync(User entity)
	{
		_db.Users.Update(entity);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateRangeAsync(IEnumerable<User> entities)
	{
		_db.Users.UpdateRange(entities);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(User entity)
	{
		_db.Users.Remove(entity);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteRangeAsync(IEnumerable<User> entities)
	{
		_db.Users.RemoveRange(entities);
		await _db.SaveChangesAsync();
	}
}
