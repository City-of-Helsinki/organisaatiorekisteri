using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    public interface IContactInformation: ILocalizedText
    {
        string PhoneNumber { get; }
        //string PhoneCallFee { get; }
        IEnumerable<LocalizedText> PhoneCallChargeInfos { get; }
        string PhoneCallChargeType { get; }

        string EmailAddress { get; }



        IEnumerable<WebPage> WebPages { get; }
        IEnumerable<LocalizedText> HomepageUrls { get; }
      

    }
}
