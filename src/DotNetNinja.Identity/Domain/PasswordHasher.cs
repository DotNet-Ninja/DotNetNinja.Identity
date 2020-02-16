using ChaosMonkey.Guards;
using Microsoft.AspNetCore.Identity;

namespace DotNetNinja.Identity.Domain
{
    public class PasswordHasher : IPasswordHasher
    {
        public PasswordHasher(IPasswordHasher<UserAccount> hasher)
        {
            Hasher = Guard.IsNotNull(hasher, nameof(hasher));
        }

        protected IPasswordHasher<UserAccount> Hasher { get; }

        public string HashPassword(string password)
        {
            return Hasher.HashPassword(null, password);
        }

        public PasswordVerificationResult VerifyPassword(string hashPassword, string password)
        {
            return Hasher.VerifyHashedPassword(null, hashPassword, password);
        }
    }
}