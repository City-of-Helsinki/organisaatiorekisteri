"use strict";

module OrganizationRegister
{
    export class Permission
    {
        public static get maintenanceOfOwnOrganizationUsers(): string
        {
            return "MAINTENANCE_OF_OWN_ORGANIZATION_USERS";
        }

        public static get maintenanceOfAllUsers(): string
        {
            return "MAINTENANCE_OF_ALL_USERS";
        }
    }
} 