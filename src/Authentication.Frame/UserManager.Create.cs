using Authentication.Frame.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TLogin>
    {
        /// <summary>
        /// Creates a new user. If the user already exists, it will return an Error.
        /// If the user modifies multiple rows, it is destro
        /// </summary>
        /// <param name="user">The user to be added.</param>
        /// <param name="name">The name to be associated.</param>
        /// <param name="username">The username to be associated</param>
        /// <param name="password">The unhashed password to be associated</param>
        /// <param name="email">The email to be associated</param>
        /// <returns>An AuthenticationResult</returns>
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
            if (queryResult.RowsModified != 1) {
                await Rollback(cancellationToken, a.ToArray());
                return AuthenticationResult<string>.ServerFault();
            }

            var qUser = queryResult.Result;

            // Add user to email store
            var result = await EmailStore.CreateUserAsync(qUser, email, cancellationToken);
            a.Add(StoreTypes.EmailStore);
            await AssertSingle(id, username, email, result, cancellationToken, a);

            //Add user to password store
            var salt = await SecurityConfiguration.RandomProvider.GenerateRandomAsync(cancellationToken);
            var hashed = await SecurityConfiguration.PasswordHasher.HashPassword(password, salt, cancellationToken);
            result = await PasswordStore.CreateUserAsync(qUser, hashed, salt, cancellationToken);
            a.Add(StoreTypes.PasswordStore);
            await AssertSingle(id, username, email, result, cancellationToken, a);

            // Start adding the email and configure it to be activated with a link.
            var guid = Guid.NewGuid().ToString();
            var token = Convert.ToBase64String(
                SecurityConfiguration.TokenProvider.CreateToken(qUser, guid, Security.TokenField.Activation));
            
            result = await EmailStore.CreateUserAsync(qUser, email, cancellationToken);
            a.Add(StoreTypes.EmailStore);
            await AssertSingle(id, username, email, result, cancellationToken, a);

            result = await TokenStore.CreateTokenAsync(qUser, token, DateTime.Now, cancellationToken);
            a.Add(StoreTypes.TokenStore);
            await AssertSingle(id, username, email, result, cancellationToken, a);

            // Email the user with the result
            await EmailConfiguration.AccountVerificationTemplate.LoadAsync(new
            {
                Email = email,
                Token = id
            });
            await EmailConfiguration.EmailProvider.Email(EmailConfiguration.AccountVerificationTemplate, email, cancellationToken);

            // Add a lockout field
            result = await LockoutStore.CreateUserAsync(qUser, cancellationToken);
            a.Add(StoreTypes.LockoutStore);
            await AssertSingle(qUser, result, cancellationToken, a);

            // Add a potential claims field
            result = await ClaimStore.CreateClaimsAsync(qUser, SecurityConfiguration.DefaultClaims, cancellationToken);
            a.Add(StoreTypes.ClaimStore);
            if (result != SecurityConfiguration.DefaultClaims.Count())
            {
                await Rollback(cancellationToken, a.ToArray());
                Logger.LogWarning($"User with username: {username} and email: {email} and guid: {guid} failed.");
                throw new ServerFaultException("Too many rows were modified");
            }

            // Add the user name
            result = await NameStore.CreateUserAsync(qUser, name, cancellationToken);
            a.Add(StoreTypes.NameStore);
            await AssertSingle(id, username, email, result, cancellationToken, a);

            await Commit(cancellationToken, a.ToArray());
            return AuthenticationResult<string>.Success(id);
        }
    }
}
