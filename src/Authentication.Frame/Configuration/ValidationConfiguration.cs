namespace Authentication.Frame.Configuration
{
    public class ValidationConfiguration<TUser>
    {
        public IValidator<string> EmailValidator { get; set; }
        public IValidator<string> NameValidator { get; set; }
        public IValidator<string> PasswordValidator { get; set; }
        public IValidator<string> UserNameValidator { get; set; }
        public IValidator<TUser> UserValidator { get; set; }

        internal string Validate()
        {
            if (EmailValidator == null)
                return nameof(EmailValidator);
            if (NameValidator == null)
                return nameof(NameValidator);
            if (PasswordValidator == null)
                return nameof(PasswordValidator);
            if (UserNameValidator == null)
                return nameof(UserNameValidator);
            if (UserValidator == null)
                return nameof(UserValidator);
            return null;
        }
    }
}
