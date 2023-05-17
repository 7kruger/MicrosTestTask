using MicrosTestTask.BLL.Models;
using MicrosTestTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MicrosTestTask.ViewModels.Admin;

public class CategoryViewModel
{
	public int Id { get; set; }
	[Required(ErrorMessage = "Не введено название категории")]
	public string Name { get; set; }
	public CategoryType CategoryType { get; set; }
	public IEnumerable<OperationModel>? Operations { get; set; }
}
