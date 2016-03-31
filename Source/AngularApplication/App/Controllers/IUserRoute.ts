"use strict";

module OrganizationRegister 
{
    export interface IUserRoute extends angular.route.IRouteParamsService 
    {
        id: string;
        organizationId: string;
    }
} 