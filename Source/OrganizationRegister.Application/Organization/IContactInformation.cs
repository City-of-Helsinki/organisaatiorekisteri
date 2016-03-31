using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    public interface IContactInformation
    {
        string PhoneNumber { get; }
        string PhoneCallFee { get; }
        string EmailAddress { get; }
        IEnumerable<WebPage> WebPages { get; }
    }
}
