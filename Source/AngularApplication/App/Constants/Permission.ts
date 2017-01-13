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

        public static get maintenanceOfUsers(): string
        {
            return "USER_MAINTENANCE";
        }

        public static get maintenanceOfOwnOrganizationData(): string
        {
            return "MAINTENANCE_OF_OWN_ORGANIZATION_DATA";
        }

        public static get maintenanceOfAllOrganizationData(): string
        {
            return "MAINTENANCE_OF_ALL_ORGANIZATION_DATA";
        }

        public static get maintenanceOfOrganizationData(): string
        {
            return "ORGANIZATION_DATA_MAINTENANCE";
        }

        
    }
} 