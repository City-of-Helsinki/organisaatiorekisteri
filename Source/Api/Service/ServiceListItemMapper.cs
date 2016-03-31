using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Service;

namespace OrganizationRegister.Api.Service
{
    internal class ServiceListItemMapper : OneWayMapper<IServiceListItem, ServiceListItem>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IServiceListItem, ServiceListItem>();
        }
    }
}