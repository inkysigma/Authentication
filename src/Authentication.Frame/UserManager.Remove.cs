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
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(cancellationToken, stores.ToArray());
                return AuthenticationResult.ServerFault();
            }

            result = await ClaimStore.DeleteUserAsync(user, cancellationToken);
            stores.Add(StoreTypes.ClaimStore);
            if (!result.Succeeded)
            {
                await Rollback(cancellationToken, stores.ToArray());
                return AuthenticationResult.ServerFault();
            }
            
            return AuthenticationResult.Success();
        }
    }
}
