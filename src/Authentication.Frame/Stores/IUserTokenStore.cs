using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores
{
    public interface IUserTokenStore<TUser, TToken> : IDisposable
    {
        Task<TToken> FetchTokenAsync(TUser user, CancellationToken cancellationToken);

        Task<TToken> AddTokenAsync(TUser user, TToken token, CancellationToken cancellationToken);

        Task<TToken> RemoveTokenAsync(TUser user, TToken token, CancellationToken cancellationToken);
    }
}
