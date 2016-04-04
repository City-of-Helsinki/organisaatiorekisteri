"use strict";

module OrganizationRegister
{
    export class InsufficientPermissionsException extends Affecto.Base.Exception
    {
        protected getExceptionName(): string
        {
            return "InsufficientPermissionsException";
        }
    }
}