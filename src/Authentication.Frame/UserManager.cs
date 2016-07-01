using System;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Configuration;
using Authentication.Frame.Stores;
using System.Collections.Generic;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim> : IDisposable
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

        public UserManager(UserManagerStoreCollection<TUser> storeCollection, ValidationConfiguration<TUser> validationConfiguration)
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

        private async Task Rollback(List<StoreTypes> rollback)
        {
            if (rollback == null)
                throw new ArgumentNullException(nameof(rollback));
            foreach (var i in rollback)
            {
                switch (i)
                {
                    case StoreTypes.UserStore:
                        await UserStore.Rollback();
                        break;
                    case StoreTypes.EmailStore:
                        await EmailStore.Rollback();
                        break;
                    case StoreTypes.LockoutStore:
                        await LockoutStore.Rollback();
                        break;
                    case StoreTypes.NameStore:
                        await NameStore.Rollback();
                        break;
                    case StoreTypes.PasswordStore:
                        await PasswordStore.Rollback();
                        break;
                    case StoreTypes.TokenStore:
                        await TokenStore.Rollback();
                        break;
                    case StoreTypes.ClaimStore:
                        await ClaimStore.Rollback();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private async Task Commit(List<StoreTypes> commit)
        {
            if (commit == null)
                throw new ArgumentNullException(nameof(commit));
            foreach (var i in commit)
            {
                switch (i)
                {
                    case StoreTypes.UserStore:
                        await UserStore.Commit();
                        break;
                    case StoreTypes.EmailStore:
                        await EmailStore.Commit();
                        break;
                    case StoreTypes.LockoutStore:
                        await LockoutStore.Commit();
                        break;
                    case StoreTypes.NameStore:
                        await NameStore.Commit();
                        break;
                    case StoreTypes.PasswordStore:
                        await PasswordStore.Commit();
                        break;
                    case StoreTypes.TokenStore:
                        await TokenStore.Commit();
                        break;
                    case StoreTypes.ClaimStore:
                        await ClaimStore.Commit();
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
