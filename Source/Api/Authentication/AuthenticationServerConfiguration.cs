using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace OrganizationRegister.Api.Authentication
{
    public class AuthenticationServerConfiguration : ConfigurationSection, IAuthenticationServerConfiguration
    {
        private static readonly AuthenticationServerConfiguration SettingsInstance =
            ConfigurationManager.GetSection("authenticationServer") as AuthenticationServerConfiguration;

        public static AuthenticationServerConfiguration Settings
        {
            get { return SettingsInstance; }
        }

        [ConfigurationProperty("useStaticConfiguration", IsRequired = false, DefaultValue = false)]
        public bool UseStaticConfiguration
        {
            get { return (bool) this["useStaticConfiguration"]; }
            set { this["useStaticConfiguration"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public Uri Url
        {
            get { return (Uri) this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("signingCertificateStore", IsRequired = false, DefaultValue = null)]
        public StoreName? SigningCertificateStore
        {
            get { return (StoreName?) this["signingCertificateStore"]; }
            set { this["signingCertificateStore"] = value; }
        }

        [ConfigurationProperty("signingCertificateThumbprint", IsRequired = false, DefaultValue = null)]
        public string SigningCertificateThumbprint
        {
            get { return (string) this["signingCertificateThumbprint"]; }
            set { this["signingCertificateThumbprint"] = value; }
        }

        [ConfigurationProperty("requiredScope", IsRequired = true)]
        public string RequiredScope
        {
            get { return (string) this["requiredScope"]; }
            set { this["requiredScope"] = value; }
        }

        protected override void PostDeserialize()
        {
            if (string.IsNullOrWhiteSpace(RequiredScope))
            {
                throw new ConfigurationErrorsException("RequiredScope is required.");
            }
            if (!(this["url"] is Uri))
            {
                throw new ConfigurationErrorsException("Url is required.");
            }
        }
    }
}