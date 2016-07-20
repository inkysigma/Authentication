﻿using System;
using System.Threading.Tasks;
using System.Threading;
namespace Authentication.Frame.Stores
{
    /// <summary>
    /// The base interface of all stores. Allows for committing and rolling back transactions.
    /// </summary>
    public interface IStore : IDisposable
    {
        /// <summary>
        /// Rollback the previous transaction.
        /// </summary>
        Task RollbackAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Commit all actions as a transaction
        /// </summary>
        Task CommitAsync(CancellationToken cancellationToken);
    }
}