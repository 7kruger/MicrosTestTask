using Microsoft.AspNetCore.Mvc.Rendering;
using MicrosTestTask.ViewModels.Admin;

namespace MicrosTestTask.ViewModels.Manage;

public class StatisticsViewModel
{
	public int? Month { get; set; }
	public int? Year { get; set; }
	public IEnumerable<SelectListItem> Months { get; set; }
	public IEnumerable<SelectListItem> Years { get; set; }
	public IEnumerable<CategoryViewModel> IncomeCategories { get; set; }
    public IEnumerable<CategoryViewModel> ExpenseCategories { get; set; }
}
