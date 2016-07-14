using System;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame.Stores
{
    public interface IClientTokenStore<TClient> : IDisposable
    {
        Task<int> CreateClientTokenAsync(TClient client, string token, string secret, CancellationToken cancellationToken);

        Task<int> DeleteClientTokenAsync(TClient client, CancellationToken cancellationToken);

        Task<QueryResult<TClient>> FetchClientTokenAsync(string token, CancellationToken cancellationToken);

        Task<QueryResult<int>> FetchNumberOfAttemptsForHour(TClient client, CancellationToken cancellationToken);

        Task<QueryResult<DateTime>> FetchLastCheckAsync(TClient client, CancellationToken cancellationToken);
    }
}
