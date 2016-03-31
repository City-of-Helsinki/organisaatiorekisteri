"use strict";

module OrganizationRegister
{
    export interface IOrganizationRoute extends angular.route.IRouteParamsService
    {
        organizationId: string;
        parentOrganizationId: string;
    }
}