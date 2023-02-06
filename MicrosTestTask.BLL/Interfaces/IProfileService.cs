using Microsoft.AspNetCore.Http;
using MicrosTestTask.BLL.Models;

namespace MicrosTestTask.BLL.Interfaces;

public interface IProfileService
{
	Task<ProfileModel> Get(string username);
	Task Create(string username);
	Task<bool> Update(string username, IFormFile image);
}
