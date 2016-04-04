using System.Collections.Generic;

namespace OrganizationRegister.Application.Settings
{
    public interface ISettingsRepository
    {
        IReadOnlyCollection<string> GetOrganizationTypeNames();
        IReadOnlyCollection<string> GetDataLanguageCodes();
        IReadOnlyCollection<string> GetWebPageTypes();
    }
}
