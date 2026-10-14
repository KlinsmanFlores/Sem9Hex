namespace tareaSem9.Core.Ports.Outbound;

public interface IPasswordHasherPort
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string storedHash);
}
