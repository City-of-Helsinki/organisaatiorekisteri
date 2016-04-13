"use strict";

module OrganizationRegister
{
    export class ErrorCode
    {
        public static get insufficientPermissions(): string
        {
            return "INSUFFICIENT_PERMISSIONS";
        }
        public static get externalLoginValidationFailed(): string
        {
            return "EXTERNAL_LOGIN_VALIDATION_FAILED";
        }
    }
} 