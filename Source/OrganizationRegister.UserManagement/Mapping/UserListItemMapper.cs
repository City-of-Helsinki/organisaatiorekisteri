using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.Mapping;
using OrganizationRegister.UserManagement.Model;

namespace OrganizationRegister.UserManagement.Mapping
{
    internal class UserListItemMapper : IMapper<IUser, UserListItem>
    {
        private readonly IMapper<IRole, Role> roleMapper;

        public UserListItemMapper(IMapper<IRole, Role> roleMapper)
        {
            if (roleMapper == null)
            {
                throw new ArgumentNullException("roleMapper");
            }
            this.roleMapper = roleMapper;
        }

        public UserListItem Map(IUser source)
        {
            if (source == null)
            {
                return null;
            }

            if (source.CustomProperties == null)
            {
                throw new ArgumentException("User custom properties cannot be null.", "source");
            }

            List<KeyValuePair<string, string>> keyValuePairs = source.CustomProperties
                .Select(c => new KeyValuePair<string, string>(c.Name, c.Value))
                .ToList();

            var customProperties = new CustomProperties(keyValuePairs);
            customProperties.ValidateRequiredProperties();

            var result = new UserListItem
            {
                Id = source.Id,
                Role = GetRole(source),
                OrganizationId = customProperties.OrganizationId.Value,
                EmailAddress = customProperties.EmailAddress,
                LastName = customProperties.LastName,
                FirstName = customProperties.FirstName,
                IsDisabled = source.IsDisabled
            };

            return result;
        }

        private Role GetRole(IUser user)
        {
            if (user.Roles == null)
            {
                throw new ArgumentException("User roles cannot be null.", "user");
            }
            if (user.Roles.Count != 1)
            {
                throw new ArgumentException("User must have a single role.", "user");
            }

            return roleMapper.Map(user.Roles.Single());
        }
    }
}