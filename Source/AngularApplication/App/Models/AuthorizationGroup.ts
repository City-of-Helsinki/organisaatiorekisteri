"use strict";

module OrganizationRegister
{
    //export class AuthorizationGroup implements Affecto.Base.IModel
    //{
    //    constructor(public name: string, public role: UserRole)
    //    {
    //    }
    //}


    export class AuthorizationGroup implements Affecto.Base.IModel {
        constructor(public name: string, public roleId: string, public roleName: string) {
        }
    }

    
}
 