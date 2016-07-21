using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores
{
    public interface IUserFullNameStore<in TUser>: IStore
    {
        Task<QueryResult<string>> FetchFullNameAsync(TUser user, CancellationToken cancellationToken);

        Task<int> CreateUserAsync(TUser user, string name, CancellationToken cancellationToken);

        Task<int> SetUserFullNameAsync(TUser user, string name, CancellationToken cancellationToken);

        Task<int> DeleteUserAsync(TUser user, CancellationToken cancellationToken);
    }
}