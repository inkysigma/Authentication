using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores
{
    public interface IUserEmailStore<TUser> : IStore
    {
        Task<int> CreateUserAsync(TUser user, string email, CancellationToken cancellationToken);

        Task<int> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<int> SetEmailAsync(TUser user, string email, CancellationToken cancellationToken);

        Task<int> SetValidatedAsync(TUser user, bool isValidated, CancellationToken cancellationToken);

        Task<QueryResult<bool>> FetchValidatedAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<string>> FetchEmailAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<TUser>> FetchUser(string email, CancellationToken cancellationToken);

        Task<QueryResult<TUser>> FetchUser(TUser user, CancellationToken cancellationToken);
    }
}
