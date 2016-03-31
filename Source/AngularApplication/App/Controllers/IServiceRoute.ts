"use strict";

module OrganizationRegister
{
    export interface IServiceRoute extends angular.route.IRouteParamsService
    {
        organizationId: string;
        serviceId: string;
    }
}