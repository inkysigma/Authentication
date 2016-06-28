﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Configuration;
using Authentication.Frame.Stores;

namespace Authentication.Frame
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class UserManager<TUser, TToken> : IDisposable
    {
        internal IUserStore<TUser> UserStore { get; set; }
        internal IUserPasswordStore<TUser> PasswordStore { get; set; } 
        internal IUserEmailStore<TUser> EmailStore { get; set; }  
        internal IUserTokenStore<TUser, TToken> TokenStore { get; set; }  
        internal IUserLockoutStore<TUser> LockoutStore { get; set; }

        public bool IsDiposed { get; private set; }

        public UserManager(UserManagerStoreCollection<TUser, TToken> storeCollection)
        {
            UserStore = storeCollection.UserStore;
            PasswordStore = storeCollection.PasswordStore;
            EmailStore = storeCollection.EmailStore;
            TokenStore = storeCollection.TokenStore;
            LockoutStore = storeCollection.LockoutStore;
        }

        public void Handle(CancellationToken cancellationToken)
        {
            if (IsDiposed)
                throw new ObjectDisposedException(nameof(UserManager<TUser, TToken>));
            cancellationToken.ThrowIfCancellationRequested();
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
            IsDiposed = true;
        }

        public async Task<string> CreateUserAsync(TUser user, string username, string password, string email, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(nameof(password)))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(nameof(email)))
                throw new ArgumentNullException(nameof(email));
            Handle(cancellationToken);
            var id = Guid.NewGuid().ToString();
        }
    }
}
