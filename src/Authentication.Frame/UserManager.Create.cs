using Authentication.Frame.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Frame.Configuration;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TLogin>
    {
        public async Task<AuthenticationResult<string>> CreateUserAsync(TUser user, string name, string username, 
            string password, string email, CancellationToken cancellationToken)
        {
            // Handle any cancellation
            Handle(cancellationToken);

            // Quick validation of the parameters supplied. Validation of empty strings should be done above as well.
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(nameof(password)))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(nameof(email)))
                throw new ArgumentNullException(nameof(email));
            
            // Validates the parameters
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

            // Create the id of the new user
            var id = Guid.NewGuid().ToString();

            // All stores that have been modified
            var a = new List<StoreTypes>();

            // Add user in database and retrieve the resulting user
            var queryResult = await UserStore.CreateUserAsync(user, id, username, DateTime.Now, cancellationToken);
            a.Add(StoreTypes.UserStore);
            if (!queryResult.Succeeded || queryResult.RowsModified != 1) {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }
            var qUser = queryResult.Result;

            // Add user to email store
            var result = await EmailStore.CreateUserAsync(qUser, email, cancellationToken);
            a.Add(StoreTypes.EmailStore);
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }

            //Add user to password store
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
            var token = Convert.ToBase64String(
                SecurityConfiguration.TokenProvider.CreateToken(qUser, guid, Security.TokenField.Activation));
            
            result = await EmailStore.CreateUserAsync(qUser, email, cancellationToken);
            a.Add(StoreTypes.EmailStore);
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }

            result = await TokenStore.CreateTokenAsync(qUser, token, cancellationToken);
            a.Add(StoreTypes.TokenStore);
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }

            // Email the user with the result
            await EmailConfiguration.AccountVerificationTemplate.LoadAsync(email, token);
            await EmailConfiguration.EmailProvider.Email(EmailConfiguration.AccountVerificationTemplate, email, cancellationToken);

            // Add a lockout field
            result = await LockoutStore.CreateUserAsync(qUser, cancellationToken);
            a.Add(StoreTypes.LockoutStore);
            if (!result.Succeeded || result.RowsModified != 1)
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }

            // Add a potential claims field
            result = await ClaimStore.CreateClaimsAsync(qUser, SecurityConfiguration.DefaultClaims, cancellationToken);
            a.Add(StoreTypes.ClaimStore);
            if (!result.Succeeded || result.RowsModified != SecurityConfiguration.DefaultClaims.Count())
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }

            result = await NameStore.CreateUserAsync(qUser, name, cancellationToken);
            a.Add(StoreTypes.NameStore);
            if (!result.Succeeded || result.RowsModified != SecurityConfiguration.DefaultClaims.Count())
            {
                await Rollback(a);
                return AuthenticationResult<string>.ServerFault();
            }

            await Commit(a);
            return AuthenticationResult<string>.Success(id);
        }
    }
}
