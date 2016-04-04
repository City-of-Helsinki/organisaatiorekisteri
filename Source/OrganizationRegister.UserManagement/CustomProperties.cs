using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Interfaces.Model;
using OrganizationRegister.Common.User;

namespace OrganizationRegister.UserManagement
{
    internal class CustomProperties
    {
        private readonly List<ICustomProperty> properties;

        public CustomProperties()
        {
            properties = new List<ICustomProperty>();
        }

        public CustomProperties(IEnumerable<ICustomProperty> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }

            this.properties = properties.ToList();

            ICustomProperty organizationId = this.properties.SingleOrDefault(p => p.Name == CustomPropertyName.OrganizationId.ToString());
            if (organizationId != null && !OrganizationRegister.Common.User.OrganizationId.IsValid(organizationId.Value))
            {
                throw new ArgumentException(string.Format("Custom property '{0}' has an invalid value '{1}'.", organizationId.Name, organizationId.Value), "properties");
            }
        }

        public CustomProperties(IEnumerable<KeyValuePair<string, string>> properties)
            : this((properties != null) ? properties.Select(p => new CustomProperty(p.Key, p.Value)) : null)
        {
        }

        public string LastName
        {
            get { return Get(CustomPropertyName.LastName); }
            set { Set(CustomPropertyName.LastName, value); }
        }

        public string FirstName
        {
            get { return Get(CustomPropertyName.FirstName); }
            set { Set(CustomPropertyName.FirstName, value); }
        }

        public string EmailAddress
        {
            get { return Get(CustomPropertyName.EmailAddress); }
            set { Set(CustomPropertyName.EmailAddress, value); }
        }

        public string PhoneNumber
        {
            get { return Get(CustomPropertyName.PhoneNumber); }
            set { Set(CustomPropertyName.PhoneNumber, value); }
        }

        public Guid? OrganizationId
        {
            get
            {
                Guid result;
                string stringValue = OrganizationIdString;
                if (OrganizationRegister.Common.User.OrganizationId.TryConvert(stringValue, out result))
                {
                    return result;
                }

                return null;
            }
            set
            {
                string stringValue;
                if (value.HasValue && OrganizationRegister.Common.User.OrganizationId.TryConvert(value.Value, out stringValue))
                {
                    Set(CustomPropertyName.OrganizationId, stringValue);
                }
                else
                {
                    Set(CustomPropertyName.OrganizationId, null);
                }
            }
        }

        public string OrganizationIdString
        {
            get
            {
                return Get(CustomPropertyName.OrganizationId);
            }
        }

        public IReadOnlyCollection<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            return properties.Select(p => new KeyValuePair<string, string>(p.Name, p.Value)).ToList();
        }

        public void ValidateRequiredProperties()
        {
            if (OrganizationId == null)
            {
                throw new RequiredCustomPropertyMissingException("Organization id is missing.");
            }
            if (string.IsNullOrWhiteSpace(EmailAddress))
            {
                throw new RequiredCustomPropertyMissingException("E-mail address is missing.");
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                throw new RequiredCustomPropertyMissingException("Last name is missing.");
            }
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                throw new RequiredCustomPropertyMissingException("First name is missing.");
            }
        }

        protected string Get(CustomPropertyName name)
        {
            return properties
                .Where(p => p.Name == name.ToString())
                .Select(p => p.Value)
                .SingleOrDefault();
        }

        protected void Set(CustomPropertyName name, string value)
        {
            properties.RemoveAll(p => p.Name == name.ToString());
            properties.Add(new CustomProperty(name.ToString(), value));
        }

        private class CustomProperty : ICustomProperty
        {
            public CustomProperty(string name, string value)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Custom property name must be defined.", "name");
                }

                Name = name;
                Value = value;
            }

            public string Name { get; private set; }
            public string Value { get; private set; }
        }
    }
}