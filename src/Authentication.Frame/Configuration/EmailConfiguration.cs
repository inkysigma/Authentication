using Authentication.Frame.Email;

namespace Authentication.Frame.Configuration
{
    public class EmailConfiguration
    {
        public IEmailProvider EmailProvider { get; set; }

        public IEmailTemplate AccountVerificationTemplate { get; set; }

        public IEmailTemplate PasswordChangeTemplate { get; set; }
    }
}
