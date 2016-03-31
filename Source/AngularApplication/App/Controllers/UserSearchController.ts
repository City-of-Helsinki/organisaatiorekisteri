"use strict";

module OrganizationRegister
{
    export class UserSearchController implements Affecto.Base.IController
    {
        public static $inject = ["$scope", "$location", "$routeParams", "userService", "busyIndicationService", "organizationService", "authenticationService"];

        public model: Array<UserListItem>;
        public organizations: Array<OrganizationName>;
        public canViewAllUsers: boolean;
        public canMaintainUsers: boolean;

        public organizationId: string;
        public userCount: number;

        constructor(private $scope: Affecto.Base.IViewScope, private $location: angular.ILocationService, $routeParams: IUserRoute,  private userService: UserService,
            private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService, private organizationService: OrganizationService,
            private authenticationService: Affecto.Login.IAuthenticationService)
        {
            $scope.controller = this;
            $scope.model = this.model;

            var user: AuthenticatedUser = authenticationService.getUser<AuthenticatedUser>();

            if (user.hasPermission(Permission.viewAllUsers))
            {
                this.canViewAllUsers = true;
            }
            else if (user.hasPermission(Permission.viewUserOrganizationUsers))
            {
                this.canViewAllUsers = false;
            }
            else
            {
                this.$location.path(Affecto.ExceptionHandling.Routes.error).search("code", ErrorCode.insufficientPermissions);
            }

            this.canMaintainUsers = user.hasPermission(Permission.userMaintenance);

            this.initializeSearch($routeParams);
        }

        private retrieveUsers(): void
        {
            this.busyIndicationService.showBusyIndicator("Haetaan käyttäjiä...");
            this.userService.getUsers(this.organizationId)
                .then((users: Array<UserListItem>) =>
                {
                    this.model = users;
                    this.userCount = users.length;
                    this.busyIndicationService.hideBusyIndicator();
                });
        }

        private initializeSearch($routeParams: IUserRoute): angular.IPromise<void>
        {
            this.organizationId = $routeParams.organizationId;
            this.busyIndicationService.showBusyIndicator("Haetaan valintalistojen sisältöä...");
            return this.organizationService.getMainOrganizations()
                .then((orgs: Array<OrganizationName>) =>
                {
                    this.organizations = orgs;
                    this.retrieveUsers();
                });
        }
    }
}  