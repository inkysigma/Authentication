using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserEmailStore<TUser> : IDisposable, IStore
    {
        Task<ExecuteResult> CreateUserAsync(TUser user, string email, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> SetEmailAsync(TUser user, string email, CancellationToken cancellationToken);

        Task<ExecuteResult> SetValidatedAsync(TUser user, bool isValidated, CancellationToken cancellationToken);

        Task<QueryResult<bool>> FetchValidatedAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<string>> FetchEmailAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<TUser>> FetchUser(string email, CancellationToken cancellationToken);

        Task<QueryResult<TUser>> FetchUser(TUser user, CancellationToken cancellationToken);
    }
}
