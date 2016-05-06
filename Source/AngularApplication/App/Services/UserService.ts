"use strict";

module OrganizationRegister
{
    export class UserService
    {
        public static $inject = ["$http", "apiBaseUrl"];

        constructor(private $http: angular.IHttpService, private apiBaseUrl: string)
        {
        }

        public getUser(userId: string): angular.IPromise<User>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/users/" + userId)
                .then((response: angular.IHttpPromiseCallbackArg<any>): User =>
                {
                    return UserMapper.map(response.data);
                });
        }

        public getUsers(organizationId: string): angular.IPromise<Array<UserListItem>>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/organizations/" + organizationId + "/internalusers")
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<UserListItem> =>
                {
                    return UserListItemMapper.map(response.data);
                });
        }

        public getUserRoles(): angular.IPromise<Array<UserRole>>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/users/userroles", true)
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<UserRole> =>
                {
                    return UserRoleMapper.map(response.data);
                });
        }

        public addUser(user: User): angular.IPromise<string>
        {
            return this.$http.post(this.apiBaseUrl + "organizationregister/users/", user)
                .then((response: angular.IHttpPromiseCallbackArg<any>): string =>
                {
                    return response.data;
                });
        }

        public isExistingUser(emailAddress: string): angular.IPromise<boolean>
        {
            return this.$http.post(this.apiBaseUrl + "organizationregister/users/isexisting", "\"" + emailAddress + "\"")
                .then((response: angular.IHttpPromiseCallbackArg<any>): boolean =>
                {
                    return response.data;
                });
        }

        public validatePasswordStrength(password: string): angular.IPromise<boolean>
        {
            return this.$http.post(this.apiBaseUrl + "organizationregister/users/password", "\"" + password + "\"")
                .then((response: angular.IHttpPromiseCallbackArg<any>): boolean =>
                {
                    return response.data;
                });            
        }
    }
} 