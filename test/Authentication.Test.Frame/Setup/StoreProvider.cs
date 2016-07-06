using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Authentication.Frame.Stores;
using Authentication.Frame.Stores.Results;
using Moq;

namespace Authentication.Test.Frame.Setup
{
    public class StoreProvider
    {
        public static IUserPasswordStore<TestUser> CreateMockPasswordStore()
        {
            var mock = new Mock<IUserPasswordStore<TestUser>>();

            mock.SetupSequence(moq =>
                moq.CreateUserAsync(It.IsAny<TestUser>(), It.IsAny<byte[]>(), It.IsAny<byte[]>(),
                    It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                    .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                    .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                    .ReturnsAsync(new ExecuteResult {Succeeded = false});

            mock.SetupSequence(moq => moq.DeleteUserAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.FetchPasswordAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<byte[]>
                {
                    Result = new byte[] {1, 2, 3, 4, 5, 6, 7, 8},
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<byte[]>
                {
                    Succeeded = false
                });

            mock.SetupSequence(moq => moq.FetchSaltAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<byte[]>
                {
                    Result = new byte[] {1, 2, 3, 4, 5, 6, 7, 8},
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<byte[]>
                {
                    Succeeded = false
                });

            mock.SetupSequence(moq => moq.SetPasswordAsync(It.IsAny<TestUser>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.SetSaltAsync(It.IsAny<TestUser>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.Setup(moq => moq.RollbackAsync(It.IsAny<CancellationToken>()));

            mock.Setup(moq => moq.CommitAsync(It.IsAny<CancellationToken>()));

            return mock.Object;
        }

        public static IUserClaimStore<TestUser, TestLogin> CreateMockClaimStore()
        {
            var mock = new Mock<IUserClaimStore<TestUser, TestLogin>>();
            mock.SetupSequence(
                moq =>
                    moq.CreateClaimsAsync(It.IsAny<TestUser>(), It.IsAny<IEnumerable<TestLogin>>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(
                    moq => moq.CreateClaimAsync(It.IsAny<TestUser>(), It.IsAny<TestLogin>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(
                    moq =>
                        moq.DeleteClaimAsync(It.IsAny<TestUser>(), It.IsAny<TestLogin>(),
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(
                    moq => moq.DeleteUserAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.FetchClaimsAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<IQueryable<TestLogin>>
                {
                    Result = (new List<TestLogin>()).AsQueryable(),
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<IQueryable<TestLogin>>
                {
                    Succeeded = false
                });

            mock.Setup(moq => moq.RollbackAsync(It.IsAny<CancellationToken>()));

            mock.Setup(moq => moq.CommitAsync(It.IsAny<CancellationToken>()));

            return mock.Object;
        }

        public static IUserEmailStore<TestUser> CreateMockEmailStore()
        {
            var mock = new Mock<IUserEmailStore<TestUser>>();

            mock.SetupSequence(moq => moq.CreateUserAsync(It.IsAny<TestUser>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })           
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(
                    moq => moq.DeleteUserAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.FetchEmailAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<string>
                {
                    Result = "example@example.com",
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<string>
                {
                    Succeeded = false
                });

            mock.SetupSequence(moq => moq.FetchUser(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<TestUser>
                {
                    Result = new TestUser(),
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<TestUser>
                {
                    Succeeded = false
                });

            mock.SetupSequence(moq => moq.FetchValidatedAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<bool>
                {
                    Result = true,
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<bool>
                {
                    Result = false,
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<bool>
                {
                    Succeeded = false
                });

            mock.SetupSequence(
                    moq => moq.SetEmailAsync(It.IsAny<TestUser>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                .ReturnsAsync(new ExecuteResult { Succeeded = false });
        
            mock.SetupSequence(
                    moq => moq.SetValidatedAsync(It.IsAny<TestUser>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.RollbackAsync(It.IsAny<CancellationToken>()));


            mock.Setup(moq => moq.CommitAsync(It.IsAny<CancellationToken>()));

            return mock.Object;
        }

        public static IUserLockoutStore<TestUser> CreateMockLockoutStore()
        {
            var mock = new Mock<IUserLockoutStore<TestUser>>();

            mock.SetupSequence(moq => moq.CreateUserAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult {RowsModified = 1, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 2, Succeeded = true})
                .ReturnsAsync(new ExecuteResult {RowsModified = 0, Succeeded = true})
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.DeleteUserAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.FetchAttemptsAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<int>
                {
                    Result = 1,
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<int>
                {
                    Result = 10000000,
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<int>
                {
                    Succeeded = false
                });

            mock.SetupSequence(moq => moq.FetchEndDate(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<DateTime>
                {
                    Result = DateTime.Now - TimeSpan.FromDays(1),
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<DateTime>
                {
                    Result = DateTime.Now + TimeSpan.FromDays(1),
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<DateTime>
                {
                    Succeeded = false
                });

            mock.SetupSequence(moq => moq.FetchLockedAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<bool>
                {
                    Result = true,
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<bool>
                {
                    Result = false,
                    Succeeded = true
                })
                .ReturnsAsync(new QueryResult<bool>
                {
                    Succeeded = false
                });

            mock.SetupSequence(moq => moq.LockoutAsync(It.IsAny<TestUser>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.UnlockAsync(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.Setup(moq => moq.RollbackAsync(It.IsAny<CancellationToken>()));

            mock.Setup(moq => moq.CommitAsync(It.IsAny<CancellationToken>()));

            return mock.Object;
        }

        public static IUserLoginStore<TestUser, TestLogin> CreateMockLoginStore()
        {
            var mock = new Mock<IUserLoginStore<TestUser, TestLogin>>();

            mock.SetupSequence(moq => moq.CreateUserLoginAsync(It.IsAny<TestUser>(), It.IsAny<TestLogin>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.DeleteUser(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.DeleteUserLoginAsync(It.IsAny<TestUser>(), It.IsAny<TestLogin>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ExecuteResult { RowsModified = 1, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 2, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { RowsModified = 0, Succeeded = true })
                .ReturnsAsync(new ExecuteResult { Succeeded = false });

            mock.SetupSequence(moq => moq.FetchUserLogins(It.IsAny<TestUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<IQueryable<TestLogin>>
                {
                    Result = (new List<TestLogin>()).AsQueryable(),
                    Succeeded = true,
                    RowsModified = 1
                })
                .ReturnsAsync(new QueryResult<IQueryable<TestLogin>>
                {
                    Result = (new List<TestLogin>()).AsQueryable(),
                    Succeeded = true,
                    RowsModified = 2
                })
                .ReturnsAsync(new QueryResult<IQueryable<TestLogin>>
                {
                    Result = (new List<TestLogin>()).AsQueryable(),
                    Succeeded = true,
                    RowsModified = 0
                })
                .ReturnsAsync(new QueryResult<IQueryable<TestLogin>>
                {
                    Succeeded = false
                });

            return mock.Object;
        }
    }
}
