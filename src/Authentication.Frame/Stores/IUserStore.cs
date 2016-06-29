using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserStore<TUser> : IDisposable, IStore
    {
        Task<ExecuteResult> CreateUserAsync(TUser user, string id, string username, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> UpdateUserAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<TUser>> FetchUserAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<TUser>> FetchUserByUsernameAsync(string name, CancellationToken cancellationToken);
    }
}
