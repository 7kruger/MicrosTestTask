using MicrosTestTask.BLL.Models;
using System.Security.Claims;

namespace MicrosTestTask.BLL.Interfaces;

public interface IAccountService
{
	Task<IdentityResult> Register(UserModel model);
	Task<IdentityResult> Authenticate(UserModel model);
	Task<bool> ChangePassword(string username, string newPassword);
}
