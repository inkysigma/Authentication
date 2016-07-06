namespace Authentication.Frame.Result
{
	public class AuthenticationResult
    {
		public AuthenticationType Type { get; set; }
		public ValidateResult Validation { get; set; }

		public static AuthenticationResult Success()
        {
            return new AuthenticationResult
            {
                Type = AuthenticationType.Success
            };
        }

		public static AuthenticationResult Invalidate(ValidateResult result)
        {
            return new AuthenticationResult
            {
				Type = AuthenticationType.ValidationFailure,
				Validation = result
            };
        }

		public static AuthenticationResult Error()
        {
            return new AuthenticationResult
            {
                Type = AuthenticationType.Error
            };
        }
		
		public static AuthenticationResult ServerFault()
        {
            return new AuthenticationResult
            {
                Type = AuthenticationType.ServerFault
            };
        }
    }

    public class AuthenticationResult<T>
    {
		public AuthenticationType Type { get; set; }
		public ValidateResult Validation { get; set; }
        public T Result { get; set; }

		public static AuthenticationResult<T> Success(T result)
        {
            return new AuthenticationResult<T>
            {
                Type = AuthenticationType.Success,
                Result = result
            };
        }

		public static AuthenticationResult<T> Invalidate(ValidateResult result)
        {
            return new AuthenticationResult<T>
            {
                Type = AuthenticationType.ValidationFailure,
                Validation = result
            };
        }

		public static AuthenticationResult<T> Error()
        {
            return new AuthenticationResult<T>
            {
                Type = AuthenticationType.Error
            };
        }

		public static AuthenticationResult<T> ServerFault()
        {
            return new AuthenticationResult<T>
            {
                Type = AuthenticationType.ServerFault
            };
        }
    }

    public enum AuthenticationType
    {
		/// <summary>
        /// The user was not authorized for the transaction (e.g. the user tried
        /// to redeem an invalid key)
        /// </summary>
        Error,
		/// <summary>
        /// The request completed successfully
        /// </summary>
        Success,
		/// <summary>
        /// The model was validated incorrectly.
        /// </summary>
		ValidationFailure,
		/// <summary>
        /// If the database modifies incorrect number of roles. The user should be prompted
        /// to put in a support ticket, because something went horribly wrong.
        /// </summary>
		ServerFault
    }
}
