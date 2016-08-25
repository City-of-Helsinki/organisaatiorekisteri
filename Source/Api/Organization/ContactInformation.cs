using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Organization
{
    public class ContactInformation
    {
        public string PhoneNumber { get; set; }
        public string PhoneCallFee { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<WebPage> WebPages { get; set; }
        public IEnumerable<LocalizedText> HomepageUrls { get; set; }
    }
}