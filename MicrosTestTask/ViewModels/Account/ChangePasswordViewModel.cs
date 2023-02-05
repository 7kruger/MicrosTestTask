using System.ComponentModel.DataAnnotations;

namespace MicrosTestTask.ViewModels.Account;

public class ChangePasswordViewModel
{
	public string Name { get; set; }

	[Required(ErrorMessage = "Введите новый пароль")]
	[DataType(DataType.Password)]
	[MinLength(3, ErrorMessage = "Пароль должен быть не меньше 3 символов")]
	public string NewPassword { get; set; }

	[Required(ErrorMessage = "Повторите новый пароль")]
	[DataType(DataType.Password)]
	[Compare("NewPassword", ErrorMessage = "Пароль не совпадает с новым")]
	public string ConfirmPassword { get; set; }
}
