﻿using System;
using System.Collections.Generic;
using ServiceRegister.Common;

namespace ServiceRegister.Application.Organization
{
    public interface IOrganizationService
    {
        Guid AddOrganization(string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions);
        Guid AddSubOrganization(Guid parentOrganizationId, string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names, 
            IEnumerable<LocalizedText> descriptions);
        IEnumerable<IHierarchicalOrganization> GetActiveOrganizationHierarchy();
        IEnumerable<IOrganizationName> GetActiveOrganizations();
        IEnumerable<IOrganizationName> GetActiveMainOrganizations();
        IEnumerable<IOrganizationName> GetMainOrganizations();
        IOrganization GetOrganization(Guid organizationId);
        void SetOrganizationBasicInformation(Guid organizationId, string businessId, string oid, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions,
            string type, string municipalityCode);
        void SetOrganizationContactInformation(Guid organizationId, string phoneNumber, string phoneCallFee, string emailAddress, IEnumerable<WebPage> webSites);
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