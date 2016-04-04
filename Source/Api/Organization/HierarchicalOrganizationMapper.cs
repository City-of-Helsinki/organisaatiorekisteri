using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Organization
{
    internal class HierarchicalOrganizationMapper : OneWayMapper<IHierarchicalOrganization, HierarchicalOrganization>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IHierarchicalOrganization, HierarchicalOrganization>();
        }
    }
}