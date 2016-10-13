using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Organization
{
    internal class HierarchicalOrganizationMapper : OneWayMapper<IHierarchicalOrganization, HierarchicalOrganization>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IHierarchicalOrganization, HierarchicalOrganization>()
                .ForMember(target => target.ValidFrom, source => source.MapFrom(org => org.ValidFrom.HasValue ? org.ValidFrom.Value.ToString("yyyy-MM-dd") : null))
                .ForMember(target => target.ValidTo, source => source.MapFrom(org => org.ValidTo.HasValue ? org.ValidTo.Value.ToString("yyyy-MM-dd") : null));
        }
    }
}