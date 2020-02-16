using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using DotNetNinja.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace DotNetNinja.Identity.Domain
{
    public class UserValidator : IUserValidator
    {
        public UserValidator(IUserRepository repository, IPasswordHasher hasher)
        {
            Users = Guard.IsNotNull(repository, nameof(repository));
            Hasher = Guard.IsNotNull(hasher, nameof(hasher));
        }

        protected IUserRepository Users { get; }
        protected  IPasswordHasher Hasher { get; }

        // TODO: Enable External Providers
        public Task<UserAccount> FindByExternalProviderAsync(string provider, string userId)
        {
            return Task.FromResult((UserAccount)null);
        }

        // TODO: Enable External Providers / Auto Provisioning
        public Task<UserAccount> AutoProvisionUserAsync(string provider, string userId, IEnumerable<Claim> claims)
        {
            return Task.FromResult((UserAccount)null);
        }

        public async Task<bool> ValidateCredentialsAsync(string userName, string password)
        {
            var user = await Users.RetrieveByUserNameAsync(userName);
            if (user == null)
            {
                return false;
            }
            var passwordResult = Hasher.VerifyPassword(user.PasswordHash, password);
            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return false;
            }
            if (passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = Hasher.HashPassword(password);
                await Users.CommitAsync();
            }

            return true;
        }

        public Task<UserAccount> FindByUserNameAsync(string userName)
        {
            return Users.RetrieveByUserNameAsync(userName);
        }
    }
}