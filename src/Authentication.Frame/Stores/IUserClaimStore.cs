using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserClaimStore<in TUser, TClaim> : IDisposable, IStore
    {
        Task<ExecuteResult> DeleteUserAsync(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<IQueryable<TClaim>>> FetchClaimsAsync(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteClaimAsync(TUser user, TClaim claim, CancellationToken cancellationToken);

        Task<ExecuteResult> CreateClaimAsync(TUser user, TClaim claim, CancellationToken cancellationToken);

        Task<ExecuteResult> AddClaimsAsync(TUser user, IEnumerable<TClaim> claims, CancellationToken cancellationToken);
    }
}
