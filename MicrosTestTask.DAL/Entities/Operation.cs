namespace MicrosTestTask.DAL.Entities;

public class Operation
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public double Sum { get; set; }
    public string? Comment { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
