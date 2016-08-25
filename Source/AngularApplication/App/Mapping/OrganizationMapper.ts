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

            return new Organization(data.id, data.numericId, data.names, data.businessId, data.descriptions, data.oid, data.type, municipalityCode, this.createDate(data.validFrom),
                this.createDate(data.validTo), data.phoneNumber, data.phoneCallFee, data.emailAddress, data.webPages, data.visitingAddress, data.visitingAddressQualifiers,
                data.useVisitingAddressAsPostalAddress, data.postalStreetAddress, data.postalPostOfficeBoxAddress, data.homepageUrls, data.isSubOrganization);
        }

        private static createDate(data: any): Date
        {
            var date: Date = null;
            if (data != null && data !== "")
            {
                date = new Date(data);
            }
            return date;
        }
    }
}  