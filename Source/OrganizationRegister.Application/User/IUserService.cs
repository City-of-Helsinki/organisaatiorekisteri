using System;
using System.Collections.Generic;

namespace OrganizationRegister.Application.User
{
    public interface IUserService
    {
        Guid AddUser(Guid roleId, Guid organizationId, string emailAddress, string password, string lastName, string firstName, string phoneNumber);
        void SetUser(Guid id, Guid roleId, Guid organizationId, string emailAddress, string password, string lastName, string firstName, string phoneNumber);
        IEnumerable<IRole> GetRoles();
        IEnumerable<IUserListItem> GetInternalUsers(Guid organizationId);
        IUser GetUser(Guid userId);
        bool IsExistingUser(string emailAddress);
        bool ValidatePasswordStrength(string password);
        void DeleteUser(Guid userId);

    }
}