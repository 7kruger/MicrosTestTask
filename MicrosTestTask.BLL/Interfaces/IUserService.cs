using MicrosTestTask.BLL.Models;

namespace MicrosTestTask.BLL.Interfaces;

public interface IUserService
{
	IEnumerable<UserModel> GetUsers();
	Task Unlock(IEnumerable<int> selectedUsers);
	Task Block(IEnumerable<int> selectedUsers);
	Task AddToAdmin(IEnumerable<int> selectedUsers);
	Task RemoveFromAdmin(IEnumerable<int> selectedUsers);
	Task Delete(IEnumerable<int> selectedUsers);
}
