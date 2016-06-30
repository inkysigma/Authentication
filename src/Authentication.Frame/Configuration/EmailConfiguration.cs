using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Frame.Email;

namespace Authentication.Frame.Configuration
{
    public class EmailConfiguration
    {
        public IEmailProvider EmailProvider { get; set; }

        public IEmailTemplate AccountVerificationTemplate { get; set; }
    }
}
