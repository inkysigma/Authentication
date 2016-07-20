using System;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores
{
    public interface IUserPasswordStore<in TUser> : IStore
    {
        Task<int> CreateUserAsync(TUser user, byte[] password, byte[] salt, CancellationToken cancellationToken);

        Task<int> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<byte[]>> FetchPasswordAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<byte[]>> FetchSaltAsync(TUser user, CancellationToken cancellationToken);

        Task<int> SetPasswordAsync(TUser user, byte[] password, CancellationToken cancellationToken);

        Task<int> SetSaltAsync(TUser user, byte[] salt, CancellationToken cancellationToken);
    }
}
