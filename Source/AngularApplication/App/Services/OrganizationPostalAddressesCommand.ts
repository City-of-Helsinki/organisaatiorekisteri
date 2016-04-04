"use strict";

module OrganizationRegister
{
    export class OrganizationPostalAddressesCommand
    {
        constructor(public streetAddressPostalCode: string, public streetAddresses: Array<LocalizedText>, public streetAddressPostalDistricts: Array<LocalizedText>,
            public postOfficeBox: string, public postOfficeBoxAddressPostalCode: string, public postOfficeBoxAddressPostalDistricts: Array<LocalizedText>,
            public useVisitingAddress: boolean)
        {
        }
    }
}