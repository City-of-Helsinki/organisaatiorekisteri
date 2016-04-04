"use strict";

module OrganizationRegister
{
    export class OrganizationVisitingAddressCommand
    {
        constructor(public qualifiers: Array<LocalizedText>, public postalCode: string, public streetAddresses: Array<LocalizedText>, public postalDistricts: Array<LocalizedText>)
        {
        }
    }
}