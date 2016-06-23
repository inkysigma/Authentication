using Authentication.Frame.Configuration;
using Authentication.Frame.Stores;

namespace Authentication.Frame
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class UserManager<TUser, TToken>
    {
        internal IUserStore<TUser> UserStore { get; set; }
        internal IUserPasswordStore<TUser> PasswordStore { get; set; } 
        internal IUserEmailStore<TUser> EmailStore { get; set; }  
        internal IUserTokenStore<TUser, TToken> TokenStore { get; set; }  
        internal IUserLockoutStore<TUser> LockoutStore { get; set; }

        public UserManager(StoreCollection<TUser, TToken> storeCollection)
        {
            UserStore = storeCollection.UserStore;
            PasswordStore = storeCollection.PasswordStore;
            EmailStore = storeCollection.EmailStore;
            TokenStore = storeCollection.TokenStore;
            LockoutStore = storeCollection.LockoutStore;
        }
    }
}
