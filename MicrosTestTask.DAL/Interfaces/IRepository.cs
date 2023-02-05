namespace MicrosTestTask.DAL.Interfaces;

public interface IRepository<T>
{
	IQueryable<T> GetAll();
	Task CreateAsync(T entity);
	Task UpdateAsync(T entity);
	Task UpdateRangeAsync(IEnumerable<T> entities);
	Task DeleteAsync(T entity);
	Task DeleteRangeAsync(IEnumerable<T> entities);
}
