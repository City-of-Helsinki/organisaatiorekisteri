using System.Collections.Generic;

namespace OrganizationRegister.Application.Settings
{
    public interface ISettingsService
    {
        IEnumerable<string> GetOrganizationTypes();
        IEnumerable<ILanguage> GetServiceLanguages();
        IEnumerable<string> GetWebPageTypes();
    }
}
