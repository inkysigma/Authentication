using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Configuration;
using Authentication.Frame.Result;
using Authentication.Frame.Stores;
using Authentication.Frame.Stores.Results;
using Microsoft.Extensions.Logging;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TLogin> : IDisposable
    {
        private ILogger Logger { get; set; }

        internal IUserStore<TUser> UserStore { get; set; }
        internal IUserPasswordStore<TUser> PasswordStore { get; set; } 
        internal IUserEmailStore<TUser> EmailStore { get; set; }  
        internal IUserTokenStore<TUser> TokenStore { get; set; }
        internal IUserLockoutStore<TUser> LockoutStore { get; set; }
        internal IUserClaimStore<TUser, TClaim> ClaimStore { get; set; }
        internal IUserFullNameStore<TUser> NameStore { get; set; }
        internal IUserLoginStore<TUser, TLogin> LoginStore { get; set; }
        
        internal ValidationConfiguration<TUser> ValidationConfiguration { get; set; }

        internal SecurityConfiguration<TUser, TClaim> SecurityConfiguration { get; set; }

        internal EmailConfiguration EmailConfiguration { get; set; }

        public bool IsDiposed { get; private set; }

        public UserManager(StoreConfiguration<TUser, TClaim, TLogin> storeCollection, 
            ValidationConfiguration<TUser> validationConfiguration, ILogger logger)
        {
            if (storeCollection == null)
                throw new ArgumentNullException(nameof(storeCollection));
            if (storeCollection.Validate() != null)
                throw new ArgumentNullException(storeCollection.Validate());
            if (validationConfiguration == null)
                throw new ArgumentNullException(nameof(validationConfiguration));
            if (validationConfiguration.Validate() != null)
                throw new ArgumentNullException(validationConfiguration.Validate());
            UserStore = storeCollection.UserStore;
            PasswordStore = storeCollection.PasswordStore;
            EmailStore = storeCollection.EmailStore;
            ClaimStore = storeCollection.ClaimStore;
            TokenStore = storeCollection.TokenStore;
            LockoutStore = storeCollection.LockoutStore;
            ValidationConfiguration = validationConfiguration;
            Logger = logger;
        }

        private void Handle(CancellationToken cancellationToken)
        {
            if (IsDiposed)
                throw new ObjectDisposedException(nameof(UserManager<TUser, TClaim, TLogin>));
            cancellationToken.ThrowIfCancellationRequested();
        }

        private async Task Rollback(CancellationToken cancellationToken, params StoreTypes[] rollback)
        {
            if (rollback == null)
                throw new ArgumentNullException(nameof(rollback));
            foreach (var i in rollback)
            {
                switch (i)
                {
                    case StoreTypes.UserStore:
                        await UserStore.RollbackAsync(cancellationToken);
                        break;
                    case StoreTypes.EmailStore:
                        await EmailStore.RollbackAsync(cancellationToken);
                        break;
                    case StoreTypes.LockoutStore:
                        await LockoutStore.RollbackAsync(cancellationToken);
                        break;
                    case StoreTypes.NameStore:
                        await NameStore.RollbackAsync(cancellationToken);
                        break;
                    case StoreTypes.PasswordStore:
                        await PasswordStore.RollbackAsync(cancellationToken);
                        break;
                    case StoreTypes.TokenStore:
                        await TokenStore.RollbackAsync(cancellationToken);
                        break;
                    case StoreTypes.ClaimStore:
                        await ClaimStore.RollbackAsync(cancellationToken);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private async Task Commit(CancellationToken cancellationToken, params StoreTypes[] commit)
        {
            if (commit == null)
                throw new ArgumentNullException(nameof(commit));
            foreach (var i in commit)
            {
                switch (i)
                {
                    case StoreTypes.UserStore:
                        await UserStore.CommitAsync(cancellationToken);
                        break;
                    case StoreTypes.EmailStore:
                        await EmailStore.CommitAsync(cancellationToken);
                        break;
                    case StoreTypes.LockoutStore:
                        await LockoutStore.CommitAsync(cancellationToken);
                        break;
                    case StoreTypes.NameStore:
                        await NameStore.CommitAsync(cancellationToken);
                        break;
                    case StoreTypes.PasswordStore:
                        await PasswordStore.CommitAsync(cancellationToken);
                        break;
                    case StoreTypes.TokenStore:
                        await TokenStore.CommitAsync(cancellationToken);
                        break;
                    case StoreTypes.ClaimStore:
                        await ClaimStore.CommitAsync(cancellationToken);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private async Task AssertSingle(TUser user, ExecuteResult result, CancellationToken cancellationToken, List<StoreTypes> stores)
        {
            if (!result.Succeeded && result.RowsModified != 1)
            {
                await Rollback(cancellationToken, stores.ToArray());
                Logger.LogWarning($"User with {await FetchUserKeyAsync(user, cancellationToken)} and {await FetchUserNameAsync(user, cancellationToken)}");
                throw new ServerFaultException("Too many rows were modified");
            }
        }

        private async Task AssertSingle<T>(TUser user, QueryResult<T> query,
            CancellationToken cancellationToken, List<StoreTypes> stores)
        {
            if (!query.Succeeded && query.RowsModified != 1)
            {
                await Rollback(cancellationToken, stores.ToArray());
                throw new ServerFaultException("Too many rows were modified");
            }
        }

        private async Task AssertSingle(TUser user, ExecuteResult result, CancellationToken cancellationToken,
            params StoreTypes[] stores)
        {
            if (!result.Succeeded && result.RowsModified != 1)
            {
                await Rollback(cancellationToken, stores);
                Logger.LogWarning($"User with {await FetchUserKeyAsync(user, cancellationToken)} and {await FetchUserName(user, cancellationToken)}");
                throw new ServerFaultException("Too many rows were modified");
            }
        } 

        public void Dispose()
        {
            if (IsDiposed)
                return;
            UserStore.Dispose();
            PasswordStore.Dispose();
            EmailStore.Dispose();
            TokenStore.Dispose();
            LockoutStore.Dispose();
            NameStore.Dispose();
            ClaimStore.Dispose();
            IsDiposed = true;
        }

        private enum StoreTypes
        {
            LoginStore,
            UserStore,
            PasswordStore,
            EmailStore,
            TokenStore,
            LockoutStore,
            NameStore,
            ClaimStore
        }
    }

}
