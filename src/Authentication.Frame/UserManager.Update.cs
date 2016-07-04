using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Authentication.Frame.Result;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim>
    {
        public async Task<AuthenticationResult> RequestUpdatePasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Handle(cancellationToken);
            var id = Convert.ToBase64String(await SecurityConfiguration.RandomProvider.GenerateRandomAsync(cancellationToken));
            var generate = Convert.ToBase64String(SecurityConfiguration.TokenProvider.CreateToken(user, id, Security.TokenField.Password));
            var result = await TokenStore.CreateTokenAsync(qUser, generate, cancellationToken);   
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(cancellationToken, StoreTypes.TokenStore);
                return AuthenticationResult.ServerFault();
            }
            result = await EmailStore.FetchEmailAsync(qUser, generate, cancellationToken);
            await EmailConfiguration.PasswordChangeTemplate.LoadAsync(new
            {
                
            });
            await EmailConfiguration.EmailProvider.Email();

        }

        public async Task<AuthenticationResult> UpdatePasswordAsync(TUser user, string token, CancellationToken cancellationToken)
        {

        }
    }
}
