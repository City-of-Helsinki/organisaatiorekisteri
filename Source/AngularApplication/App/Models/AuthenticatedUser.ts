"use strict";

module OrganizationRegister
{
    export class AuthenticatedUser extends Affecto.Login.AuthenticatedUser
    {
        constructor(public name: string, public accountName: string, public roles: Array<string>, public permissions: Array<string>,
            public customProperties: Array<Affecto.Login.CustomProperty>, public organizationId: string)
        {
            super(name, accountName, roles, permissions, customProperties);
        }
    }
}