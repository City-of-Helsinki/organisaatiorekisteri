using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Authentication.Claims;
using Affecto.Authentication.Passwords;
using Affecto.Authentication.Passwords.Specifications;
using Affecto.Mapping;
using Affecto.Patterns.Specification;
using OrganizationRegister.Application.User;
using OrganizationRegister.Common.User;
using OrganizationRegister.UserManagement.Mapping;
using IdentityManagement = Affecto.IdentityManagement.Interfaces;
using IUserService = OrganizationRegister.Application.User.IUserService;

namespace OrganizationRegister.UserManagement
{
    internal class UserService : IUserService
    {
        private readonly IdentityManagement.IIdentityManagementService identityManagementService;
        private readonly MapperFactory mapperFactory;
        private readonly IAuthenticatedUserContext userContext;

        public UserService(IdentityManagement.IIdentityManagementService identityManagementService, MapperFactory mapperFactory,
            IAuthenticatedUserContext userContext)
        {
            if (identityManagementService == null)
            {
                throw new ArgumentNullException(nameof(identityManagementService));
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
            this.mapperFactory = mapperFactory;
            this.userContext = userContext;
        }

        public IEnumerable<IRole> GetRoles()
        {
            var roleMapper = mapperFactory.CreateRoleMapper();
            return roleMapper.Map(identityManagementService.GetRoles());
        }

        public bool IsExistingUser(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentException("Email address cannot be empty.", nameof(emailAddress));
            }

            return identityManagementService.IsExistingUserAccount(emailAddress, IdentityManagement.Model.AccountType.Password);
        }

        public bool ValidatePasswordStrength(string password)
        {
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

            CheckManageUsersOfOrganization(organizationId);
            CheckManageUsersInRole(roleId);

            if (IsExistingUser(emailAddress))
            {
                throw new ExistingUserAccountException($"User account '{emailAddress}' already exists.");
            }

            string displayName = $"{lastName} {firstName}";

            var customProperties = new CustomProperties
            {
                OrganizationId = organizationId,
                LastName = lastName,
                FirstName = firstName,
                EmailAddress = emailAddress,
                PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber
            };

            IdentityManagement.Model.IUserListItem user = identityManagementService.CreateUser(displayName, customProperties.ToKeyValuePairs());
            identityManagementService.AddUserAccount(user.Id, emailAddress, password);
            identityManagementService.AddUserRole(user.Id, roleId);

            return user.Id;
        }

        public IEnumerable<IUserListItem> GetInternalUsers(Guid organizationId)
        {
            CheckManageUsersOfOrganization(organizationId);

            string organizationIdString = OrganizationId.Convert(organizationId);
            IEnumerable<IdentityManagement.Model.IUser> users = identityManagementService.GetUsers(CustomPropertyName.OrganizationId.ToString(),
                organizationIdString, IdentityManagement.Model.AccountType.Password);

            var mapper = mapperFactory.CreateUserMapper();
            return mapper.Map(users).ToList();
        }

        private void CheckManageUsersOfOrganization(Guid organizationId)
        {
            Guid userOrganizationId = userContext.GetUserOrganizationId();
            if (organizationId == userOrganizationId)
            {
                userContext.CheckPermission(Permissions.Users.MaintenanceOfOwnOrganizationUsers);
            }
            else
            {
                userContext.CheckPermission(Permissions.Users.MaintenanceOfAllUsers);
            }
        }

        private void CheckManageUsersInRole(Guid roleId)
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
    }
}