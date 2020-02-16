using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetNinja.Identity.Domain;

namespace DotNetNinja.Identity.Data
{
    public interface IUserRepository
    {
        Task<UserAccount> RetrieveAsync(int id);
        Task<UserAccount> RetrieveBySubjectAsync(Guid subject);
        Task<UserAccount> RetrieveByUserNameAsync(string userName);

        void UpdateUserAccount(UserAccount account);
        Task<int> CommitAsync();
        Task<int> CommitAsync(CancellationToken cancellation);
    }
}