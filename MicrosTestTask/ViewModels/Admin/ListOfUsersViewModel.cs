using MicrosTestTask.Models.Shared;

namespace MicrosTestTask.ViewModels.Admin;

public class ListOfUsersViewModel
{
	public IEnumerable<UserViewModel> Users { get; set; }
	public Pagination Pagination { get; set; }
}
