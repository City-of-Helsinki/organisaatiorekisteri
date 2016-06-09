"use strict";

module OrganizationRegister
{
    export class UserSearchController implements Affecto.Base.IController
    {
        public static $inject = ["$scope", "$location", "$routeParams", "userService", "busyIndicationService", "organizationService", "authenticationService", "$q"];

        public model: Array<UserListItem>;
        public organizations: Array<OrganizationName>;
        public canViewAllUsers: boolean;

        public organizationId: string;
        public userCount: number;

        constructor(private $scope: Affecto.Base.IViewScope, private $location: angular.ILocationService, $routeParams: IUserRoute,  private userService: UserService,
            private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService, private organizationService: OrganizationService,
            private authenticationService: Affecto.Login.IAuthenticationService, private $q: angular.IQService)
        {
            $scope.controller = this;
            $scope.model = this.model;

            var user: AuthenticatedUser = authenticationService.getUser<AuthenticatedUser>();

            if (user.hasPermission(Permission.maintenanceOfAllUsers))
            {
                this.canViewAllUsers = true;
            }
            else if (user.hasPermission(Permission.maintenanceOfOwnOrganizationUsers))
            {
                this.canViewAllUsers = false;
            }
            else
            {
                this.$location.path(Affecto.ExceptionHandling.Routes.error).search("code", ErrorCode.insufficientPermissions);
            }

            this.retrieveUsersAndOrganizatioNames($routeParams);
        }

        public retrieveUsers(): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Haetaan käyttäjiä...");
            return this.userService.getUsers(this.organizationId)
                .then((users: Array<UserListItem>) =>
                {
                    this.setUsers(users);
                    this.busyIndicationService.hideBusyIndicator();
                });            
        }

        private retrieveUsersAndOrganizatioNames($routeParams: IUserRoute): angular.IPromise<void>
        {
            this.organizationId = $routeParams.organizationId;
            this.busyIndicationService.showBusyIndicator("Haetaan käyttäjiä...");
            return this.$q.all([this.organizationService.getMainOrganizations(), this.userService.getUsers(this.organizationId)])
                .then((result: Array<any>) =>
                {
                    this.organizations = result[0];
                    this.setUsers(result[1]);
                    this.busyIndicationService.hideBusyIndicator();
                });
        }

        private setUsers(users: Array<UserListItem>): void
        {
            this.model = users;
            this.userCount = this.model.length;            
        }
    }
}  