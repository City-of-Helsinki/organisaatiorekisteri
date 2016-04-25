"use strict";

module OrganizationRegister
{
    export interface IExternalLogInRoute extends angular.route.IRouteParamsService
    {
        access_token: string;
    }
}