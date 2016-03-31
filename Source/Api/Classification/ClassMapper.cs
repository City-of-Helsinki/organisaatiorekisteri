using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Classification;

namespace OrganizationRegister.Api.Classification
{
    public class ClassMapper : OneWayMapper<IClass, Class>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IClass, Class>();
        }
    }
}