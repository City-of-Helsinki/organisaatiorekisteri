using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    public interface IOrganizationService
    {
        Guid AddOrganization(string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions, 
            DateTime? validFrom, DateTime? validTo, IEnumerable<LocalizedText> nameAbbreviations, bool canBeTransferredToFsc, bool canBeResponsibleDeptForService);
        Guid AddSubOrganization(Guid parentOrganizationId, string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names, 
            IEnumerable<LocalizedText> descriptions, DateTime? validFrom, DateTime? validTo, IEnumerable<LocalizedText> nameAbbreviations, bool canBeTransferredToFsc, bool canBeResponsibleDeptForService);
        IEnumerable<IHierarchicalOrganization> GetOrganizationHierarchy();
        IEnumerable<IHierarchicalOrganization> GetOrganizationHierarchy(bool includeFutureOrganizations);
        IEnumerable<IOrganizationName> GetOrganizations();
        IEnumerable<IHierarchicalOrganization> GetCompleteOrganizationHierarchyForOrganization(Guid rootOrganizationId);
        IEnumerable<IHierarchicalOrganization> GetOrganizationHierarchyForOrganization(Guid? rootOrganizationId, bool includeFutureOrganizations);
        IEnumerable<IOrganizationName> GetMainOrganizations();
        IEnumerable<IOrganizationName> GetMainOrganizationsNames();
        IOrganization GetOrganization(Guid organizationId);
        void SetOrganizationBasicInformation(Guid organizationId, string businessId, string oid, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions, 
            string type, string municipalityCode, DateTime? validFrom, DateTime? validTo, IEnumerable<LocalizedText> nameAbbreviations, bool canBeTransferredToFsc, bool canBeResponsibleDeptForService, string ptvId);
        //void SetOrganizationContactInformation(Guid organizationId, string phoneNumber, string phoneCallFee, string emailAddress, IEnumerable<WebPage> webSites, IEnumerable<LocalizedText> homepgeUrls);

        void SetOrganizationContactInformation(Guid organizationId, string phoneNumber, string callChargeType, IEnumerable<LocalizedText> callChargeInfos,
            string emailAddress, IEnumerable<WebPage> webSites, IEnumerable<LocalizedText> homepageUrls);

        void SetOrganizationVisitingAddress(Guid organizationId, IEnumerable<LocalizedText> streetAddresses, string postalCode, IEnumerable<LocalizedText> postalDistricts, 
            IEnumerable<LocalizedText> qualifiers);
        void SetOrganizationPostalAddresses(Guid organizationId, bool useVisitingAddress, IEnumerable<LocalizedText> streetAddresses, string streetAddressPostalCode, 
            IEnumerable<LocalizedText> streetAddressPostalDistricts, string postOfficeBox, string postOfficeBoxAddressPostalCode, 
            IEnumerable<LocalizedText> postOfficeBoxAddressPostalDistricts);
        void SetOrganizationAuthorizationInformation(Guid organizationId, IEnumerable<AuthorizationGroup> groups);
        void RemoveOrganization(Guid organizationId);
        void DeactivateOrganization(Guid organizationId);
        IOrganizationName GetOrganizationName(Guid organizationId);

        IEnumerable<IHierarchicalOrganization> GetOrganizationsAsFlatlist(string searchTerm, Guid? organizationId);

        IEnumerable<IOrganizationListItem> GetGroupOrganizationsAsFlatlist(string groupIds);


        IEnumerable<IOrganizationListItem> GetOrganizationListForMunicipality(int rootMunicipalityCode);

        IEnumerable<IOrganizationListItem> GetOrganizationListForOrganization(Guid organizationId);

        IEnumerable<IOrganizationListItem> GetOrganizationListForOrganization(string businessId);

        IEnumerable<IOrganizationListItem> GetMainOrganizationList();

        List<AuthorizationGroup> GetAuthorizationGroups();
    

    }
}