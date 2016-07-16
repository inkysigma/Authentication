using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores
{
    public interface IUserLoginStore<in TUser, TLogin> : IStore
    {
        Task<int> CreateUserLoginAsync(TUser user, TLogin login, DateTime creationTime, CancellationToken cancellationToken);

        Task<int> DeleteUserLoginAsync(TUser user, TLogin login, CancellationToken cancellationToken);

        Task<int> DeleteUser(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<IQueryable<TLogin>>> FetchUserLogins(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<bool>> HasLogin(TUser user, TLogin hash, CancellationToken cancellationToken);

        Task ClearAsync();
    }
}
