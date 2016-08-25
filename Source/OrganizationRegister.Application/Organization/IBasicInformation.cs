using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    public interface IBasicInformation: ILocalizedText
    {
        string BusinessId { get; }
        string Oid { get; }
        string Type { get; }
        IEnumerable<LocalizedText> Names { get; }
        IEnumerable<LocalizedText> Descriptions { get; }
        string MunicipalityCode { get; }
     
        DateTime? ValidFrom { get; }
        DateTime? ValidTo { get; }
    }
}