using MicrosTestTask.DAL.Enums;

namespace MicrosTestTask.DAL.Entities;

public class User
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
	public Role Role { get; set; }
	public bool IsBlocked { get; set; }
	public DateTime RegistrationDate { get; set; }
}
