using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.Mapping;
using OrganizationRegister.UserManagement.Model;

namespace OrganizationRegister.UserManagement.Mapping
{
    internal class UserMapper : IMapper<IUser, User>
    {

        public User Map(IUser source)
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

            var result = new User
            {
                Id = source.Id,
                RoleId = GetRoleId(source),
                OrganizationId = customProperties.OrganizationId.Value,
                EmailAddress = customProperties.EmailAddress,
                LastName = customProperties.LastName,
                FirstName = customProperties.FirstName,
                PhoneNumber = customProperties.PhoneNumber,
                IsDisabled = source.IsDisabled
            };

            return result;
        }


        private Guid GetRoleId(IUser user)
        {
            if (user.Roles == null)
            {
                throw new ArgumentException("User roles cannot be null.", "user");
            }
            if (user.Roles.Count != 1)
            {
                throw new ArgumentException("User must have a single role.", "user");
            }

            return user.Roles.Single().Id;
        }
    }
}