using Authentication.Frame.Stores;

namespace Authentication.Frame.Configuration
{
    public class StoreConfiguration<TUser, TClaim, TLogin>
    {
        public IUserStore<TUser> UserStore { get; set; }
        public IUserPasswordStore<TUser> PasswordStore { get; set; }
        public IUserClaimStore<TUser, TClaim> ClaimStore { get; set; }
        public IUserLoginStore<TUser, TLogin> LoginStore { get; set; }
        public IUserEmailStore<TUser> EmailStore { get; set; }
        public IUserTokenStore<TUser> TokenStore { get; set; }
        public IUserLockoutStore<TUser> LockoutStore { get; set; }

        internal string Validate()
        {
            if (UserStore == null)
                return nameof(UserStore);
            if (PasswordStore == null)
                return nameof(PasswordStore);
            return null;
        }
    }
}
