using System;
using System.Security.Policy;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using DotNetNinja.Identity.Data;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace DotNetNinja.Identity.Domain
{
    public class ResourceOwnerPasswordValidator: IResourceOwnerPasswordValidator
    {
        public ResourceOwnerPasswordValidator(IUserRepository repository, IPasswordHasher hasher)
        {
            Users = Guard.IsNotNull(repository, nameof(repository));
            Hasher = Guard.IsNotNull(hasher, nameof(hasher));
        }

        protected IUserRepository Users { get; }
        protected IPasswordHasher Hasher { get; }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await Users.RetrieveByUserNameAsync(context.UserName);
            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid Credentials");
                return;
            }
            var passwordResult = Hasher.VerifyPassword(user.PasswordHash, context.Password);
            if (passwordResult == PasswordVerificationResult.Failed)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid Credentials");
                return;
            }
            if (passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = Hasher.HashPassword(context.Password);
                Users.UpdateUserAccount(user);
            }
            context.Result = new GrantValidationResult(user.Subject.ToString(), "pwd" ,DateTime.Now);
        }
    }
}