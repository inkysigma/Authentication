using System;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserPasswordStore<in TUser> : IDisposable, IStore
    {
        Task<ExecuteResult> CreateUserAsync(TUser user, byte[] password, byte[] salt, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<byte[]>> FetchPasswordAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<byte[]>> FetchSaltAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> SetPasswordAsync(TUser user, byte[] password, CancellationToken cancellationToken);

        Task<ExecuteResult> SetSaltAsync(TUser user, byte[] salt, CancellationToken cancellationToken);
    }
}
