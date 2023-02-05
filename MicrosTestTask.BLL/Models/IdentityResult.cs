using System.Security.Claims;

namespace MicrosTestTask.BLL.Models;

public class IdentityResult
{
	public IdentityResult(IEnumerable<string> errors, bool succeeded, ClaimsIdentity claims)
	{
		Errors = errors;
		Succeeded = succeeded;
		Claims = claims;
	}

	public IEnumerable<string> Errors { get; }
	public bool Succeeded { get; }
	public ClaimsIdentity Claims { get; }
}
