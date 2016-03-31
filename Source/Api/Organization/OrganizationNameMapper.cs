using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Organization
{
    internal class OrganizationNameMapper : OneWayMapper<IOrganizationName, OrganizationName>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IOrganizationName, OrganizationName>();
        }
    }
}