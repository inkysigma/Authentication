using System;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Configuration;
using Authentication.Frame.Stores;
using System.Collections.Generic;

namespace Authentication.Frame
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public partial class UserManager<TUser, TClaim, TToken> : IDisposable
    {
        internal IUserStore<TUser> UserStore { get; set; }
        internal IUserPasswordStore<TUser> PasswordStore { get; set; } 
        internal IUserEmailStore<TUser> EmailStore { get; set; }  
        internal IUserTokenStore<TUser, TToken> TokenStore { get; set; }
        internal IUserLockoutStore<TUser> LockoutStore { get; set; }
        internal IUserClaimStore<TUser, TClaim> ClaimStore { get; set; }
        internal IUserFullNameStore<TUser> NameStore { get; set; }
        
        internal ValidationConfiguration<TUser> ValidationConfiguration { get; set; }

        internal SecurityConfiguration<TUser, TClaim, TToken> SecurityConfiguration { get; set; }

        internal EmailConfiguration EmailConfiguration { get; set; }

        public bool IsDiposed { get; private set; }

        public UserManager(UserManagerStoreCollection<TUser, TToken> storeCollection, ValidationConfiguration<TUser> validationConfiguration)
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
                throw new ObjectDisposedException(nameof(UserManager<TUser, TClaim, TToken>));
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
