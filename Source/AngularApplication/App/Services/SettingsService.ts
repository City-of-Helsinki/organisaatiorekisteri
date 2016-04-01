"use strict";

module OrganizationRegister
{
    export class SettingsService
    {
        public static $inject = ["$http", "apiBaseUrl"];

        constructor(private $http: angular.IHttpService, private apiBaseUrl: string)
        {
        }

        public getOrganizationTypes(): angular.IPromise<Array<string>>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/organizationtypes", { cache: true })
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<string> =>
                {
                    return response.data;
                });
        }

        public getUserRoles(): angular.IPromise<Array<UserRole>>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/roles", { cache: true })
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<UserRole> =>
                {
                    return UserRoleMapper.map(response.data);
                });
        }

        public getWebPageTypes(): angular.IPromise<Array<string>>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/webpagetypes", { cache: true })
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<string> =>
                {
                    return response.data;
                });
        }
    }
}
 