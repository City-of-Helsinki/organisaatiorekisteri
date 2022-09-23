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
        IEnumerable<LocalizedText> Names { get;  }
        IEnumerable<LocalizedText> Descriptions { get; }
        IEnumerable<LocalizedText> NameAbbreviations { get; }
        string MunicipalityCode { get; }
     
        DateTime? ValidFrom { get; }
        DateTime? ValidTo { get; }

        bool CanBeTransferredToFsc { get; set; }
        bool CanBeResponsibleDeptForService { get; set; }

        string PTVId { get; set; }
    }
}