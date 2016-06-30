using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserTokenStore<TUser> : IDisposable, IStore
    {
        Task<QueryResult<string>> FetchTokenAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> CreateTokenAsync(TUser user, string token, CancellationToken cancellationToken);

        Task<ExecuteResult> RemoveTokenAsync(TUser user, string token, CancellationToken cancellationToken);
    }
}
