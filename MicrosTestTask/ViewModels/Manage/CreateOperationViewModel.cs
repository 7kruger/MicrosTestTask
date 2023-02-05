using Microsoft.AspNetCore.Mvc.Rendering;

namespace MicrosTestTask.ViewModels.Manage;

public class CreateOperationViewModel
{
	public IEnumerable<SelectListItem> IncomeCategories { get; set; }
	public IEnumerable<SelectListItem> ExpenseCategories { get; set; }
}
