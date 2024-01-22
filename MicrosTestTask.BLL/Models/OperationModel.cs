using MicrosTestTask.DAL.Entities;

namespace MicrosTestTask.BLL.Models;

public class OperationModel
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public double Sum { get; set; }
    public string? Comment { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int CategoryId { get; set; }
    public CategoryModel CategoryModel { get; set; }
}
