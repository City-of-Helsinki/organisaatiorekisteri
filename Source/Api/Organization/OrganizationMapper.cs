using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Organization
{
    internal class OrganizationMapper : OneWayMapper<IOrganization, Organization>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IOrganization, Organization>();
            Mapper.CreateMap<StreetAddress, Location.StreetAddress>();
            Mapper.CreateMap<PostOfficeBoxAddress, Location.PostOfficeBoxAddress>();
        }
    }
}