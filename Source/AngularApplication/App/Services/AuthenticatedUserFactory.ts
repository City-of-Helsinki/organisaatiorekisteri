"use strict";

module OrganizationRegister
{
    export class AuthenticatedUserFactory implements Affecto.Login.IAuthenticatedUserFactory
    {
        public createUser(jsonData: any): AuthenticatedUser
        {
            return new AuthenticatedUser(jsonData.name, jsonData.accountName, jsonData.roles, jsonData.permissions, jsonData.customProperties, jsonData.organizationId);
        }
    }
}