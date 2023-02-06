using System.ComponentModel.DataAnnotations;

namespace MicrosTestTask.ViewModels.Profile;

public class EditProfileViewModel
{
	[Required]
	public string Username { get; set; }
	public IFormFile? Image { get; set; }
	public string? ImgRef { get; set; }
}
