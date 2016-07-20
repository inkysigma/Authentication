using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Authentication.Frame.Result;
using Authentication.Frame.Security;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TLogin>
    {
        public async Task<AuthenticationResult> RequestUpdatePasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Handle(cancellationToken);
            var qResult = await FetchUserAsync(user, cancellationToken);
            if (qResult.Type != AuthenticationType.Success)
                return new AuthenticationResult
                {
                    Type = qResult.Type,
                    Validation = qResult.Validation
                };
            var qUser = qResult.Result;
            var id = Guid.NewGuid().ToString();
            var generate = Convert.ToBase64String(SecurityConfiguration.TokenProvider.CreateToken(user, id, Security.TokenField.Password));
            var result = await TokenStore.CreateTokenAsync(qUser, generate, DateTime.Now, cancellationToken);
            await AssertSingle(qUser, result, cancellationToken);
            var emailResult = await EmailStore.FetchEmailAsync(qUser, cancellationToken);
            await AssertSingle(qUser, emailResult, cancellationToken, StoreTypes.EmailStore);

            var email = emailResult.Result;

            await EmailConfiguration.PasswordChangeTemplate.LoadAsync(new
            {
                Email = email,
                Token = id
            });
            await EmailConfiguration.EmailProvider.Email(EmailConfiguration.PasswordChangeTemplate, email, cancellationToken);

            await Commit(cancellationToken, StoreTypes.TokenStore);
            return AuthenticationResult.Success();
        }

        public async Task<AuthenticationResult> UpdatePasswordAsync(TUser user, string password, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            Handle(cancellationToken);

            var validate = await ValidationConfiguration.PasswordValidator.ValidateAsync(password, cancellationToken);
            if (!validate.Succeeded)
                return AuthenticationResult.Invalidate(validate);

            var random = await SecurityConfiguration.RandomProvider.GenerateRandomAsync(cancellationToken);
            var hashed = await SecurityConfiguration.PasswordHasher.HashPassword(password, random, cancellationToken);

            var result = await PasswordStore.SetPasswordAsync(user, hashed, cancellationToken);
            await AssertSingle(user, result, cancellationToken, StoreTypes.PasswordStore);

            result = await PasswordStore.SetSaltAsync(user, random, cancellationToken);
            await AssertSingle(user, result, cancellationToken, StoreTypes.PasswordStore);

            await Commit(cancellationToken, StoreTypes.PasswordStore);
            return AuthenticationResult.Success();
        }

        public async Task<AuthenticationResult<bool>> ValidateUpdateToken(TUser user, string updateToken, TokenField field, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(updateToken))
                throw new ArgumentNullException(nameof(updateToken));
            Handle(cancellationToken);

            var result = await FetchUserAsync(user, cancellationToken);
            if (result.Type != AuthenticationType.Success)
                return AuthenticationResult<bool>.Error();

            var query = await TokenStore.FetchTokenAsync(result.Result, cancellationToken);
            if (query.RowsModified == 0)
                return AuthenticationResult<bool>.Error();

            var hash = Convert.ToBase64String(SecurityConfiguration.TokenProvider.CreateToken(user, updateToken, field));

            if (!query.Result.Contains(hash))
                return AuthenticationResult<bool>.Error();

            await TokenStore.RemoveTokenAsync(user, hash, cancellationToken);

            await Commit(cancellationToken, StoreTypes.TokenStore);
            return AuthenticationResult<bool>.Success(true);
        }

        public async Task<AuthenticationResult>  ActiavteAccount(TUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            Handle(cancellationToken);
            var rows = await UserStore.ActivateAccountAsync(user, cancellationToken);
            if (rows != 1)
            {
                await Rollback(cancellationToken, StoreTypes.UserStore);
                return AuthenticationResult.Error();
            }
            return AuthenticationResult.Success();
        }
    }
}
