using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Validation;

namespace OrganizationRegister.Api.Validation
{
    internal class BusinessIdentifierValidationResultMapper : OneWayMapper<IBusinessIdentifierValidationResult, BusinessIdentifierValidationResult>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<IBusinessIdentifierValidationResult, BusinessIdentifierValidationResult>();
        }
    }
}