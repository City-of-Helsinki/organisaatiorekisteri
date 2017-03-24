using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Organization
{
    internal class OrganizationListItemMapper : OneWayMapper<IOrganizationListItem, OrganizationListItem>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IOrganizationListItem, OrganizationListItem>();
        }
    }
}