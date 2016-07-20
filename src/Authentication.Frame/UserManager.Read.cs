using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Frame.Result;
using System.Threading;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TLogin>
    {
        public async Task<AuthenticationResult<string>> FetchUserKeyAsync(TUser user,
            CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Handle(cancellationToken);
            var result = await UserStore.FetchUserKeyAsync(user, cancellationToken);
            if (result.RowsModified == 0)
                return AuthenticationResult<string>.Error();
            return AuthenticationResult<string>.Success(result.Result);
        }

        /// <summary>
        /// Fetches the user based on given information. It fills in the key required and fills in
        /// other fields that UserStore can supply and can be used to identify.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="canellationToken"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult<TUser>> FetchUserAsync(TUser user, CancellationToken canellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Handle(canellationToken);
            var result = await UserStore.FetchUserAsync(user, canellationToken);
            if (result.RowsModified != 1)
            {
                await Rollback(canellationToken, StoreTypes.UserStore);
                result = await EmailStore.FetchUser(user, canellationToken);
                if (result.RowsModified != 1)
                    return AuthenticationResult<TUser>.Error();
            }
            return AuthenticationResult<TUser>.Success(result.Result);
        }

        public async Task<AuthenticationResult<string>> FetchUserNameAsync(TUser user,
            CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Handle(cancellationToken);
            var query = await FetchUserAsync(user, cancellationToken);
            if (query.Type != AuthenticationType.Success)
                return AuthenticationResult<string>.Error();
            user = query.Result;
            var result = await UserStore.FetchUserNameAsync(user, cancellationToken);
            await AssertSuccess(user, result, cancellationToken, StoreTypes.UserStore);
            return AuthenticationResult<string>.Success(result.Result);
        }

        public async Task<AuthenticationResult<string>> FetchUserEmailAsync(TUser user,
            CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentException(nameof(user));
            Handle(cancellationToken);
            var query = await FetchUserAsync(user, cancellationToken);
            if (query.Type != AuthenticationType.Success)
                return AuthenticationResult<string>.Error();
            user = query.Result;
            var result = await EmailStore.FetchEmailAsync(user, cancellationToken);
            if (result.RowsModified != 1)
            {
                await
            }
        }

        public async Task<AuthenticationResult<string>> FetchUserNam
    }
}
