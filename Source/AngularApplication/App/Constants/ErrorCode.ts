"use strict";

module OrganizationRegister
{
    export class ErrorCode
    {
        public static get insufficientPermissions(): string
        {
            return "INSUFFICIENT_PERMISSIONS";
        }
    }
} 