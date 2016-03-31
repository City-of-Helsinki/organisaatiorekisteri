using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Classification;

namespace OrganizationRegister.Api.Classification
{
    public class HierarchicalClassMapper : OneWayMapper<IHierarchicalClass, HierarchicalClass>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IHierarchicalClass, HierarchicalClass>();
        }
    }
}