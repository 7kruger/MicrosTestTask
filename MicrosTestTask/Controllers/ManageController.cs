using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.DAL.Enums;
using MicrosTestTask.Services.Interfaces;
using MicrosTestTask.ViewModels.Admin;
using MicrosTestTask.ViewModels.Manage;

namespace MicrosTestTask.Controllers;

[Authorize]
public class ManageController : Controller
{
	private readonly IOperationService _operationService;
	private readonly ICategoryService _categoryService;
	private readonly IManageService _manageService;

	public ManageController(IOperationService operationService,
							ICategoryService categoryService,
							IManageService manageService)
	{
		_operationService = operationService;
		_categoryService = categoryService;
		_manageService = manageService;
	}

	[HttpGet]
	public IActionResult Create()
	{
		var categories = _categoryService.GetCategories();
		var createOperationViewModel = new CreateOperationViewModel();

		createOperationViewModel.IncomeCategories = categories.Where(x => x.CategoryType == DAL.Enums.CategoryType.Income)
			.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

		createOperationViewModel.ExpenseCategories = categories.Where(x => x.CategoryType == DAL.Enums.CategoryType.Expense)
			.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

		return View(createOperationViewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Create(OperationViewModel model)
	{
		var created = await _operationService.Create(new OperationModel
		{
			Sum = model.Sum,
			Comment = model.Comment,
			Date = model.Date,
			CategoryId = model.CategoryId
		}, GetCurrentUsername);

		if (created)
		{
			return Ok();
		}

		return BadRequest("Ошибка при создании операции");
	}

    [HttpGet]
	public IActionResult History(DateTime? startDate, DateTime? endDate, CategoryType? categoryType)
	{
		var historyViewModel = _manageService.GetHistoryViewModel(GetCurrentUsername, startDate, endDate, categoryType);

		return View(historyViewModel);
	}

	[HttpGet]
	public IActionResult Statistics(int? month)
	{
		var statisticsViewModel = _manageService.GetStatisticsViewModel(month);

        return View(statisticsViewModel);
	}

	private string GetCurrentUsername => User.Identity.Name;
}
