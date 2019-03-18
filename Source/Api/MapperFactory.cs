using Affecto.Mapping;
using Microsoft.AspNet.Identity;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Api.Settings;
using OrganizationRegister.Api.User;
using OrganizationRegister.Api.Validation;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Application.User;
using OrganizationRegister.Application.Validation;
using IRole = OrganizationRegister.Application.User.IRole;
using IUser = OrganizationRegister.Application.User.IUser;

namespace OrganizationRegister.Api
{
    public class MapperFactory
    {
        public virtual IMapper<IHierarchicalOrganization, HierarchicalOrganization> CreateHierarchicalOrganizationMapper()
        {
            return new HierarchicalOrganizationMapper();
        }

        public virtual IMapper<IOrganizationName, OrganizationName> CreateOrganizationNameMapper()
        {
            return new OrganizationNameMapper();
        }

        public virtual IMapper<IOrganizationListItem, OrganizationListItem> CreateOrganizationListItemMapper()
        {
            return new OrganizationListItemMapper();
        }

        public virtual IMapper<IOrganizationListContainer, OrganizationListContainer> CreateOrganizationListContainerMapper()
        {
            return new OrganizationListContainerMapper();
        }

        public virtual IMapper<IBusinessIdentifierValidationResult, BusinessIdentifierValidationResult> CreateBusinessIdentifierValidationResultMapper()
        {
            return new BusinessIdentifierValidationResultMapper();
        }

        public virtual IMapper<IOrganization, Organization.Organization> CreateOrganizationMapper()
        {
            return new OrganizationMapper();
        }

        public virtual IMapper<IRole, Role> CreateRoleMapper()
        {
            return new RoleMapper();
        }

        public virtual IMapper<IUserListItem, UserListItem> CreateUserListItemMapper()
        {
            return new UserListItemMapper(CreateRoleMapper());
        }

        public virtual IMapper<IUser, User.User> CreateUserMapper()
        {
            return new UserMapper();
        }
    }
}