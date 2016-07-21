using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Configuration;
using Authentication.Frame.Result;
using Authentication.Frame.Security;
using Authentication.Frame.Stores;

namespace Authentication.Frame
{
    public class LoginManager<TUser, TClaim, TLogin> : IDisposable
    {
        private IUserPasswordStore<TUser> PasswordStore { get; }
        private IUserLoginStore<TUser, TLogin> LoginStore { get; }
        private IUserClaimStore<TUser, TClaim> ClaimStore { get; }
        private UserManager<TUser, TClaim, TLogin> UserManager { get; }
        private ILoginTokenProvider<TUser, TLogin> LoginProvider { get; set; }
        private bool IsDisposed { get; set; }

        public LoginManager(IUserPasswordStore<TUser> passwordStore, IUserLoginStore<TUser, TLogin> loginStore,
            IUserClaimStore<TUser, TClaim> claimStore, UserManager<TUser, TClaim, TLogin> userManage)
        {
            PasswordStore = passwordStore;
            LoginStore = loginStore;
            ClaimStore = claimStore;
            UserManager = userManage;
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;
            PasswordStore.Dispose();
            LoginStore.Dispose();
            ClaimStore.Dispose();
            IsDisposed = true;
        }

        private void Handle(CancellationToken cancellationToken)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(LoginManager<TUser, TClaim, TLogin>));
            cancellationToken.ThrowIfCancellationRequested();
        }

        public async Task<AuthenticationResult<bool>> VerifyLogin(TUser user, TLogin login, CancellationToken cancellationToken)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));
            Handle(cancellationToken);
            var query = await UserManager.FetchUserAsync(user, cancellationToken);
            if (query.Type != AuthenticationType.Success)
                return AuthenticationResult<bool>.Error();

            var result = await LoginStore.FetchUserLogins(user, cancellationToken);

            if (result.Result.Any(f => await LoginProvider.CompareTokensAsync()))
            
        }
    }
}
