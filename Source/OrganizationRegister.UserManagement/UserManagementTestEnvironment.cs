using System;
using Affecto.IdentityManagement.Commanding;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.UserManagement
{
    public class UserManagementTestEnvironment
    {
        private static readonly Guid AdministratorRoleId = Guid.Parse("B5A20242-A695-4E24-9173-88AF3A90D2E9");
        private static readonly Guid OrganizationAdministratorRoleId = Guid.Parse("70089FF9-548F-4A45-854C-ED9F56B13EA6");
        private static readonly Guid BasicUserRoleId = Guid.Parse("0CCF78EA-10EA-4B00-A16B-9776C77CA46F");

        private readonly Lazy<IDbRepository> repository;
        private readonly Lazy<IUserService> userService;

        public UserManagementTestEnvironment(Lazy<IDbRepository> repository, Lazy<IUserService> userService)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            this.repository = repository;
            this.userService = userService;
        }

        public Guid AddBasicTestUser(Guid organizationId, string emailAddress, string password, string lastName, string firstName)
        {
            return userService.Value.AddUser(BasicUserRoleId, organizationId, emailAddress, password, lastName, firstName, null);
        }

        public Guid AddOrganizationAdministratorTestUser(Guid organizationId, string emailAddress, string password, string lastName, string firstName)
        {
            return userService.Value.AddUser(OrganizationAdministratorRoleId, organizationId, emailAddress, password, lastName, firstName, null);
        }

        public Guid AddAdministratorTestUser(Guid organizationId, string emailAddress, string password, string lastName, string firstName)
        {
            return userService.Value.AddUser(AdministratorRoleId, organizationId, emailAddress, password, lastName, firstName, null);
        }

        public void RemoveAllUsers()
        {
            repository.Value.RemoveUsers();
            repository.Value.SaveChanges();
        }
    }
}