using System;
using System.Configuration;

namespace OrganizationRegister.AngularApplication.Configuration
{
    public class OrganizationRegisterApiConfiguration : ConfigurationSection
    {
        private static readonly OrganizationRegisterApiConfiguration SettingsInstance =
            ConfigurationManager.GetSection("organizationRegisterApi") as OrganizationRegisterApiConfiguration;

        public static OrganizationRegisterApiConfiguration Settings
        {
            get { return SettingsInstance; }
        }

        [ConfigurationProperty("baseUrl", IsRequired = true)]
        public Uri BaseUrl
        {
            get { return (Uri) this["baseUrl"]; }
            set { this["baseUrl"] = value; }
        }

        [ConfigurationProperty("maxOntologyTermSearchResults")]
        public int MaxOntologyTermSearchResults
        {
            get { return (int)this["maxOntologyTermSearchResults"]; }
            set { this["maxOntologyTermSearchResults"] = value; }
        }

        protected override void PostDeserialize()
        {
            if (!(this["baseUrl"] is Uri))
            {
                throw new ConfigurationErrorsException("Base url is required.");
            }
        }
    }
}