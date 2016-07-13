using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores;
using Authentication.Frame.Stores.Results;
using Moq.Language;
using Moq;
using Moq.Language.Flow;

namespace Authentication.Test.Frame.Setup
{
    public static class StoreExtensions
    {
        public static ISetup<T, Task> SetupStore<T>(this Mock<T> current) where T : class, IStore
        {
            current.Setup(moq => moq.CommitAsync(It.IsAny<CancellationToken>()));
            return current.Setup(moq => moq.RollbackAsync(It.IsAny<CancellationToken>()));
        }

        public static ISetupSequentialResult<Task<ExecuteResult>> SetupExecution(this ISetupSequentialResult<Task<ExecuteResult>> current)
        {
            return current.ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { Succeeded = false }); ;
        }
    }
}
