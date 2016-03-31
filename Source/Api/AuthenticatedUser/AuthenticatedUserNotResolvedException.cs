using System;

namespace OrganizationRegister.Api.AuthenticatedUser
{
    public class AuthenticatedUserNotResolvedException : Exception
    {
        public AuthenticatedUserNotResolvedException(string message)
            : base(message)
        {
        }

        public AuthenticatedUserNotResolvedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}