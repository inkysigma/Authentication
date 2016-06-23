using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores.Results;

namespace Authentication.Frame.Stores
{
    public interface IClientTokenStore<TClient>
    {
        Task<ExecuteResult> AddClientTokenAsync(TClient client, string token, string secret, CancellationToken cancellationToken);

        Task<ExecuteResult> DeleteClientTokenAsync(TClient client, CancellationToken cancellationToken);

        Task<QueryResult<TClient>> FetchClientTokenAsync(string token, CancellationToken cancellationToken);

        Task<QueryResult<int>> FetchNumberOfAttemptsForHour(TClient client, CancellationToken cancellationToken);

        Task<QueryResult<DateTime>> FetchLastCheckAsync(TClient client, CancellationToken cancellationToken);
    }
}
