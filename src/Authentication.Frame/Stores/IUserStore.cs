using System;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserStore<TUser> : IStore
    {
        Task<QueryResult<TUser>> CreateUserAsync(TUser user, string id, string username, DateTime creationTime, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> UpdateUserAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<TUser>> FetchUserAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<TUser>> FetchUserById(string id, CancellationToken cancellationToken);

        Task<QueryResult<string>> FetchUserKeyAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<TUser>> FetchUserByUsernameAsync(string username, CancellationToken cancellationToken);

        Task<QueryResult<string>> FetchUserNameAsync(TUser user, CancellationToken cancellationToken);
    }
}
