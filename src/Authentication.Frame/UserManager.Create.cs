using Authentication.Frame.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TToken>
    {
        public async Task<AuthenticationResult<string>> CreateUserAsync(TUser user, string name, string username, 
            string password, string email, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(nameof(password)))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(nameof(email)))
                throw new ArgumentNullException(nameof(email));
            
            var validation = await ValidationConfiguration.EmailValidator.ValidateAsync(email, cancellationToken);
            if (!validation.Succeeded)
                return AuthenticationResult<string>.Invalidate(validation);
            validation = await ValidationConfiguration.UserNameValidator.ValidateAsync(username, cancellationToken);
            if (!validation.Succeeded)
                return AuthenticationResult<string>.Invalidate(validation);
            validation = await ValidationConfiguration.NameValidator.ValidateAsync(name, cancellationToken);
            if (!validation.Succeeded)
                return AuthenticationResult<string>.Invalidate(validation);
            validation = await ValidationConfiguration.PasswordValidator.ValidateAsync(password, cancellationToken);
            if (!validation.Succeeded)
                return AuthenticationResult<string>.Invalidate(validation);
            validation = await ValidationConfiguration.UserValidator.ValidateAsync(user, cancellationToken);
            if (!validation.Succeeded)
                return AuthenticationResult<string>.Invalidate(validation);

            Handle(cancellationToken);
            var id = Guid.NewGuid().ToString();

            var a = new List<StoreTypes>();
            var queryResult = await UserStore.CreateUserAsync(user, id, username, DateTime.Now, cancellationToken);
            a.Add(StoreTypes.UserStore);
            if (!queryResult.Succeeded || queryResult.RowsModified != 1) {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }
            var qUser = queryResult.Result;
            var result = await EmailStore.CreateUserAsync(qUser, email, cancellationToken);
            a.Add(StoreTypes.EmailStore);
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }
            var salt = await SecurityConfiguration.RandomProvider.GenerateRandomAsync(cancellationToken);
            var hashed = await SecurityConfiguration.PasswordHasher.HashPassword(password, salt, cancellationToken);
            result = await PasswordStore.CreateUserAsync(qUser, hashed, salt, cancellationToken);
            a.Add(StoreTypes.PasswordStore);
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }

            // Start adding the email and configure it to be activated with a link.
            var guid = Guid.NewGuid().ToString();
            var token = SecurityConfiguration.TokenProvider.CreateToken(qUser, guid, Security.TokenField.Activation);

            EmailConfiguration.AccountVerificationTemplate.Load(email, token);

            result = await EmailStore.CreateUserAsync(qUser, email, cancellationToken);
            a.Add(StoreTypes.EmailStore);
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }

            result = await TokenStore.CreateTokenAsync(qUser, token, cancellationToken);
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }

            await EmailConfiguration.EmailProvider.Email(EmailConfiguration.AccountVerificationTemplate, email, cancellationToken);



            return AuthenticationResult<string>.Success(id);
        }
    }
}
