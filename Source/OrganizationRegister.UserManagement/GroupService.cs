using System;
using System.Linq;
using Affecto.Authentication.Claims;
using OrganizationRegister.Common.User;
using OrganizationRegister.UserManagement.Mapping;
using IdentityManagement = Affecto.IdentityManagement.Interfaces;
using IGroupService = OrganizationRegister.Application.User.IGroupService;

namespace OrganizationRegister.UserManagement
{
    internal class GroupService : IGroupService
    {
        private readonly IdentityManagement.IIdentityManagementService identityManagementService;
        private readonly MapperFactory mapperFactory;
        private readonly IAuthenticatedUserContext userContext;
       

        public GroupService(IdentityManagement.IIdentityManagementService identityManagementService, MapperFactory mapperFactory,
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


        public Guid AddGroup(string name)
        {
            CheckManageUsersPermission();

            var group = identityManagementService.CreateGroup(name, name, name);
            return group.Id;
        }

        public Guid? GetGroupId(string name)
        {
            var group = identityManagementService.GetGroups().SingleOrDefault(g => string.Equals(g.Name, name, StringComparison.OrdinalIgnoreCase));
            return @group?.Id;
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