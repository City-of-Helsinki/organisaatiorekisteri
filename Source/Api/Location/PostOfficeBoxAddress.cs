using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Location
{
    public class PostOfficeBoxAddress
    {
        public string PostOfficeBox { get; set; }
        public string PostalCode { get; set; }
        public IEnumerable<LocalizedText> PostalDistricts { get; set; }
    }
}