using MicrosTestTask.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace MicrosTestTask.ViewModels.Admin;

public class CategoryViewModel
{
	public int Id { get; set; }
	[Required(ErrorMessage = "Не введено название категории")]
	public string Name { get; set; }
	public CategoryType CategoryType { get; set; }
}
