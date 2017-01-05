"use strict";

module OrganizationRegister
{
    export class OrganizationService
    {
        public static $inject = ["$http", "apiBaseUrl"];

        constructor(private $http: angular.IHttpService, private apiBaseUrl: string)
        {
        }

        public addOrganization(organization: Organization, parentOrganizationId?: string): angular.IPromise<string>
        {
            if (parentOrganizationId == null)
            {
                return this.$http.post(this.apiBaseUrl + "organizationregister/organizations", organization)
                    .then((response: angular.IHttpPromiseCallbackArg<string>): string =>
                    {
                        return response.data;
                    });                
            }

            return this.$http.post(this.apiBaseUrl + "organizationregister/organizations/" + parentOrganizationId + "/organizations", organization)
                .then((response: angular.IHttpPromiseCallbackArg<string>): string =>
                {
                    return response.data;
                });                
        }

        public getOrganizationHierarchy(): angular.IPromise<Tree>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/organizationhierarchy")
                .then((response: angular.IHttpPromiseCallbackArg<any>): Tree =>
                {
                    return new Tree(HierarchicalOrganizationMapper.map(response.data));
                });
        }

        public getOrganizationHierarchyForOrganization(id: string): angular.IPromise<Tree>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/organizationhierarchyfororganization/" + id)
                .then((response: angular.IHttpPromiseCallbackArg<any>): Tree =>
                {
                    return new Tree(HierarchicalOrganizationMapper.map(response.data));
                });
        }

        public getOrganization(id: string): angular.IPromise<Organization>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/organizations/" + id)
                .then((response: angular.IHttpPromiseCallbackArg<any>): Organization =>
                {
                    return OrganizationMapper.map(response.data);
                });
        }

        public getMainOrganizations(): angular.IPromise<Array<OrganizationName>>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/mainorganizations", true)
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<OrganizationName> =>
                {
                    return OrganizationNameMapper.map(response.data);
                });
        }

        public getOrganizations(): angular.IPromise<Array<OrganizationName>>
        {
            return this.$http.get(this.apiBaseUrl + "organizationregister/organizations", true)
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<OrganizationName> =>
                {
                    return OrganizationNameMapper.map(response.data);
                });
        }

        public setOrganizationContactInformation(organization: Organization): angular.IPromise<void>
        {
            return this.$http.put(this.apiBaseUrl + "organizationregister/organizations/" + organization.id + "/contactinformation", organization)
                .then((): void =>
                {
                });
        }

        public setOrganizationBasicInformation(organization: Organization): angular.IPromise<void>
        {
            return this.$http.put(this.apiBaseUrl + "organizationregister/organizations/" + organization.id + "/basicinformation", organization)
                .then((): void =>
                {
                });
        }

        public setOrganizationVisitingAddress(organization: Organization): angular.IPromise<void>
        {
            return this.$http.put(this.apiBaseUrl + "organizationregister/organizations/" + organization.id + "/visitingaddress",
                new OrganizationVisitingAddressCommand(organization.visitingAddressQualifiers, organization.visitingAddress.postalCode, organization.visitingAddress.streetAddresses,
                    organization.visitingAddress.postalDistricts))
                .then((): void =>
                {
                });
        }

        public setOrganizationPostalAddress(organization: Organization): angular.IPromise<void>
        {
            return this.$http.put(this.apiBaseUrl + "organizationregister/organizations/" + organization.id + "/postaladdresses",
                new OrganizationPostalAddressesCommand(organization.postalStreetAddress.postalCode, organization.postalStreetAddress.streetAddresses,
                    organization.postalStreetAddress.postalDistricts, organization.postalPostOfficeBoxAddress.postOfficeBox, organization.postalPostOfficeBoxAddress.postalCode,
                    organization.postalPostOfficeBoxAddress.postalDistricts, organization.useVisitingAddressAsPostalAddress))
                .then((): void =>
                {
                });
        }

        public deactivateOrganization(id: string): angular.IPromise<void>
        {
            return this.$http.put(this.apiBaseUrl + "organizationregister/organizations/" + id + "/deactivate", null)
                .then((): void =>
                {
                });
        }
    }
}