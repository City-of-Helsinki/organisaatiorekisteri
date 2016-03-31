"use strict";

module OrganizationRegister
{
    export class ClassificationService
    {
        public static $inject = ["$http", "apiBaseUrl", "maxOntologyTermSearchResults"];

        constructor(private $http: ng.IHttpService, private apiBaseUrl: string, private maxOntologyTermSearchResults: number)
        {
        }

        public getLifeEvents(): angular.IPromise<Array<Hierarchical>>
        {
            return this.$http.get(this.apiBaseUrl + "serviceregister/lifeevents", { cache: true })
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<Hierarchical> =>
                {
                    return HierarchicalClassMapper.map(response.data);;
                });
        }

        public getServiceClasses(): angular.IPromise<Array<Hierarchical>>
        {
            return this.$http.get(this.apiBaseUrl + "serviceregister/serviceclasses", { cache: true })
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<Hierarchical> =>
                {
                    return HierarchicalClassMapper.map(response.data);;
                });
        }

        public getOnthologyTerms(): angular.IPromise<Array<Hierarchical>>
        {
            return this.$http.get(this.apiBaseUrl + "serviceregister/ontologyterms", { cache: true })
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<Hierarchical> =>
                {
                    return HierarchicalClassMapper.map(response.data);;
                });
        }

        public getTargetGroups(): angular.IPromise<Array<Hierarchical>>
        {
            return this.$http.get(this.apiBaseUrl + "serviceregister/targetgroups", { cache: true })
                .then((response: angular.IHttpPromiseCallbackArg<any>): Array<Hierarchical> =>
                {
                    return HierarchicalClassMapper.map(response.data);;
                });
        }

        public searchOntologyTerms(searchText: string): angular.IPromise<OntologyTermsSearchResult>
        {
            var getUrl: string = this.apiBaseUrl + "serviceregister/ontologyterms/" + encodeURIComponent(searchText);
            if (this.maxOntologyTermSearchResults > 0)
            {
                getUrl = getUrl + "/?maxResults=" + this.maxOntologyTermSearchResults;
            }
            return this.$http.get(getUrl)
                .then((response: angular.IHttpPromiseCallbackArg<any>): OntologyTermsSearchResult =>
                {
                    return new OntologyTermsSearchResult(ClassMapper.map(response.data.classes), response.data.searchText);
                });
        }
    }
}