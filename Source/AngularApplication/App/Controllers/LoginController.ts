"use strict";

module OrganizationRegister
{
    export class LoginController implements Affecto.Base.IController
    {
        public static $inject = ["$scope", "$location", "authenticationService", "busyIndicationService"];
        public emailAddress: string;
        public password: string;
        public loginFailed: boolean;
        
        private requestedRoute: string;

        constructor(private $scope: Affecto.Base.IViewScope, private $location: angular.ILocationService, private authenticationService: Affecto.Login.IAuthenticationService,
            private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService)
        {
            $scope.controller = this;
            this.loginFailed = false;
            this.requestedRoute = this.$location.search()[UrlParameter.requestedRoute];
        }

        public login(): void
        {            
            this.busyIndicationService.showBusyIndicator("Kirjaudutaan sisään...");

            this.authenticationService
                .logInWithCredentials(this.emailAddress, this.password)
                .then(this.onLogInCompleted, this.onLogInError);
        }

        private checkPermission(): void
        {
            var user: AuthenticatedUser = this.authenticationService.getUser<AuthenticatedUser>();
            if (!user.hasPermission(Permission.maintenanceOfUsers) && !user.hasPermission(Permission.maintenanceOfOrganizationData))
                {
                    this.authenticationService.logOut();
                    this.$location.path(Affecto.ExceptionHandling.Routes.error)
                        .search("code", ErrorCode.insufficientPermissions);
                }
        }


        private onLogInCompleted = (): void=>
        {
            this.checkPermission();
            this.loginFailed = false;

            if (this.requestedRoute && this.requestedRoute !== Route.login)
            {
                this.$location.search(UrlParameter.requestedRoute, null);
                this.$location.path(this.requestedRoute);
            }
            else
            {
                this.$location.path(Route.frontPage);
            }
            
        }

        private onLogInError = (): void=>
        {
            this.loginFailed = true;
            this.password = "";
            this.busyIndicationService.hideBusyIndicator();
        }
    }
}