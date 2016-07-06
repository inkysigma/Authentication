using System;
using System.Threading.Tasks;
using Authentication.Frame.Result;
using System.Threading;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TLogin>
    {
        public async Task<AuthenticationResult<TUser>> FetchUserAsync(TUser user, CancellationToken canellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Handle(canellationToken);
            var result = await UserStore.FetchUserAsync(user, canellationToken);
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(canellationToken, StoreTypes.UserStore);
                result = await EmailStore.FetchUser(user, canellationToken);
                if (!result.Succeeded || result.RowsModified != 1)
                {
                    await Rollback(canellationToken);
                    return AuthenticationResult<TUser>.ServerFault();
                }
            }
            return AuthenticationResult<TUser>.Success(result.Result);
        }
    }
}
