using System;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserLockoutStore<in TUser> : IStore
    {
        Task<ExecuteResult> CreateUserAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> LockoutAsync(TUser user, DateTime end, CancellationToken cancellationToken);

        Task<ExecuteResult> UnlockAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<int>> FetchAttemptsAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<bool>> FetchLockedAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<DateTime>> FetchEndDate(TUser user, CancellationToken cancellationToken);
    }
}