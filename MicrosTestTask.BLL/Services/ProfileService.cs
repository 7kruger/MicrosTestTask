using Microsoft.EntityFrameworkCore;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Enums;
using MicrosTestTask.DAL.Interfaces;

namespace MicrosTestTask.BLL.Services;

public class ProfileService : IProfileService
{
	private readonly IRepository<Operation> _operationRepository;

	public ProfileService(IRepository<Operation> operationRepository)
	{
		_operationRepository = operationRepository;
	}

	public async Task<ProfileModel> Get(string username)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(username))
			{
				return null;
			}

			var operations = await _operationRepository.GetAll()
				.Where(x => x.User.Name == username)
				.ToListAsync();

			var allTimeIncome = operations.Where(x => x.Category.CategoryType == CategoryType.Income).Sum(x => x.Sum);
			var allTimeExpense = operations.Where(x => x.Category.CategoryType == CategoryType.Expense).Sum(x => x.Sum);

			return new ProfileModel
			{
				Username = username,
				AllTimeIncome = allTimeIncome,
				AllTimeExpense = allTimeExpense,
				Difference = allTimeIncome - allTimeExpense
			};
		}
		catch (Exception)
		{
			return null;
		}
	}
}
