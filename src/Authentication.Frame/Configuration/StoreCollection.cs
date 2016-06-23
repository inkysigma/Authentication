using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Frame.Stores;

namespace Authentication.Frame.Configuration
{
    public class UserManagerStoreCollection<TUser, TToken>
    {
        public IUserStore<TUser> UserStore { get; set; }
        public IUserPasswordStore<TUser> PasswordStore { get; set; }
        public IUserEmailStore<TUser> EmailStore { get; set; }
        public IUserTokenStore<TUser, TToken> TokenStore { get; set; }
        public IUserLockoutStore<TUser> LockoutStore { get; set; }
    }
}
