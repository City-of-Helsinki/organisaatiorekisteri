using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Authentication.Claims;
using Affecto.Authentication.Passwords;
using Affecto.Authentication.Passwords.Specifications;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.Mapping;
using Affecto.Patterns.Specification;
using Microsoft.AspNet.Identity;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;
using OrganizationRegister.Common.User;
using OrganizationRegister.UserManagement.Mapping;
using IdentityManagement = Affecto.IdentityManagement.Interfaces;
using IRole = OrganizationRegister.Application.User.IRole;
using IUser = OrganizationRegister.Application.User.IUser;
using IUserListItem = OrganizationRegister.Application.User.IUserListItem;
using IUserService = OrganizationRegister.Application.User.IUserService;

namespace OrganizationRegister.UserManagement
{
    internal class UserService : IUserService
    {
        private readonly IdentityManagement.IIdentityManagementService identityManagementService;
        private readonly MapperFactory mapperFactory;
        private readonly IAuthenticatedUserContext userContext;
        private readonly IOrganizationService organizationService;

        public UserService(IdentityManagement.IIdentityManagementService identityManagementService, IOrganizationService organizationService, MapperFactory mapperFactory,
            IAuthenticatedUserContext userContext)
        {
            if (identityManagementService == null)
            {
                throw new ArgumentNullException(nameof(identityManagementService));
            }
            if (organizationService == null)
            {
                throw new ArgumentNullException(nameof(organizationService));
            }
            if (mapperFactory == null)
            {
                throw new ArgumentNullException(nameof(mapperFactory));
            }
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.identityManagementService = identityManagementService;
            this.organizationService = organizationService;
            this.mapperFactory = mapperFactory;
            this.userContext = userContext;
        }

        public IEnumerable<IRole> GetRoles()
        {
            CheckManageUsersPermission();

            var roleMapper = mapperFactory.CreateRoleMapper();
            return roleMapper.Map(identityManagementService.GetRoles());
        }

        public bool IsExistingUser(string emailAddress)
        {
            CheckManageUsersPermission();

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentException("Email address cannot be empty.", nameof(emailAddress));
            }

            return identityManagementService.IsExistingUserAccount(emailAddress, IdentityManagement.Model.AccountType.Password);
        }

        public bool ValidatePasswordStrength(string password)
        {
            CheckManageUsersPermission();

            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            var passwordObject = new Password(password);

            var lengthSpecification = new PasswordMinimumLengthSpecification(10);
            var characterSpecification = new PasswordCharacterSpecification(3);

            return lengthSpecification.And(characterSpecification).IsSatisfiedBy(passwordObject);
        }

        public Guid AddUser(Guid roleId, Guid organizationId, string emailAddress, string password, string lastName, string firstName, string phoneNumber)
        {
            CheckManageUsersOfOrganizationPermission(organizationId);
            CheckManageUsersInRolePermission(roleId);

            if (roleId == Guid.Empty)
            {
                throw new ArgumentException("User's role id cannot be empty.", nameof(roleId));
            }
            if (organizationId == Guid.Empty)
            {
                throw new ArgumentException("User's organization id cannot be empty.", nameof(organizationId));
            }
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentException("User's email address cannot be empty.", nameof(emailAddress));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("User's password cannot be empty.", nameof(password));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("User's last name cannot be empty.", nameof(lastName));
            }
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("User's first name cannot be empty.", nameof(firstName));
            }

            if (IsExistingUser(emailAddress))
            {
                throw new ExistingUserAccountException($"User account '{emailAddress}' already exists.");
            }

            string name = $"{lastName} {firstName}";

            var customProperties = new CustomProperties
            {
                OrganizationId = organizationId,
                LastName = lastName,
                FirstName = firstName,
                EmailAddress = emailAddress,
                PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber
            };

            IdentityManagement.Model.IUserListItem user = identityManagementService.CreateUser(name, customProperties.ToKeyValuePairs());
            identityManagementService.AddUserAccount(user.Id, emailAddress, password);
            identityManagementService.AddUserRole(user.Id, roleId);

            return user.Id;
        }



        public void SetUser(Guid id, Guid roleId, Guid organizationId, string emailAddress, string password, string lastName, string firstName, string phoneNumber)
        {

            var user = GetUser(id);
            CheckManageUsersOfOrganizationPermission(user.OrganizationId);
            CheckManageUsersInRolePermission(roleId);

            if (id == Guid.Empty)
            {
                throw new ArgumentException("User's id cannot be empty.", nameof(id));
            }

            if (roleId == Guid.Empty)
            {
                throw new ArgumentException("User's role id cannot be empty.", nameof(roleId));
            }

            if (organizationId == Guid.Empty)
            {
                throw new ArgumentException("User's organization id cannot be empty.", nameof(organizationId));
            }

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentException("User's email address cannot be empty.", nameof(emailAddress));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("User's last name cannot be empty.", nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("User's first name cannot be empty.", nameof(firstName));
            }

            if (!IsExistingUser(emailAddress))
            {
                throw new ExistingUserAccountException($"User account '{emailAddress}' not exists.");
            }

            string name = $"{lastName} {firstName}";
            var customProperties = new CustomProperties
            {
                OrganizationId = organizationId,
                LastName = lastName,
                FirstName = firstName,
                EmailAddress = emailAddress,
                PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber
            };
            identityManagementService.UpdateUser(id, name, false, customProperties.ToKeyValuePairs());

            if (roleId != user.RoleId)
            {
                identityManagementService.RemoveUserRole(id, user.RoleId);
                identityManagementService.AddUserRole(id, roleId);
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                identityManagementService.ChangeUserPassword(id, password);
            }


        }


        public IEnumerable<IUserListItem> GetInternalUsers(Guid organizationId)
        {
            CheckManageUsersOfOrganizationPermission(organizationId);

            string organizationIdString = OrganizationId.Convert(organizationId);
            IEnumerable<IdentityManagement.Model.IUser> users = identityManagementService.GetUsers(CustomPropertyName.OrganizationId.ToString(),
                organizationIdString, IdentityManagement.Model.AccountType.Password);

            var mapper = mapperFactory.CreateUserMapper();
            return mapper.Map(users).ToList();
        }

        public IUser GetUser(Guid userId)
        {

            IdentityManagement.Model.IUser user = identityManagementService.GetUser(userId);
            var mapper = mapperFactory.CreateInternalUserMapper();
            var mappedUser =  mapper.Map(user);

            CheckManageUsersOfOrganizationPermission(mappedUser.OrganizationId);

            return mappedUser;
        }

        public void  DeleteUser(Guid userId)
        {

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("User's id cannot be empty.", nameof(userId));
            }

            var user = GetUser(userId);

            CheckManageUsersOfOrganizationPermission(user.OrganizationId);

            string name = $"{user.LastName} {user.FirstName}";
            identityManagementService.UpdateUser(userId, name, true);

        }

        private void CheckManageUsersOfOrganizationPermission(Guid organizationId)
        {
            Guid userOrganizationId = userContext.GetUserOrganizationId();
            var userOrgs = organizationService.GetCompleteOrganizationHierarchyForOrganization(userOrganizationId).Flatten(o => o.SubOrganizations);
            userContext.CheckPermission(userOrgs.Any(o => o.Id == organizationId)
                ? Permissions.Users.MaintenanceOfOwnOrganizationUsers : Permissions.Users.MaintenanceOfAllUsers);
        }

        private void CheckManageUsersInRolePermission(Guid roleId)
        {
            IEnumerable<IdentityManagement.Model.IRole> roles = identityManagementService.GetRoles();
            IdentityManagement.Model.IRole newRole = roles.SingleOrDefault(r => r.Id == roleId);

            if (newRole == null)
            {
                throw new ArgumentException($"No roles found with id '{roleId}'.");
            }

            if (newRole.Name == Roles.Administrator)
            {
                userContext.CheckPermission(Permissions.Users.MaintenanceOfAllUsers);
            }
            else
            {
                userContext.CheckPermission(Permissions.Users.MaintenanceOfOwnOrganizationUsers);
            }
        }

        private void CheckManageUsersPermission()
        {
            if (!userContext.HasPermission(Permissions.Users.MaintenanceOfAllUsers) && !userContext.HasPermission(Permissions.Users.MaintenanceOfOwnOrganizationUsers))
            {
                throw new InsufficientPermissionsException($"{Permissions.Users.MaintenanceOfAllUsers} or {Permissions.Users.MaintenanceOfOwnOrganizationUsers}");
            }
        }
    }
}