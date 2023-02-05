using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.ViewModels.Profile;

namespace MicrosTestTask.Controllers;

[Authorize]
public class ProfileController : Controller
{
	private readonly IProfileService _profileService;

	public ProfileController(IProfileService profileService)
	{
		_profileService = profileService;
	}

	[HttpGet]
	[Route("[controller]/")]
	public async Task<IActionResult> Profile()
	{
		var model = await _profileService.Get(GetCurrentUsername);

		if (model == null)
		{
			return View("Error", "Не удалось загрузить профиль");
		}

		var profile = new ProfileViewModel
		{
			Username = model.Username,
			AllTimeIncome = model.AllTimeIncome,
			AllTimeExpense = model.AllTimeExpense,
			Difference = model.Difference,
		};

		return View(profile);
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> ShowProfile(string username)
	{
		var model = await _profileService.Get(username);

		if (model != null)
		{
			var profile = new ProfileViewModel
			{
				Username = model.Username,
				AllTimeIncome = model.AllTimeIncome,
				AllTimeExpense = model.AllTimeExpense,
				Difference = model.Difference
			};

			return Ok(profile);
		}

		return NotFound();
	}

	private string GetCurrentUsername => User.Identity?.Name;
}
