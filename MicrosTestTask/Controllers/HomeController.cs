using Microsoft.AspNetCore.Mvc;

namespace MicrosTestTask.Controllers;

public class HomeController : Controller
{
	public IActionResult Index() => View();
}
