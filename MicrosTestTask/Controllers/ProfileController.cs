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
			ImgRef = model.ImgRef
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
				Difference = model.Difference,
				ImgRef = model.ImgRef
			};

			return Ok(profile);
		}

		return NotFound();
	}

	[HttpGet]
	public async Task<IActionResult> Edit()
	{
		var profile = await _profileService.Get(GetCurrentUsername);
		var editProfileViewModel = new EditProfileViewModel { Username = profile.Username, ImgRef = profile.ImgRef };
		return View(editProfileViewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Edit(EditProfileViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var updated = await _profileService.Update(model.Username, model.Image);

		if (updated)
		{
			return RedirectToAction("Profile");
		}

		ModelState.AddModelError(string.Empty, "Произогла ошибка при обновлении профиля");
		return View(ModelState);
	}

	private string GetCurrentUsername => User.Identity?.Name;
}
