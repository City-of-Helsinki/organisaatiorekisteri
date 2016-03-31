"use strict";

module OrganizationRegister
{
    export class InvalidBusinessIdentifierReason
    {
        public static get format(): string
        {
            return "INVALID_FORMAT";
        }

        public static get orderNumber(): string
        {
            return "INVALID_ORDER_NUMBER";
        }

        public static get checkSum(): string
        {
            return "INVALID_CHECK_SUM";
        }

        public static get alreadyExists(): string
        {
            return "ALREADY_EXISTS";
        }
    }
}