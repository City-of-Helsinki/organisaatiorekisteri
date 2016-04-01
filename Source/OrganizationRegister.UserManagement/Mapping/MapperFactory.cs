using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.Mapping;
using OrganizationRegister.UserManagement.Model;

namespace OrganizationRegister.UserManagement.Mapping
{
    internal class MapperFactory
    {
        public virtual IMapper<IRole, Role> CreateRoleMapper()
        {
            return new RoleMapper();
        }

        public virtual IMapper<IUser, UserListItem> CreateUserMapper()
        {
            return new UserListItemMapper(CreateRoleMapper());
        }
    }
}