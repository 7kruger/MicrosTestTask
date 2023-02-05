using System.ComponentModel.DataAnnotations;

namespace MicrosTestTask.ViewModels.Account;

public class RegisterViewModel
{
	[Required(ErrorMessage = "Не указан логин")]
	public string Name { get; set; }

	[Required(ErrorMessage = "Не указан пароль")]
	[MinLength(3, ErrorMessage = "Пароль должен содержать минимум 3 символа")]
	[DataType(DataType.Password)]
	public string Password { get; set; }

	[Required(ErrorMessage = "Поле подтверждения пароля обязательное")]
	[DataType(DataType.Password)]
	[Compare("Password", ErrorMessage = "Пароли не совпадают")]
	public string ConfirmPassword { get; set; }
}
