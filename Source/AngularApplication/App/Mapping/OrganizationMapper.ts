"use strict";

module OrganizationRegister
{
    export class OrganizationMapper
    {
        public static map(data: any): Organization
        {
            var municipalityCode: number = null;
            if (data.municipalityCode != null && data.municipalityCode !== "")
            {
                municipalityCode = parseInt(data.municipalityCode);
            }

            return new Organization(data.id, data.numericId, data.names, data.businessId, data.descriptions, data.oid, data.type, municipalityCode, data.validFrom, data.validTo, data.phoneNumber,
                data.phoneCallFee, data.emailAddress, data.webPages, data.visitingAddress, data.visitingAddressQualifiers, data.useVisitingAddressAsPostalAddress,
                data.postalStreetAddress, data.postalPostOfficeBoxAddress, data.isSubOrganization);
        }
    }
}  