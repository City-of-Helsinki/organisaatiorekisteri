"use strict";

module OrganizationRegister
{
    export class ServiceSearchController implements Affecto.Base.IController
    {
        public static $inject = ["$scope", "$routeParams", "serviceService", "busyIndicationService"];

        public model: Array<ServiceListItem>;
        private organizationId: string;

        constructor(private $scope: Affecto.Base.IViewScope, $routeParams: IServiceRoute, private serviceService: ServiceService,
            private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService)
        {
            $scope.controller = this;
            $scope.model = this.model;
            this.organizationId = $routeParams.organizationId;
            this.retrieveServices();
        }

        private retrieveServices(): void
        {
            this.busyIndicationService.showBusyIndicator("Haetaan palveluita...");
            this.serviceService.getServices(this.organizationId)
                .then((services: Array<ServiceListItem>) =>
                {
                    this.model = services;
                    this.busyIndicationService.hideBusyIndicator();
                });
        }
    }
}  