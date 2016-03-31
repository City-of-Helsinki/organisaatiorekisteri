"use strict";

module OrganizationRegister
{
    export class PostOfficeBoxAddress
    {
        constructor(public postOfficeBox: string, public postalCode: string, public postalDistricts: Array<LocalizedText>)
        {
        }
    }
}
  