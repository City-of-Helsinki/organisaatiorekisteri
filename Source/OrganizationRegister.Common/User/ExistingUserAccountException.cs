using System;

namespace OrganizationRegister.Common.User
{
    public class ExistingUserAccountException : Exception
    {
        public ExistingUserAccountException(string message) : base(message)
        {
        }
    }
}