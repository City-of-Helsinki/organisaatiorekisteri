using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganizationService
    {
        Guid AddOrganization(string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions, 
            DateTime? validFrom, DateTime? validTo);
        Guid AddSubOrganization(Guid parentOrganizationId, string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names, 
            IEnumerable<LocalizedText> descriptions, DateTime? validFrom, DateTime? validTo);
        IEnumerable<IHierarchicalOrganization> GetActiveOrganizationHierarchy();
        IEnumerable<IOrganizationName> GetActiveOrganizations();
        IEnumerable<IOrganizationName> GetActiveMainOrganizations();
        IEnumerable<IOrganizationName> GetMainOrganizations();
        IOrganization GetOrganization(Guid organizationId);
        void SetOrganizationBasicInformation(Guid organizationId, string businessId, string oid, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions, 
            string type, string municipalityCode, DateTime? validFrom, DateTime? validTo);
        void SetOrganizationContactInformation(Guid organizationId, string phoneNumber, string phoneCallFee, string emailAddress, IEnumerable<WebPage> webSites, IEnumerable<LocalizedText> homepgeUrls);
        void SetOrganizationVisitingAddress(Guid organizationId, IEnumerable<LocalizedText> streetAddresses, string postalCode, IEnumerable<LocalizedText> postalDistricts, 
            IEnumerable<LocalizedText> qualifiers);
        void SetOrganizationPostalAddresses(Guid organizationId, bool useVisitingAddress, IEnumerable<LocalizedText> streetAddresses, string streetAddressPostalCode, 
            IEnumerable<LocalizedText> streetAddressPostalDistricts, string postOfficeBox, string postOfficeBoxAddressPostalCode, 
            IEnumerable<LocalizedText> postOfficeBoxAddressPostalDistricts);
        void RemoveOrganization(Guid organizationId);
        void DeactivateOrganization(Guid organizationId);
        IOrganizationName GetOrganizationName(Guid organizationId);
    }
}