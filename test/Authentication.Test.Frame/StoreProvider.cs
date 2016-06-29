using System;
using System.Collections.Generic;
using System.Threading;
using Authentication.Frame.Stores;
using Authentication.Frame.Stores.Results;
using Moq;
using Xunit;

namespace Authentication.Test.Frame
{
    public class StoreProvider
    {
        public static IUserPasswordStore<TestUser> CreateMockPasswordStore()
        {
            var mock = new Mock<IUserPasswordStore<TestUser>>();

            mock.SetupSequence(moq =>
                moq.CreateUserAsync(It.IsAny<TestUser>(), It.IsAny<byte[]>(), It.IsAny<byte[]>(),
                    CancellationToken.None))
                    .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                    .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                    .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true});

            mock.SetupSequence(moq => moq.DeleteUserAsync(It.IsAny<TestUser>(), CancellationToken.None))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true});

            mock.SetupSequence(moq => moq.FetchPasswordAsync(It.IsAny<TestUser>(), CancellationToken.None))
                .ReturnsAsync(new QueryResult<byte[]>
                {
                    Result = new byte[] {1, 2, 3, 4, 5, 6, 7, 8},
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<byte[]>
                {
                    Succeeded = false
                });

            mock.SetupSequence(moq => moq.FetchSaltAsync(It.IsAny<TestUser>(), CancellationToken.None))
                .ReturnsAsync(new QueryResult<byte[]>
                {
                    Result = new byte[] {1, 2, 3, 4, 5, 6, 7, 8},
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<byte[]>
                {
                    Succeeded = false
                });

            mock.SetupSequence(moq => moq.SetPasswordAsync(It.IsAny<TestUser>(), It.IsAny<byte[]>(), CancellationToken.None))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true});

            mock.SetupSequence(moq => moq.SetSaltAsync(It.IsAny<TestUser>(), It.IsAny<byte[]>(), CancellationToken.None))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true });

            return mock.Object;
        }

        public static IUserClaimStore<TestUser, TestClaim> CreateMockClaimStore()
        {
            var mock = new Mock<IUserClaimStore<TestUser, TestClaim>>();
            mock.Setup(moq => moq.AddClaimsAsync(It.IsAny<TestUser>(), It.IsAny<IEnumerable<TestClaim>()))
            return mock.Object;
        }
    }
}
