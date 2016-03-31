using Affecto.Mapping.AutoMapper;
using AutoMapper;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Settings
{
    internal class LanguageMapper : OneWayMapper<ILanguage, Language>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<ILanguage, Language>();
        }
    }
}