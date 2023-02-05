using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.ViewModels.Account;
using System.Security.Claims;

namespace MicrosTestTask.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpGet]
	public IActionResult Login() => View();

	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var userModel = new UserModel()
		{
			Name = model.Name,
			Password = model.Password,
		};

		var result = await _accountService.Authenticate(userModel);

		if (result.Succeeded)
		{
			await Authenticate(result.Claims);
			return Redirect("/");
		}

		foreach (var error in result.Errors)
		{
			ModelState.AddModelError(string.Empty, error);
		}

		return View(model);
	}

	[HttpGet]
	public IActionResult Register() => View();

	[HttpPost]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var userModel = new UserModel()
		{
			Name = model.Name,
			Password = model.Password,
		};

		var result = await _accountService.Register(userModel);

		if (result.Succeeded)
		{
			await Authenticate(result.Claims);
			return Redirect("/");
		}

		foreach (var error in result.Errors)
		{
			ModelState.AddModelError(string.Empty, error);
		}

		return View(model);
	}

	[HttpGet]
	[Authorize]
	public IActionResult ChangePassword() => View();

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var result = await _accountService.ChangePassword(model.Name, model.NewPassword);

		if (result)
		{
			return Redirect("/");
		}

		return View("Error", "Не удалось сменить пароль");
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync();
		return RedirectToAction("Login", "Account");
	}

	private async Task Authenticate(ClaimsIdentity identy)
	{
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(identy));
	}
}
