using Microsoft.AspNetCore.Mvc.Rendering;
using MicrosTestTask.Models.Shared;

namespace MicrosTestTask.ViewModels.Admin;

public class ListOfCategoriesViewModel
{
	public IEnumerable<CategoryViewModel> Categories { get; set; }
    public Pagination Pagination { get; set; }
}
