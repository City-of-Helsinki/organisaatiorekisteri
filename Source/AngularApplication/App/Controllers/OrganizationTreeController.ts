"use strict";

module OrganizationRegister
{
    export class OrganizationTreeController implements Affecto.Base.IController
    {
        public static $inject = ["$scope", "$routeParams", "$location", "organizationService", "busyIndicationService"];

        public model: Tree;
        public selectedOrganizationId: string;
        public isEditModeEnabled: boolean;
        public treeOptions: any;

        constructor(private $scope: Affecto.Base.IViewScope, $routeParams: IOrganizationRoute, private $location: angular.ILocationService,
            private organizationService: OrganizationService, private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService)
        {
            $scope.controller = this;
            $scope.model = this.model;

            this.setSelectedOrganizationId($routeParams);
            this.createTreeOptions();
            this.retrieveOrganizationsAndExpandAllNodes();
            this.isEditModeEnabled = false;
        }

        public navigateToOrganization(orgId: string, selected: boolean): void
        {
            if (selected)
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
            return !this.isEditModeEnabled && this.model != null && this.model.value != null && this.model.value.length > 0;
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
            this.organizationService.getOrganizationHierarchy()
                .then((orgs: Tree) =>
                {
                    this.model = orgs;
                    this.model.expandAll();
                    this.busyIndicationService.hideBusyIndicator();
                });
        }
    }
}