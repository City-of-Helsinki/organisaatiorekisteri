"use strict";

module OrganizationRegister
{
    export class OrganizationIdNotFoundException extends Affecto.Base.Exception
    {
        protected getExceptionName(): string
        {
            return "OrganizationIdNotFoundException";
        }
    }
}