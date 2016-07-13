using System;
using System.Threading.Tasks;
using System.Threading;
namespace Authentication.Frame.Stores
{
    /// <summary>
    /// The base interface of all stores. Allows for committing and rolling back transactions.
    /// </summary>
    public interface IStore : IDisposable
    {
        Task RollbackAsync(CancellationToken cancellationToken);
        Task CommitAsync(CancellationToken cancellationToken);
    }
}