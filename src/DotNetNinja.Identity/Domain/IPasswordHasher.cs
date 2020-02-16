using Microsoft.AspNetCore.Identity;

namespace DotNetNinja.Identity.Domain
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        PasswordVerificationResult VerifyPassword(string hashPassword, string password);
    }
}