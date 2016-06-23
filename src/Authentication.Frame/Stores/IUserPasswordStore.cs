using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserPasswordStore<in TUser> : IDisposable
    {
        Task<ExecuteResult> AddUserAsync(TUser user, byte[] password, byte[] salt, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<byte[]>> FetchPasswordAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<byte[]>> FetchSaltAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> SetSaltAsync(TUser user, byte[] salt, CancellationToken cancellationToken);
    }
}
