"use strict";

module OrganizationRegister
{
    export class Route
    {
        public static get login(): string
        {
            return "/Login";
        }

        public static get externalLogin(): string
        {
            return "/Login/External";
        }

        public static get frontPage(): string
        {
            return "/";
        }
    }
} 