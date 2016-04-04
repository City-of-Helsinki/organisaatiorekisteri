"use strict";

module OrganizationRegister
{
    export class Permission
    {
        public static get viewAllUsers(): string
        {
            return "VIEW_ALL_USERS";
        }

        public static get viewUserOrganizationUsers(): string
        {
            return "VIEW_USER_ORGANIZATION_USERS";
        }

        public static get userMaintenance(): string
        {
            return "USER_MAINTENANCE";
        }

        public static get manageAdministratorUsers(): string
        {
            return "MANAGE_ADMINISTRATOR_USERS";
        }
    }
} 