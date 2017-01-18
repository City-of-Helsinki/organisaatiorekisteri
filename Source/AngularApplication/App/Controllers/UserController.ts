"use strict";

module OrganizationRegister
{
    export class UserController implements Affecto.Base.IController
    {
        public static $inject = [
            "$scope", "$location", "$routeParams", "$sce", "$q", "userService", "settingsService", "validationService", "busyIndicationService", "organizationService", "authenticationService"
        ];

        public userRoles: Array<UserRole>;
        public validPhoneNumber: boolean;
        public validEmailAddress: boolean;
        public existingEmailAddress: boolean;
        public validPassword: boolean;
        public validConfirmedPassword: boolean;
        public model: User;
        public originalModel: User;
        public userInformationForm: angular.IFormController;
        public canViewAllOrganizations: boolean;
        public rootOrganizationId: string; 
        public organizationHiearchy: Tree;
        public selectedOrganizationName: string;

       
        constructor(private $scope: Affecto.Base.IViewScope,
            private $location: angular.ILocationService,
            $routeParams: IUserRoute,
            private $sce: angular.ISCEService,
            private $q: angular.IQService,
            private userService: UserService,
            private settingsService: SettingsService,
            private validationService: ValidationService,
            private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService,
            private organizationService: OrganizationService,
            private authenticationService: Affecto.Login.IAuthenticationService)
        {

            var user: AuthenticatedUser = authenticationService.getUser<AuthenticatedUser>();
            if (!user.hasPermission(Permission.maintenanceOfOwnOrganizationUsers) &&
                !user.hasPermission(Permission.maintenanceOfAllUsers))
            {
                this.$location.path(Affecto.ExceptionHandling.Routes.error)
                    .search("code", ErrorCode.insufficientPermissions);
            }


            this.initializeUser($routeParams);

            $scope.controller = this;
            $scope.model = this.model;

            this.validPhoneNumber = true;
            this.validEmailAddress = true;
            this.existingEmailAddress = false;
            this.validConfirmedPassword = true;
            this.validPassword = true;

            this.initializeSelectionLists();
        }

        public toggleOrganisationSelection(classId: string, selected: boolean): void
        {
            if (selected)
            {
                this.model.organizationId = classId;
                this.selectedOrganizationName = this.organizationHiearchy.get(this.model.organizationId).name;
            }
            else
            {
                this.model.organizationId = null;
                this.selectedOrganizationName = "";
            }
        }

       
        public canAddUser(): boolean
        {
            return this.validPhoneNumber &&
                this.validEmailAddress &&
                !this.existingEmailAddress &&
                this.validPassword &&
                this.validConfirmedPassword &&
                this.model.hasOrganizationId();
        }

        public canSaveUser(): boolean
        {
            return this.canAddUser() &&
                    this.model.hasId();
        }

        public goToHomePage(): void
        {
            this.$location.path("/Organizations/" + this.model.organizationId + "/Users");
        }

        public validateEmailAddress(): void
        {
            if (this.model.hasEmailAddress())
            {
                this.$q.all([this.validationService.validateEmailAddress(this.model.emailAddress), this.userService.isExistingUser(this.model.emailAddress)])
                    .then((result: Array<any>) =>
                    {
                        this.setEmailAddressValidity(result[0], result[1]);
                    });
            }
            else
            {
                this.setEmailAddressValidity(true, false);
            }
        }

        public validatePhoneNumber(): void
        {
            if (this.model.hasPhoneNumber())
            {
                this.validationService.validatePhoneNumber(this.model.phoneNumber)
                    .then(this.setPhoneNumberValidity);
            }
            else
            {
                this.setPhoneNumberValidity(true);
            }
        }

        public validatePassword(): void
        {
            if (this.model.hasPassword())
            {
                this.userService.validatePasswordStrength(this.model.password)
                    .then((result: boolean) =>
                    {
                        this.setPasswordValidity(result);
                    });
            }
            else
            {
                this.setPasswordValidity(true);
            }
        }

        public validateConfirmedPassword(): void
        {
            if (this.model.hasBothPasswords() && this.validPassword)
            {
                this.setConfirmedPasswordValidity(this.model.password === this.model.passwordConfirm);
            }
            else
            {
                this.setConfirmedPasswordValidity(true);
            }
        }

        public addUser()
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan käyttäjän tietoja...");
            this.originalModel = angular.copy(this.model);

            return this.userService.addUser(this.model)
                .then(() =>
                {
                    this.busyIndicationService.hideBusyIndicator();
                    this.goToHomePage();
                });
        }


        public saveUser()
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan käyttäjän tietoja...");
            this.originalModel = angular.copy(this.model);

            return this.userService.setUser(this.model)
                .then(() =>
                {
                    this.busyIndicationService.hideBusyIndicator();
                    this.goToHomePage();
                });
        }

        public deleteUser(): void
        {
            this.busyIndicationService.showBusyIndicator("Poistetaan käyttäjän tietoja...");
            this.userService.deleteUser(this.model.id)
                .then(() =>
                {
                    this.busyIndicationService.hideBusyIndicator();
                    this.goToHomePage();
                });
        }



        private setEmailAddressValidity = (isValid: boolean, isExisting: boolean): void =>
        {
            this.validEmailAddress = isValid;
            this.existingEmailAddress = isExisting;
            this.setFormFieldValidity("emailAddress", isValid && !isExisting);
        }

        private setPhoneNumberValidity = (isValid: boolean): void =>
        {
            this.validPhoneNumber = isValid;
            this.setFormFieldValidity("phoneNumber", isValid);
        }

        private setConfirmedPasswordValidity = (isValid: boolean): void =>
        {
            this.validConfirmedPassword = isValid;
            this.setFormFieldValidity("passwordConfirm", isValid);
        }

        private setPasswordValidity = (isValid: boolean): void =>
        {
            this.validPassword = isValid;
            this.setFormFieldValidity("password", isValid);
            if (!isValid)
            {
                this.setConfirmedPasswordValidity(true);
            }
        }

        private setFormFieldValidity = (fieldName: string, isValid: boolean): void =>
        {
            if (this.userInformationForm[fieldName] != null)
            {
                this.userInformationForm[fieldName].$setValidity("format", isValid);
            }
        }


        private initializeUser($routeParams: IUserRoute): angular.IPromise<void>
        {

            this.model = new User();

            if ($routeParams.id === undefined || $routeParams.id == null)
            {
                //this.model = new User();
                this.model.organizationId = $routeParams.organizationId;
            }
            else
            {
                this.busyIndicationService.showBusyIndicator("Haetaan käyttäjän tietoja...");
                return this.userService.getUser($routeParams.id)
                    .then((user: User) =>
                    {
                        this.model = user;
                        this.model.organizationId = user.organizationId;
                        this.toggleOrganisationSelection(this.model.organizationId, true);
                        this.busyIndicationService.hideBusyIndicator();
                    });
            }

            this.originalModel = angular.copy(this.model);
        }


        //private initializeUser($routeParams: IUserRoute): angular.IPromise<void>
        //{
        //    if ($routeParams.id === undefined || $routeParams.id == null)
        //    {
        //        this.model = new User();
        //        this.model.organizationId = $routeParams.organizationId;
        //    }
        //    else
        //    {
        //        this.busyIndicationService.showBusyIndicator("Haetaan käyttäjän tietoja...");
        //        return this.userService.getUser($routeParams.id)
        //            .then((user: User) =>
        //            {
        //                this.model = user;
        //                this.busyIndicationService.hideBusyIndicator();
        //            });
        //    }

        //    this.originalModel = angular.copy(this.model);
        //}


        private getOrganizationHierarchy(): angular.IPromise<Tree>
        {
            if (this.authenticationService.getUser<AuthenticatedUser>().hasPermission(Permission.maintenanceOfAllUsers))
            {
                return this.organizationService.getOrganizationHierarchy();
            }
            else
            {
                return this.organizationService
                    .getOrganizationHierarchyForOrganization(this.authenticationService.getUser<AuthenticatedUser>()
                        .organizationId);
            }
        }

        private initializeSelectionLists(): void
        {
            this.busyIndicationService.showBusyIndicator("Haetaan valintalistojen sisältöä...");

            this.$q.all([this.getOrganizationHierarchy(), this.settingsService.getUserRoles()])
                .then((result: Array<any>) =>
                {
                    this.organizationHiearchy = result[0];
                   

                    var userRoles: Array<UserRole> = result[1];
                    if (this.authenticationService.getUser<AuthenticatedUser>().hasPermission(Permission.maintenanceOfAllUsers))
                {
                        this.userRoles = userRoles;
                    }
                    else
                        {
                        this.userRoles = userRoles.filter((item: UserRole) => item.name !== Role.systemAdmin);
                    }
                    this.busyIndicationService.hideBusyIndicator();
                   
                });
        }
    }
}