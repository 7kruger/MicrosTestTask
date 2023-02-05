using MicrosTestTask.BLL.Models;

namespace MicrosTestTask.BLL.Interfaces;

public interface IProfileService
{
	Task<ProfileModel> Get(string username);
}
