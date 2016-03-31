using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Api.Classification;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Service;

namespace OrganizationRegister.Api.Service
{
    public class ServiceMapper : OneWayMapper<IService, Service>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IService, Service>();
            Mapper.CreateMap<IHierarchicalClass, HierarchicalClass>();
            Mapper.CreateMap<IClass, Class>();
        }
    }
}