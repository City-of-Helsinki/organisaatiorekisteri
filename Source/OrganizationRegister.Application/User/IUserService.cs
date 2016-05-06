using System;
using System.Collections.Generic;

namespace OrganizationRegister.Application.User
{
    public interface IUserService
    {
        Guid AddUser(Guid roleId, Guid organizationId, string emailAddress, string password, string lastName, string firstName, string phoneNumber);
        IEnumerable<IRole> GetRoles();
        IEnumerable<IUserListItem> GetInternalUsers(Guid organizationId);
        bool IsExistingUser(string emailAddress);
        bool ValidatePasswordStrength(string password);
    }
}