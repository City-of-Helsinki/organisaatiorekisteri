using System;

namespace OrganizationRegister.Common.User
{
    public class RequiredCustomPropertyMissingException : Exception
    {
        public RequiredCustomPropertyMissingException(string message) : base(message)
        {
        }
    }
}