using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Location
{
    public interface IVisitingAddressQualifiers
    {
        IEnumerable<LocalizedText> VisitingAddressQualifiers { get; }
        string GetVisitingAddressQualifier(string languageCode);
    }
}