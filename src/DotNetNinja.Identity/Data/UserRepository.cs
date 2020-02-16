using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChaosMonkey.Guards;
using DotNetNinja.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.Identity.Data
{
    public class UserRepository: IUserRepository
    {
        public UserRepository(UserDbContext db)
        {
            Db = Guard.IsNotNull(db, nameof(db));
        }

        protected UserDbContext Db { get; }

        protected IQueryable<UserAccount> Projection => Db.UserAccounts.AsNoTracking();

        public Task<UserAccount> RetrieveAsync(int id)
        {
            return Projection.SingleOrDefaultAsync(account => account.Id == id);
        }

        public Task<UserAccount> RetrieveBySubjectAsync(Guid subject)
        {
            return Projection.SingleOrDefaultAsync(account => account.Subject == subject);
        }

        public Task<UserAccount> RetrieveByUserNameAsync(string userName)
        {
            return Projection.SingleOrDefaultAsync(account => account.UserName == userName);
        }

        public void UpdateUserAccount(UserAccount account)
        {
            account.DateModified = DateTimeOffset.Now;
            Db.UserAccounts.Update(account);
        }

        public Task<int> CommitAsync()
        {
            return Db.SaveChangesAsync();
        }

        public Task<int> CommitAsync(CancellationToken cancellation)
        {
            return Db.SaveChangesAsync(cancellation);
        }
    }
}