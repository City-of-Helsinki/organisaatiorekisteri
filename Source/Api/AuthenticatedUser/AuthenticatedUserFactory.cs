using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Affecto.AuthenticationServer.Claims;
using OrganizationRegister.Common.User;

namespace OrganizationRegister.Api.AuthenticatedUser
{
    internal class AuthenticatedUserFactory : AuthenticatedUserFactory<AuthenticatedUser>
    {
        public AuthenticatedUserFactory(IEnumerable<Claim> claims)
            : base(claims)
        {
        }

        protected override AuthenticatedUser CreateInstance(Guid id, string name, string accountName, IEnumerable<string> permissions = null, IEnumerable<string> roles = null,
            IEnumerable<CustomProperty> customProperties = null, IEnumerable<Guid> groups = null, IEnumerable<string> organizations = null)
        {
            if (customProperties == null)
            {
                throw new ArgumentException("Custom properties must be provided.");
            }

            List<CustomProperty> propertyList = customProperties.ToList();
            List<CustomProperty> organizationIdProperties = propertyList.Where(c => c.Name == CustomPropertyName.OrganizationId.ToString()).ToList();

            if (organizationIdProperties.Count == 0)
            {
                throw new ArgumentException("Organization id custom property must be provided.");
            }
            if (organizationIdProperties.Count > 1)
            {
                throw new ArgumentException("More than one organization id custom properties provided.");
            }

            Guid organizationId = OrganizationId.Convert(organizationIdProperties[0].Value);
            return new AuthenticatedUser(id, name, accountName, organizationId, permissions, roles, propertyList);
        }
    }
}