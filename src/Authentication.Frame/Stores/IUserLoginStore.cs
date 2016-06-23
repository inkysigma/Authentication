using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserLoginStore<in TUser, TLogin>
    {
        Task<ExecuteResult> AddUserLoginAsync(TUser user, TLogin login, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUserLoginAsync(TUser user, TLogin login, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUser(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<IQueryable<TLogin>>> FetchUserLogins(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<bool>> HasLogin(TUser user, TLogin hash, CancellationToken cancellationToken);
    }
}
