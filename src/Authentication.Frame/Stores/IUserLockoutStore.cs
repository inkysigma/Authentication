using System;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores
{
    public interface IUserLockoutStore<in TUser> : IStore
    {
        Task<int> CreateUserAsync(TUser user, CancellationToken cancellationToken);

        Task<int> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<int> LockoutAsync(TUser user, DateTime end, CancellationToken cancellationToken);

        Task<int> UnlockAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<int>> FetchAttemptsAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<bool>> FetchLockedAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<DateTime>> FetchEndDate(TUser user, CancellationToken cancellationToken);
    }
}