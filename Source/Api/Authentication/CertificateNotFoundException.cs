using System;

namespace OrganizationRegister.Api.Authentication
{
    public class CertificateNotFoundException : Exception
    {
        public CertificateNotFoundException(string message)
            : base(message)
        {
        }
    }
}