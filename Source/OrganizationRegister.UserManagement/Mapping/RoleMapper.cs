using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.UserManagement.Model;

namespace OrganizationRegister.UserManagement.Mapping
{
    internal class RoleMapper : OneWayMapper<IRole, Role>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IRole, Role>();
        }
    }
}