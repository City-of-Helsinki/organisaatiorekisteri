using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Organization
{
    public class BasicInformation
    {
        public string BusinessId { get; set; }
        public IEnumerable<LocalizedText> Names { get; set; }
        public IEnumerable<LocalizedText> Descriptions { get; set; }
        public string Type { get; set; }
        public string MunicipalityCode { get; set; }
        public string Oid { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}