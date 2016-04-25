"use strict";

module OrganizationRegister
{
    export class ExternalLoginController implements Affecto.Base.IController
    {
        public static $inject = ["$scope", "$location", "authenticationService", "busyIndicationService", "requestedRouteService", "$routeParams"];

        constructor(private $scope: Affecto.Base.IControllerScope, private $location: ng.ILocationService, private authenticationService: Affecto.Login.IAuthenticationService,
            private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService, private routeService: Affecto.Login.RequestedRouteService,
            $routeParams: IExternalLogInRoute)
        {
            $scope.controller = this;
            this.busyIndicationService.showBusyIndicator("Kirjaudutaan sisään...");
            this.authenticationService.logInWithAccessToken($routeParams.access_token)
                .then(this.onLogInCompleted, this.onLogInError);
        }

        private onLogInCompleted = (): void =>
        {
            this.busyIndicationService.hideBusyIndicator();
            var requestedRoute: string = this.routeService.route;
            if (requestedRoute && requestedRoute !== Route.login && requestedRoute !== Route.externalLogin)
            {
                this.$location.path(requestedRoute);
            }
            else
            {
                this.$location.path(Route.frontPage);
            }
        }

        private onLogInError = (): void =>
        {
            this.busyIndicationService.hideBusyIndicator();
            this.$location
                .path(Affecto.ExceptionHandling.Routes.error)
                .search(Affecto.ExceptionHandling.UrlParameters.errorCode, ErrorCode.externalLoginValidationFailed);
        }
    }
}