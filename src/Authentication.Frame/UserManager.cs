using System;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Configuration;
using Authentication.Frame.Stores;
using System.Collections.Generic;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TLogin> : IDisposable
    {
        internal IUserStore<TUser> UserStore { get; set; }
        internal IUserPasswordStore<TUser> PasswordStore { get; set; } 
        internal IUserEmailStore<TUser> EmailStore { get; set; }  
        internal IUserTokenStore<TUser> TokenStore { get; set; }
        internal IUserLockoutStore<TUser> LockoutStore { get; set; }
        internal IUserClaimStore<TUser, TClaim> ClaimStore { get; set; }
        internal IUserFullNameStore<TUser> NameStore { get; set; }
        
        internal ValidationConfiguration<TUser> ValidationConfiguration { get; set; }

        internal SecurityConfiguration<TUser, TClaim> SecurityConfiguration { get; set; }

        internal EmailConfiguration EmailConfiguration { get; set; }

        public bool IsDiposed { get; private set; }

        public UserManager(UserManagerStoreConfiguration<TUser> storeCollection, ValidationConfiguration<TUser> validationConfiguration)
        {
            UserStore = storeCollection.UserStore;
            PasswordStore = storeCollection.PasswordStore;
            EmailStore = storeCollection.EmailStore;
            TokenStore = storeCollection.TokenStore;
            LockoutStore = storeCollection.LockoutStore;
            ValidationConfiguration = validationConfiguration;
        }

        private void Handle(CancellationToken cancellationToken)
        {
            if (IsDiposed)
                throw new ObjectDisposedException(nameof(UserManager<TUser, TClaim>));
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

        internal enum StoreTypes
        {
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
