using MicrosTestTask.ViewModels.Admin;

namespace MicrosTestTask.ViewModels.Manage;

public class OperationViewModel
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public double Sum { get; set; }
    public string? Comment { get; set; }
    public int CategoryId { get; set; }
    public CategoryViewModel CategoryViewModel { get; set; }
}
