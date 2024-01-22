using Microsoft.AspNetCore.Mvc.Rendering;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.Domain.Enums;
using MicrosTestTask.Services.Interfaces;
using MicrosTestTask.ViewModels.Admin;
using MicrosTestTask.ViewModels.Manage;

namespace MicrosTestTask.Services.Implementations;

public class ManageService : IManageService
{
	private readonly ICategoryService _categoryService;
	private readonly IOperationService _operationService;

	public ManageService(ICategoryService categoryService, 
						 IOperationService operationService,
						 IUserService userService)
	{
		_categoryService = categoryService;
		_operationService = operationService;
	}

	public async Task<HistoryViewModel> GetHistoryViewModel(string username, DateTime? startDate, DateTime? endDate, CategoryType? categoryType)
	{
		var categories = await _categoryService.GetCategories();
		var historyViewModel = new HistoryViewModel();

		historyViewModel.IncomeCategories = categories.Where(x => x.CategoryType == CategoryType.Income)
			.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

		historyViewModel.ExpenseCategories = categories.Where(x => x.CategoryType == CategoryType.Expense)
			.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

		var operations = (await _operationService.GetAll()).Where(x => x.User.Name == username);

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

    public async Task<StatisticsViewModel> GetStatisticsViewModel(string username, int? month, int? year)
    {
		month = month ?? DateTime.Now.Month;
		year = year ?? DateTime.Now.Year;
		var categories = (await _categoryService.GetCategories()).ToList();
		categories.ForEach(x => x.Operations = x.Operations.Where(o => o.User.Name == username));

        var statisticsViewModel = new StatisticsViewModel();
        statisticsViewModel.IncomeCategories = categories.Where(x => x.CategoryType == CategoryType.Income)
            .Select(x =>
            {
				var operations = x.Operations.Where(o => o.Date.Month == month && o.Date.Year == year);
				return new CategoryViewModel
				{
					Id = x.Id,
					CategoryType = x.CategoryType,
					Name = x.Name,
					Operations = operations,
				};
			});
        statisticsViewModel.ExpenseCategories = categories.Where(x => x.CategoryType == CategoryType.Expense)
            .Select(x =>
            {
                var operations = x.Operations.Where(o => o.Date.Month == month && o.Date.Year == year);
                return new CategoryViewModel
                {
                    Id = x.Id,
                    CategoryType = x.CategoryType,
                    Name = x.Name,
                    Operations = operations,
                };
            });

        statisticsViewModel.Months = GetMonths();
        statisticsViewModel.Month = month ?? null;
		statisticsViewModel.Years = GetYears();
		statisticsViewModel.Year = year ?? null;

		return statisticsViewModel;
    }

	private IEnumerable<SelectListItem> GetMonths()
	{
		var months = new List<SelectListItem>
		{
            new SelectListItem { Value = "1", Text = "Январь", Selected = false },
            new SelectListItem { Value = "2", Text = "Февраль", Selected = false },
            new SelectListItem { Value = "3", Text = "Март", Selected = false },
            new SelectListItem { Value = "4", Text = "Апрель", Selected = false },
            new SelectListItem { Value = "5", Text = "Май", Selected = false },
            new SelectListItem { Value = "6", Text = "Июнь", Selected = false },
            new SelectListItem { Value = "7", Text = "Июль", Selected = false },
            new SelectListItem { Value = "8", Text = "Август", Selected = false },
            new SelectListItem { Value = "9", Text = "Сентябрь", Selected = false },
            new SelectListItem { Value = "10", Text = "Октябрь", Selected = false },
            new SelectListItem { Value = "11", Text = "Ноябрь", Selected = false },
            new SelectListItem { Value = "12", Text = "Декабрь", Selected = false },
        };

		return months;
	}

	private IEnumerable<SelectListItem> GetYears()
	{
		var years = new List<SelectListItem>();

		for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 5; i--)
		{
			years.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
		}

		return years;
	}
}
