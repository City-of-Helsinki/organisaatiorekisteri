using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    public interface IBasicInformation
    {
        string BusinessId { get; }
        string Oid { get; }
        string Type { get; }
        IEnumerable<LocalizedText> Names { get; }
        IEnumerable<LocalizedText> Descriptions { get; }
        string MunicipalityCode { get; }
        string GetDescription(string languageCode);
    }
}