using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserTokenStore<in TUser, TToken> : IDisposable, IStore
    {
        Task<QueryResult<TToken>> FetchTokenAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> CreateTokenAsync(TUser user, TToken token, CancellationToken cancellationToken);

        Task<ExecuteResult> RemoveTokenAsync(TUser user, TToken token, CancellationToken cancellationToken);
    }
}
