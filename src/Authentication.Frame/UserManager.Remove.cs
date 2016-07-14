using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Result;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TLogin>
    {
        public async Task<AuthenticationResult> RemoveUser(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Handle(cancellationToken);
            var stores = new List<StoreTypes>();

            var result = await EmailStore.DeleteUserAsync(user, cancellationToken);
            stores.Add(StoreTypes.EmailStore);
            await AssertSingle(user, result, cancellationToken, stores);

            result = await ClaimStore.DeleteUserAsync(user, cancellationToken);
            stores.Add(StoreTypes.ClaimStore);
            await AssertSingle(user, result, cancellationToken, stores);

            await LoginStore.DeleteUser(user, cancellationToken);
            stores.Add(StoreTypes.LoginStore);

            await TokenStore.RemoveUserAsync(user, cancellationToken);
            stores.Add(StoreTypes.TokenStore);

            result = await LockoutStore.DeleteUserAsync(user, cancellationToken);
            stores.Add(StoreTypes.NameStore);
            await AssertSingle(user, result, cancellationToken, stores);

            result = await NameStore.DeleteUserAsync(user, cancellationToken);
            stores.Add(StoreTypes.NameStore);
            await AssertSingle(user, result, cancellationToken, stores);

            result = await PasswordStore.DeleteUserAsync(user, cancellationToken);
            stores.Add(StoreTypes.PasswordStore);
            await AssertSingle(user, result, cancellationToken, stores);

            result = await UserStore.DeleteUserAsync(user, cancellationToken);
            stores.Add(StoreTypes.UserStore);
            await AssertSingle(user, result, cancellationToken, stores);
            
            return AuthenticationResult.Success();
        }
    }
}
