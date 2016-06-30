using Authentication.Frame.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Frame.Configuration
{
    public class SecurityConfiguration<TUser, TClaim, TToken>
    {
        public IEnumerable<TClaim> DefaultClaims { get; set; }
        public ISecureRandomProvider RandomProvider { get; set; }
        public IPasswordHasher PasswordHasher { get; set; }
        public IUserTokenProvider<TUser, TToken> TokenProvider { get; set; }
    }
}
