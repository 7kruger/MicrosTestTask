using MicrosTestTask.DAL.Enums;

namespace MicrosTestTask.DAL.Entities;

public class Category
{
	public int Id { get; set; }
	public string Name { get; set; }
	public CategoryType CategoryType { get; set; }
}
