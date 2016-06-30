using Authentication.Frame.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Frame
{
    public partial class UserManager<TUser, TClaim, TToken>
    {
        public async Task<AuthenticationResult<string>> CreateUserAsync(TUser user, string username, string password, string email, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(nameof(password)))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(nameof(email)))
                throw new ArgumentNullException(nameof(email));


            Handle(cancellationToken);
            var id = Guid.NewGuid().ToString();
            var result = await UserStore.CreateUserAsync(user, id, username, cancellationToken);



            return AuthenticationResult<string>.Success(id);
        }
    }
}
