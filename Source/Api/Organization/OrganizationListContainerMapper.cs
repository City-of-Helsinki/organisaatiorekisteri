using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Organization
{
    internal class OrganizationListContainerMapper : OneWayMapper<IOrganizationListContainer, OrganizationListContainer>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IOrganizationListContainer, OrganizationListContainer>();
            Mapper.CreateMap<IOrganizationListItem, OrganizationListItem>();
        }
    }
}