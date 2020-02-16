using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetNinja.Identity.Domain
{
    public interface IUserValidator
    {
        Task<UserAccount> FindByExternalProviderAsync(string provider, string userId);
        Task<UserAccount> AutoProvisionUserAsync(string provider, string userId, IEnumerable<Claim> claims);
        Task<bool> ValidateCredentialsAsync(string userName, string password);
        Task<UserAccount> FindByUserNameAsync(string userName);
    }
}