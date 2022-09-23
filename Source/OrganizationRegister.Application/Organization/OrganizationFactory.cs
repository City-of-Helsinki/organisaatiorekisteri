using System;
using System.Collections.Generic;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    public class OrganizationFactory
    {
        public static IOrganization CreateOrganization(Guid id)
        {
            Organization organization = new Organization(id);
            return organization;
        }

        public static IOrganization CreateOrganization(Guid id, long numericId, string businessId, string oid, string ptvId, string type, IEnumerable<LocalizedText> names, 
            IEnumerable<LocalizedText> descriptions, int? municipalityCode, DateTime? validFrom, DateTime? validTo, string phoneNumber, string callChargeType, IEnumerable<LocalizedText> phoneCallChargeInfos, string emailAddress, 
            IEnumerable<WebPage> webPages, IEnumerable<LocalizedText> visitingStreetAddresses, string visitingAddressPostalCode, IEnumerable<LocalizedText> visitingAddressLocalities,
            IEnumerable<LocalizedText> visitingAddressQualifier, IEnumerable<LocalizedText> postalStreetAddresses, string postalStreetAddressPostalCode, 
            IEnumerable<LocalizedText> postalStreetAddressLocalities, string postalAddressPostOfficeBox, string postalPostOfficeBoxAddressPostalCode,
            IEnumerable<LocalizedText> postalPostOfficeBoxAddressLocalities, bool useVisitingAddressAsPostalAddress, bool isSubOrganization, IEnumerable<string> languageCodes, 
            IEnumerable<LocalizedText> homepageUrls, IEnumerable<LocalizedText> nameAbbreviations, bool canBeTransferredToFsc, bool canBeResponsibleDeptForService, IEnumerable<AuthorizationGroup> authorizationGroups)
        {
            Organization organization = isSubOrganization ? new SubOrganization(id, numericId, businessId, oid, type, municipalityCode, names, languageCodes, canBeTransferredToFsc, canBeResponsibleDeptForService) :
                new Organization(id, numericId, businessId, oid, type, municipalityCode, names, languageCodes, canBeTransferredToFsc, canBeResponsibleDeptForService);
            organization.Descriptions = descriptions;
            organization.SetValidity(validFrom, validTo);
            organization.EmailAddress = emailAddress;
            organization.WebPages = webPages;
            organization.VisitingAddressQualifiers = visitingAddressQualifier;
            organization.SetCallInformation(phoneNumber, callChargeType, phoneCallChargeInfos);
            organization.SetVisitingAddress(visitingStreetAddresses, visitingAddressPostalCode, visitingAddressLocalities);
            organization.SetPostalAddress(useVisitingAddressAsPostalAddress, postalStreetAddresses, postalStreetAddressPostalCode, postalStreetAddressLocalities,
                postalAddressPostOfficeBox, postalPostOfficeBoxAddressPostalCode, postalPostOfficeBoxAddressLocalities);
            organization.HomepageUrls = homepageUrls;
            organization.NameAbbreviations = nameAbbreviations;
            organization.AuthorizationGroups = authorizationGroups;

            organization.PTVId = ptvId;

            return organization;
        }

        public static IHierarchicalOrganization CreateHierarchicalOrganization(Guid id, IEnumerable<LocalizedText> names, Guid? parentId, DateTime? validFrom, DateTime? validTo)
        {
            return new HierarchicalOrganization(id, names, parentId, validFrom, validTo);
        }

        public static IOrganizationName CreateOrganizationName(Guid id, IEnumerable<LocalizedText> names)
        {
            return new OrganizationName(id, names);
        }

        public static IOrganizationListItem CreateOrganizationListItem(Guid id, Guid? parentId, IEnumerable<LocalizedText> names, string type, bool canBeTransferredToFsc, bool canBeResponsibleDeptForService)
        {
            return new OrganizationListItem(id, parentId, names, type, canBeTransferredToFsc, canBeResponsibleDeptForService);
        }
    }
}