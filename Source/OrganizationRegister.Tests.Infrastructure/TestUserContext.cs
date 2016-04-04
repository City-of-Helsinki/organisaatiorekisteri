using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Authentication.Claims;

namespace OrganizationRegister.Tests.Infrastructure
{
    public class TestUserContext : IAuthenticatedUserContext
    {
        public TestUserContext()
        {
            Permissions = new List<string>(0);
            CustomProperties = new List<KeyValuePair<string, string>>(0);
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSystemUser { get; set; }
        public ICollection<string> Permissions { get; set; }
        public ICollection<KeyValuePair<string, string>> CustomProperties { get; set; }

        public bool HasPermission(string permission)
        {
            return Permissions.Contains(permission);
        }

        public bool IsInGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        public void CheckPermission(string permission)
        {
            if (!HasPermission(permission))
            {
                throw new InsufficientPermissionsException(permission);
            }
        }

        public bool HasCustomProperty(string customPropertyName)
        {
            return CustomProperties.Any(c => c.Key == customPropertyName);
        }

        public string GetCustomPropertyValue(string customPropertyName)
        {
            return CustomProperties.Where(c => c.Key == customPropertyName).Select(c => c.Value).SingleOrDefault();
        }
    }
}