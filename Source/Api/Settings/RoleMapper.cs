using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.Api.Settings
{
    public class RoleMapper : OneWayMapper<IRole, Role>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IRole, Role>();
        }
    }
}