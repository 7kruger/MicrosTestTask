using Microsoft.AspNetCore.Mvc.Rendering;
using MicrosTestTask.DAL.Enums;

namespace MicrosTestTask.ViewModels.Manage;

public class HistoryViewModel
{
    public IEnumerable<OperationViewModel> OperationViewModels { get; set; }
	public IEnumerable<SelectListItem> IncomeCategories { get; set; }
	public IEnumerable<SelectListItem> ExpenseCategories { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public CategoryType? CategoryType { get; set; }
}
