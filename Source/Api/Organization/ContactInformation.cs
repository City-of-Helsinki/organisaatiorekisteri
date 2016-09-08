using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Organization
{
    public class ContactInformation
    {
        public string PhoneNumber { get; set; }
        public string PhoneCallChargeType { get; set; }
        public IEnumerable<LocalizedText> PhoneCallChargeInfos { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<WebPage> WebPages { get; set; }
        public IEnumerable<LocalizedText> HomepageUrls { get; set; }
    }
}