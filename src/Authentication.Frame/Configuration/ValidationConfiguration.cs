using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Frame.Configuration
{
    public class ValidationConfiguration<TUser>
    {
        public IValidator<string> EmailValidator { get; set; }
        public IValidator<string> NameValidator { get; set; }
        public IValidator<string> PasswordValidator { get; set; }
        public IValidator<string> UserNameValidator { get; set; }
        public IValidator<TUser> UserValidator { get; set; }
    }
}
