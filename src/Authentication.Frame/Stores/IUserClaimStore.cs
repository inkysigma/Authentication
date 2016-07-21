using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores
{
    public interface IUserClaimStore<in TUser, TClaim> : IStore
    {
        Task<int> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<IQueryable<TClaim>>> FetchClaimsAsync(TUser user, CancellationToken cancellationToken);

        Task<int> DeleteClaimAsync(TUser user, TClaim claim, CancellationToken cancellationToken);

        Task<int> CreateClaimAsync(TUser user, TClaim claim, CancellationToken cancellationToken);

        Task<int> CreateClaimsAsync(TUser user, IEnumerable<TClaim> claims, CancellationToken cancellationToken);
    }
}
