using System;
using System.Configuration;

namespace OrganizationRegister.AngularApplication.Configuration
{
    public class AccessTokenServiceConfiguration : ConfigurationSection
    {
        private static readonly AccessTokenServiceConfiguration SettingsInstance =
            ConfigurationManager.GetSection("accessTokenService") as AccessTokenServiceConfiguration;

        public static AccessTokenServiceConfiguration Settings
        {
            get { return SettingsInstance; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public Uri Url
        {
            get { return (Uri) this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("clientId", IsRequired = true)]
        public string ClientId
        {
            get { return (string) this["clientId"]; }
            set { this["clientId"] = value; }
        }

        [ConfigurationProperty("clientSecret", IsRequired = true)]
        public string ClientSecret
        {
            get { return (string) this["clientSecret"]; }
            set { this["clientSecret"] = value; }
        }

        [ConfigurationProperty("scope", IsRequired = true)]
        public string Scope
        {
            get { return (string) this["scope"]; }
            set { this["scope"] = value; }
        }

        protected override void PostDeserialize()
        {
            if (string.IsNullOrWhiteSpace(ClientId))
            {
                throw new ConfigurationErrorsException("Client id is required.");
            }
            if (string.IsNullOrWhiteSpace(ClientSecret))
            {
                throw new ConfigurationErrorsException("Client secret is required.");
            }
            if (string.IsNullOrWhiteSpace(Scope))
            {
                throw new ConfigurationErrorsException("Scope is required.");
            }
            if (!(this["url"] is Uri))
            {
                throw new ConfigurationErrorsException("Url is required.");
            }
        }
    }
}