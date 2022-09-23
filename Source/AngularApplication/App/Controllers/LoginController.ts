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

        public loginTunnistamo(): void
        {
            console.log("loginTunnistamo");
            this.onLogInError;

            var loginTunnistamoReturn = this.loginTunnistamoReturn();

            const request = new XMLHttpRequest();
            request.open("Post", "https://localhost:44300/v1/Tunnistamo/LoginUrl",true);
            request.onload = function () {
                console.log("Success, request.status: " + request.status + ", " + request.responseText);
                //document.location.href = 'https://oauth.mocklab.io/oauth/authorize';
                loginTunnistamoReturn;
               
            };
            request.onerror = function () {
                alert("Error, request.status: " + request.status);
            };
            request.send("");
          
            console.log("loginTunnistamo end");

            //alert();
            //this.onLogInCompleted();

            //setTimeout(function () {
            
            //}, 5000);

          
        }

        public loginTunnistamoReturn(): void 
        {
            console.log("loginTunnistamoReturn");
            var str = '{"organizationId":"83e74666-0836-4c1d-948a-4b34a8b90301","id":"d7ce8c14-931d-433b-b741-6dd81ce02320","name":"HKI Järjestelmän pääkäyttäjä","accountName":"superadmin@hel.fi","customProperties":[{"name":"OrganizationId","value":"83e74666-0836-4c1d-948a-4b34a8b90301"},{"name":"LastName","value":"HKI"},{"name":"EmailAddress","value":"superadmin@hel.fi"},{"name":"FirstName","value":"Järjestelmän pääkäyttäjä"}],"roles":["Järjestelmän pääkäyttäjä"],"groups":[],"permissions":["MAINTENANCE_OF_OWN_ORGANIZATION_USERS","MAINTENANCE_OF_ALL_USERS","USER_MAINTENANCE","ORGANIZATION_DATA_MAINTENANCE","MAINTENANCE_OF_OWN_ORGANIZATION_DATA","MAINTENANCE_OF_ALL_ORGANIZATION_DATA"],"organizations":[]}';
            //var str = '{"organizationId":"83e74666-0836-4c1d-948a-4b34a8b90301","id":"d7ce8c14-931d-433b-b741-6dd81ce02320","name":"HKI Järjestelmän pääkäyttäjä","accountName":"superadmin@hel.fi","customProperties":[{"name":"OrganizationId","value":"83e74666-0836-4c1d-948a-4b34a8b90301"},{"name":"LastName","value":"HKI"},{"name":"EmailAddress","value":"superadmin@hel.fi"},{"name":"FirstName","value":"Järjestelmän pääkäyttäjä"}],"roles":["Järjestelmän pääkäyttäjä"],"groups":[],"permissions":["MAINTENANCE_OF_ALL_USERS"],"organizations":[]}';

            //this.authenticationService
            //    .tunnistamoLogInWithCode(str)
            //    .then(this.onLogInCompleted, this.onLogInError);

            var json = JSON.parse(str);
            this.authenticationService
                .tunnistamoLogInWithCode(json);


          
        }

        public login(): void
        {
            console.log("login");

            this.busyIndicationService.showBusyIndicator("Kirjaudutaan sisään...");

            this.authenticationService
                .logInWithCredentials(this.emailAddress, this.password)
                .then(this.onLogInCompleted, this.onLogInError);



         
        }

        private hasRequiredPermission(): boolean
        {
            var user: AuthenticatedUser = this.authenticationService.getUser<AuthenticatedUser>();
            return (user.hasPermission(Permission.maintenanceOfUsers) ||
                user.hasPermission(Permission.maintenanceOfOrganizationData));
        }


        private onLogInCompleted = (): void =>
        {
            console.log("onLogInCompleted");

            if (this.hasRequiredPermission())
            {
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
            else
            {
                this.loginFailed = true;
                this.authenticationService.logOut();
                this.$location
                    .path(Affecto.ExceptionHandling.Routes.error)
                    .search(Affecto.ExceptionHandling.UrlParameters.errorCode, ErrorCode.insufficientPermissions);
            }
        }

        private onLogInError = (): void =>
        {
            console.log("onLogInError");
            this.loginFailed = true;
            this.password = "";
            this.busyIndicationService.hideBusyIndicator();
        }
    }
}