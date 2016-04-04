using System;
using IdentityServer3.AccessTokenValidation;

namespace OrganizationRegister.Api.Authentication
{
    internal class IdentityServerOptionsFactory
    {
        public static IdentityServerBearerTokenAuthenticationOptions Create(IAuthenticationServerConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var options = new IdentityServerBearerTokenAuthenticationOptions
            {
                ValidationMode = ValidationMode.Local,
                Authority = configuration.Url.ToString(),
                RequiredScopes = new[] { configuration.RequiredScope }
            };

            if (configuration.UseStaticConfiguration && configuration.SigningCertificateStore != null)
            {
                var certificate = new Certificate(configuration.SigningCertificateStore.Value, configuration.SigningCertificateThumbprint);
                options.SigningCertificate = certificate.Load();
                options.IssuerName = configuration.Url.ToString();
            }

            return options;
        }
    }
}