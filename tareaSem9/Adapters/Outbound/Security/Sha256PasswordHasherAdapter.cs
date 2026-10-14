using System;
using System.Security.Cryptography;
using System.Text;
using tareaSem9.Core.Ports.Outbound;

namespace tareaSem9.Adapters.Outbound.Security;

public class Sha256PasswordHasherAdapter : IPasswordHasherPort
{
    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == storedHash;
    }
}
