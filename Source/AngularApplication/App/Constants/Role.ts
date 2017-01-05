"use strict";

module OrganizationRegister
{
    export class Role
    {
        public static get systemAdmin(): string
        {
            return "Järjestelmän pääkäyttäjä";
        }

        public static get organizationLevelAdmin(): string
        {
            return "Organisaatiotason pääkäyttäjä";
        }
    }
} 