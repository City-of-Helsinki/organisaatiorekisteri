"use strict";

module OrganizationRegister
{
    export class ServiceService
    {
        public static $inject = ["$http", "apiBaseUrl"];

        constructor(private $http: angular.IHttpService, private apiBaseUrl: string)
        {
        }

        public addService(organizationId: string, service: Service): angular.IPromise<string>
        {
            return this.$http.post(this.apiBaseUrl + "serviceregister/organizations/" + organizationId + "/services", service)
                .then((response: angular.IHttpPromiseCallbackArg<string>): string =>
                {
                    return response.data;
                });
        }

        public getServices(organizationId: string): angular.IPromise<Array<ServiceListItem>>
        {
            return this.$http.get(this.apiBaseUrl + "serviceregister/organizations/" + organizationId + "/services")
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<ServiceListItem> =>
                {
                    return ServiceListItemMapper.map(response.data);
                });
        }

        public getService(organizationId: string, serviceId: string): angular.IPromise<Service>
        {
            return this.$http.get(this.apiBaseUrl + "serviceregister/organizations/" + organizationId + "/services/" + serviceId)
                .then((response: angular.IHttpPromiseCallbackArg<any>): Service =>
                {
                    return ServiceMapper.map(response.data);
                });
        }

        public setServiceBasicInformation(organizationId: string, service: Service): angular.IPromise<void>
        {
            return this.$http.put(this.apiBaseUrl + "serviceregister/organizations/" + organizationId + "/services/" + service.id + "/basicinformation", service)
                .then((): void =>
                {
                });
        }

        public setServiceClassification(organizationId: string, service: Service): angular.IPromise<void>
        {
            return this.$http.put(this.apiBaseUrl + "serviceregister/organizations/" + organizationId + "/services/" + service.id + "/classification",
                new ServiceClassificationCommand(service.serviceClasses, service.ontologyTerms.map((term: Class) => term.id), service.targetGroups, service.lifeEvents, service.keywords))
                .then((): void =>
                {
                });
        }
    }
}