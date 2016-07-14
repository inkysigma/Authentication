using System;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores
{
    public interface IUserTokenStore<in TUser> : IStore
    {
        Task<QueryResult<string>> FetchTokenAsync(TUser user, CancellationToken cancellationToken);

        Task<int> CreateTokenAsync(TUser user, string token, CancellationToken cancellationToken);

        Task<int> RemoveTokenAsync(TUser user, string token, CancellationToken cancellationToken);

        Task<int> RemoveUserAsync(TUser user, CancellationToken cancellationToken);
    }
}
