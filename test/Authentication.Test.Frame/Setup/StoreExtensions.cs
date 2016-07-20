using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Stores;
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

        public static ISetupSequentialResult<Task<int>> SetupExecution(this ISetupSequentialResult<Task<int>> current)
        {
            return current.ReturnsAsync(1)
                .ReturnsAsync(2)
                .ReturnsAsync(0);
        }
    }
}
