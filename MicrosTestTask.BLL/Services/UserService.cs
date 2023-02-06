using Microsoft.EntityFrameworkCore;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Enums;
using MicrosTestTask.DAL.Interfaces;

namespace MicrosTestTask.BLL.Services;

public class UserService : IUserService
{
	private readonly IRepository<User> _userRepository;

	public UserService(IRepository<User> userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<IEnumerable<UserModel>> GetUsers()
	{
		try
		{
			var users = await _userRepository.GetAll()
				.Select(x => new UserModel
				{
					Id = x.Id,
					Name = x.Name,
					Password = x.Password,
					IsBlocked = x.IsBlocked,
					Role = x.Role,
					RegistrationDate = x.RegistrationDate
				}).ToListAsync();

			return users;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public async Task Block(IEnumerable<int> selectedUsers)
	{
		var users = await _userRepository.GetAll().Where(x => selectedUsers.Contains(x.Id)).ToListAsync();
		users.ForEach(x => x.IsBlocked = true);
		await _userRepository.UpdateRangeAsync(users);
	}

	public async Task Unlock(IEnumerable<int> selectedUsers)
	{
		var users = await _userRepository.GetAll().Where(x => selectedUsers.Contains(x.Id)).ToListAsync();
		users.ForEach(x => x.IsBlocked = false);
		await _userRepository.UpdateRangeAsync(users);
	}

	public async Task AddToAdmin(IEnumerable<int> selectedUsers)
	{
		var users = await _userRepository.GetAll().Where(x => selectedUsers.Contains(x.Id)).ToListAsync();
		users.ForEach(x => x.Role = Role.Admin);
		await _userRepository.UpdateRangeAsync(users);
	}

	public async Task RemoveFromAdmin(IEnumerable<int> selectedUsers)
	{
		var users = await _userRepository.GetAll().Where(x => selectedUsers.Contains(x.Id)).ToListAsync();
		users.ForEach(x => x.Role = Role.User);
		await _userRepository.UpdateRangeAsync(users);
	}

	public async Task Delete(IEnumerable<int> selectedUsers)
	{
		var users = _userRepository.GetAll().Where(x => selectedUsers.Contains(x.Id)).AsEnumerable();
		await _userRepository.DeleteRangeAsync(users);
	}
}
