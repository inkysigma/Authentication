using Authentication.Frame.Security;
using System.Collections.Generic;

namespace Authentication.Frame.Configuration
{
    public class SecurityConfiguration<TUser, TClaim>
    {
        public IEnumerable<TClaim> DefaultClaims { get; set; }
        public ISecureRandomProvider RandomProvider { get; set; }
        public IPasswordHasher PasswordHasher { get; set; }
        public IUserTokenProvider<TUser> TokenProvider { get; set; }
    }
}
