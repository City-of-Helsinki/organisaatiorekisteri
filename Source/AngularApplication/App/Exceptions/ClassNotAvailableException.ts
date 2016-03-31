"use strict";

module OrganizationRegister
{
    export class ClassNotAvailableException extends Affecto.Base.Exception
    {
        protected getExceptionName(): string
        {
            return "ClassNotAvailableException";
        }
    }
}