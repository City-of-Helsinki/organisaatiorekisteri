"use strict";

module OrganizationRegister
{
    export class BusinessIdentifierValidationResult
    {
        constructor(public isValid: boolean, public reasonForInvalidity: string)
        {
        }
    }
}