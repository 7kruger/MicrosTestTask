using System.Text;
using SHA3.Net;

namespace MicrosTestTask.Domain.Helpers;

public static class PasswordHashHelper
{
    public static string HashPassword(string password)
    {
        using (var sha3256 = Sha3.Sha3256())
        {
            var bytes = Sha3.Sha3256().ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}
