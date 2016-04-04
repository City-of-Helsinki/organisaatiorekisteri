"use strict";

module OrganizationRegister
{
    export class MainNavigationController extends Affecto.Login.LoginDrivenController
    {
        public static $inject = ["$scope", "$location", "busyIndicationService", "userService", "organizationService", "authenticationService"];

        public currentSection: CurrentNavigationSection;
        public organizations: Array<OrganizationName>;
        public selectedOrganizationId: string;
        
        constructor($scope: Affecto.Base.IViewScope, private $location: angular.ILocationService, private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService,
            public userService: UserService, private organizationService: OrganizationService, authenticationService: Affecto.Login.IAuthenticationService)
        {
            super($scope, authenticationService);
            $scope.controller = this;

            this.currentSection = CurrentNavigationSection.ServiceInformationSection;
        }

        public logOut()
        {
            this.authenticationService.logOut();
            this.$location.path(Route.login);
        }

        public isAdministrationSectionActive(): boolean
        {
            return this.currentSection === CurrentNavigationSection.AdministrationSection;
        }

        public isServiceInformationSectionActive(): boolean
        {
            return this.currentSection === CurrentNavigationSection.ServiceInformationSection;
        }

        public changeToServiceInformationSection(): void
        {
            this.currentSection = CurrentNavigationSection.ServiceInformationSection;
        }

        public changeToAdministrationSection(): void
        {
            this.currentSection = CurrentNavigationSection.AdministrationSection;
        }

        protected onUserLoggedIn(): void
        {
            this.currentSection = CurrentNavigationSection.ServiceInformationSection;
        }
    }
}