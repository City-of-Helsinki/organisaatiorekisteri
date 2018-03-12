"use strict";

module OrganizationRegister
{
    export class OrganizationTreeController implements Affecto.Base.IController 
    {
        public static $inject = ["$scope", "$routeParams", "$location", "organizationService", "busyIndicationService", "authenticationService"];

        public model: TreeSearch;

     
        public selectedOrganizationId: string;
        public isEditModeEnabled: boolean;
        public treeOptions: any;
        public canViewAllOrganizations: boolean;
        public rootOrganizationId: string;
        public user: AuthenticatedUser;
        public searchTerm: string;


        constructor(private $scope: Affecto.Base.IViewScope, $routeParams: IOrganizationRoute, private $location: angular.ILocationService,
            private organizationService: OrganizationService, private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService, authenticationService: Affecto.Login.IAuthenticationService)
        {
            
            $scope.controller = this;
            $scope.model = this.model;

            var user: AuthenticatedUser = authenticationService.getUser<AuthenticatedUser>();
            if (user.hasPermission(Permission.maintenanceOfAllOrganizationData))
            {
                this.canViewAllOrganizations = true;
            }
            else if (user.hasPermission(Permission.maintenanceOfOwnOrganizationData))
            {
                this.canViewAllOrganizations = false;
                this.rootOrganizationId = user.organizationId;
            }
           
            this.setSelectedOrganizationId($routeParams);
            this.createTreeOptions();
            this.retrieveOrganizationsAndExpandAllNodes();
            this.isEditModeEnabled = false;

            this.searchTerm = this.organizationService.getSearchTerm();
            if (this.searchTerm !== "")
            {
                this.search();
            }

           
            
        }

       
        
        public navigateToOrganization(orgId: string, selected: boolean): void
        {
            if(selected)
            {
                this.$location.path("/Organizations/" + orgId);
            }
        }

        public createSubOrganization($event: any, node: any): void
        {
            $event.stopPropagation();
            this.$location.path("/Organizations/" + node.id + "/Organizations");
        }


        public get canEdit(): boolean
        {

            return !this.isEditModeEnabled && this.model != null && this.model.tree.value != null && this.model.tree.value.length > 0;
        }

        public enableEditMode(): void
        {
            this.isEditModeEnabled = true;
        }

        public disableEditMode(): void
        {
            this.isEditModeEnabled = false;
        }

        private createTreeOptions(): void
        {
            this.treeOptions = {
                templateUrl: "organizationTreeTemplate.html",
                isOrgTree: true,
                selectedNodeId: this.selectedOrganizationId
            }
        }


        public search(): void
        {
            if (this.searchTerm !== "")
            {
                if (this.canViewAllOrganizations)
                {
                    this.organizationService.getOrganizationFlatlist(this.searchTerm)
                        .then((orgs: Array<Hierarchical>) =>
                        {
                            this.model.searchResult = orgs;
                        });
                }
                else
                {
                    this.organizationService
                        .getOrganizationFlatlistForOrganization(this.searchTerm, this.rootOrganizationId)
                        .then((orgs: Array<Hierarchical>) =>
                        {
                            this.model.searchResult = orgs;
                        });
                    }
            }
            

            this.organizationService.setSearchTerm(this.searchTerm);
        }

        public resetSearch(): void
        {
            this.model.searchResult = null;
            this.searchTerm = "";
            this.organizationService.setSearchTerm(this.searchTerm);
        }

        public selectSearchResult(id: string): void
        {
            this.selectedOrganizationId = id;
            this.navigateToOrganization(id,true);
        }

        private setSelectedOrganizationId($routeParams: IOrganizationRoute): void
        {
            if ($routeParams != null)
            {
                this.selectedOrganizationId = $routeParams.organizationId;
            }
        }

        private retrieveOrganizationsAndExpandAllNodes(): void
        {
            this.busyIndicationService.showBusyIndicator("Haetaan organisaatioita...");

            if (this.canViewAllOrganizations)
            {
                this.organizationService.getOrganizationHierarchy()
                    .then((orgs: Tree) =>
                    {
                        this.setOrganizationsAndExpandAllNodes(orgs);
                    });
            }
            else 
            {
                this.organizationService.getOrganizationHierarchyForOrganization(this.rootOrganizationId)
                    .then((orgs: Tree) =>
                    {
                        this.setOrganizationsAndExpandAllNodes(orgs);
                    });
            }
        }

        private setOrganizationsAndExpandAllNodes(orgs: Tree)
        {
            this.model.tree = orgs;
            this.model.tree.expandAll();
            this.busyIndicationService.hideBusyIndicator();
        }
    }
}