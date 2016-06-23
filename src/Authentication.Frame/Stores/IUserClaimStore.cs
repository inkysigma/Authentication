using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IUserClaimStore<in TUser, TClaim> : IDisposable
    {
        Task<ExecuteResult> CreateUser(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteUser(TUser user, CancellationToken cancellationToken);

        Task<QueryResult<IQueryable<TClaim>>> FetchClaims(TUser user, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteClaim(TUser user, TClaim claim, CancellationToken cancellationToken);

        Task<ExecuteResult> AddClaim(TUser user, TClaim claim, CancellationToken cancellationToken);

        Task<ExecuteResult> AddClaims(TUser user, IEnumerable<TClaim> claims, CancellationToken cancellationToken);
    }
}
