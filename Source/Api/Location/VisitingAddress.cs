using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Location
{
    public class VisitingAddress
    {
        public string PostalCode { get; set; }
        public IEnumerable<LocalizedText> StreetAddresses { get; set; }
        public IEnumerable<LocalizedText> PostalDistricts { get; set; }
        public IEnumerable<LocalizedText> Qualifiers { get; set; }
    }
}