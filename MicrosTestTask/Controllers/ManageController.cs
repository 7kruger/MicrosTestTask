using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.ViewModels.Admin;
using MicrosTestTask.ViewModels.Manage;
using Microsoft.AspNetCore.Mvc.Rendering;
using MicrosTestTask.BLL.Models;

namespace MicrosTestTask.Controllers;

[Authorize]
public class ManageController : Controller
{
	private readonly IOperationService _operationService;
	private readonly ICategoryService _categoryService;

    public ManageController(IOperationService operationService, ICategoryService categoryService)
    {
        _operationService = operationService;
		_categoryService = categoryService;
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
	public async Task<IActionResult> History(int? categoryFilterApplied)
	{
		var categories = _categoryService.GetCategories();
		var historyViewModel = new HistoryViewModel();

		historyViewModel.IncomeCategories = categories.Where(x => x.CategoryType == DAL.Enums.CategoryType.Income)
			.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

		historyViewModel.ExpenseCategories = categories.Where(x => x.CategoryType == DAL.Enums.CategoryType.Expense)
			.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

		historyViewModel.OperationViewModels = _operationService.GetAll(GetCurrentUsername)
			.Select(x => new OperationViewModel
			{
				Id = x.Id,
				Sum = x.Sum,
				Comment = x.Comment,
				Date = x.Date,
				CategoryViewModel = new CategoryViewModel { Id = x.CategoryModel.Id, Name = x.CategoryModel.Name, CategoryType = x.CategoryModel.CategoryType },
				CategoryId = x.CategoryId
			});

		// TODO filters

		historyViewModel.CategoryFilterApplied = categoryFilterApplied ?? 1;		

		return View(historyViewModel);
	}

	[HttpGet]
	public async Task<IActionResult> Statistics()
	{
		return View();
	}

	private string GetCurrentUsername => User.Identity.Name;
}
