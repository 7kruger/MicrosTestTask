using Microsoft.AspNetCore.Mvc.Rendering;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.DAL.Enums;
using MicrosTestTask.Services.Interfaces;
using MicrosTestTask.ViewModels.Admin;
using MicrosTestTask.ViewModels.Manage;

namespace MicrosTestTask.Services.Implementations;

public class ManageService : IManageService
{
	private readonly ICategoryService _categoryService;
	private readonly IOperationService _operationService;

	public ManageService(ICategoryService categoryService, IOperationService operationService)
	{
		_categoryService = categoryService;
		_operationService = operationService;
	}

	public HistoryViewModel GetHistoryViewModel(string username, DateTime? startDate, DateTime? endDate, CategoryType? categoryType)
	{
		var categories = _categoryService.GetCategories();
		var historyViewModel = new HistoryViewModel();

		historyViewModel.IncomeCategories = categories.Where(x => x.CategoryType == DAL.Enums.CategoryType.Income)
			.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

		historyViewModel.ExpenseCategories = categories.Where(x => x.CategoryType == DAL.Enums.CategoryType.Expense)
			.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

		var operations = _operationService.GetAll(username);

		if (categoryType != null)
		{
			operations = operations.Where(x => x.CategoryModel.CategoryType == categoryType);
		}

		if (startDate != null)
		{
			operations = operations.Where(x => x.Date >= startDate);
		}
		if (endDate != null)
		{
			operations = operations.Where(x => x.Date <= endDate);
		}

		historyViewModel.OperationViewModels = operations.Select(x => new OperationViewModel
		{
			Id = x.Id,
			Sum = x.Sum,
			Comment = x.Comment,
			Date = x.Date,
			CategoryViewModel = new CategoryViewModel { Id = x.CategoryModel.Id, Name = x.CategoryModel.Name, CategoryType = x.CategoryModel.CategoryType },
			CategoryId = x.CategoryId
		});

		historyViewModel.StartDate = startDate ?? null;
		historyViewModel.EndDate = endDate ?? null;

		return historyViewModel;
	}
}
