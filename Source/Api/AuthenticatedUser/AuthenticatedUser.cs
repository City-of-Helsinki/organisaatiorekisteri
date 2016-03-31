using System;
using System.Collections.Generic;
using Affecto.AuthenticationServer.Claims;

namespace OrganizationRegister.Api.AuthenticatedUser
{
    public class AuthenticatedUser : Affecto.AuthenticationServer.Claims.AuthenticatedUser
    {
        public Guid OrganizationId { get; private set; }

        public AuthenticatedUser(Guid id, string name, string accountName, Guid organizationId, IEnumerable<string> permissions = null, IEnumerable<string> roles = null,
            IEnumerable<CustomProperty> customProperties = null)
            : base(id, name, accountName, permissions, roles, customProperties)
        {
            if (organizationId == Guid.Empty)
            {
                throw new ArgumentException("Organization id must be provided.", "organizationId");
            }

            OrganizationId = organizationId;
        }
    }
}