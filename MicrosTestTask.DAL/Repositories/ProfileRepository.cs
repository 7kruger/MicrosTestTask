using Microsoft.EntityFrameworkCore;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;

namespace MicrosTestTask.DAL.Repositories;

public class ProfileRepository : IRepository<Profile>
{
	private readonly ApplicationDbContext _db;

	public ProfileRepository(ApplicationDbContext db)
	{
		_db = db;
	}

	public IQueryable<Profile> GetAll()
	{
		return _db.Profiles.Include(p => p.User);
	}

	public async Task CreateAsync(Profile entity)
	{
		await _db.Profiles.AddAsync(entity);
		await _db.SaveChangesAsync();
	}

	public async Task UpdateAsync(Profile entity)
	{
		_db.Update(entity);
		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Profile entity)
	{
		throw new System.NotImplementedException();
	}

	public Task UpdateRangeAsync(IEnumerable<Profile> entities)
	{
		throw new NotImplementedException();
	}

	public Task DeleteRangeAsync(IEnumerable<Profile> entities)
	{
		throw new NotImplementedException();
	}
}
