using MicrosTestTask.Domain.Enums;

namespace MicrosTestTask.BLL.Models;

public class CategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }
    public IEnumerable<OperationModel> Operations { get; set; }
}
