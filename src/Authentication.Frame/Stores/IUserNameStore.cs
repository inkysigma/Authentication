using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserFullNameStore<in TUser>
    {
        Task<QueryResult<string>> FetchFullNameAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> AddUserFullNameAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> SetUserFullNameAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> RemoveUserFullNameAsync(TUser user, CancellationToken cancellationToken);
    }
}