"use strict";

module OrganizationRegister
{
    export class OrganizationController implements Affecto.Base.IController
    {
        public static $inject = ["$scope", "$location", "$routeParams", "$sce", "$route", "$q", "organizationService", "settingsService", "validationService", "busyIndicationService"];

        public organizationTypes: Array<string>;
        public webPageTypes: Array<string>;
        public callChargeTypes: Array<string>;
        public groupRoles: Array<UserRole>;

        public validBusinessId: boolean;
        public validValidity: boolean;
        public validPhoneNumber: boolean;
        public validPtvId: boolean;
        public validEmailAddress: boolean;
        public validWebPageUrl: boolean;
        public validEditedWebPageUrl: boolean;
        public validVisitingAddressPostalCode: boolean;
        public validPostalStreetAddressPostalCode: boolean;
        public validPostalPostOfficeBoxAddressPostalCode: boolean;
        public webPageEditModeOn: boolean;
        public isValidFromCalendarShown: boolean;
        public isValidToCalendarShown: boolean;
        public businessIdErrorMessage: string;
        public ptvIdErrorMessage: string;
        public webPageUrlBeforeEditing: string;
        public toBeAddedPostalAddressType: string;
        public editedSection: EditedOrganizationSection;
        public parentOrganizationId: string;
        public validHomepageUrls: boolean;
        public parentOrganizationNames: Array<LocalizedText>;
        public model: Organization;
        public originalModel: Organization;
        public basicInformationForm: angular.IFormController;
        public contactInformationForm: angular.IFormController;
        public visitingAddressForm: angular.IFormController;
        public postalAddressForm: angular.IFormController;
        public authorizationInformationForm: angular.IFormController;
        public authorizationGroupEditModeOn: boolean;
        public authorizationGroupNameBeforeEditing: string;

        public displayOrganizationDeleteConfirm: boolean;

        constructor(private $scope: Affecto.Base.IViewScope, private $location: angular.ILocationService, $routeParams: IOrganizationRoute, private $sce: angular.ISCEService,
            $route: angular.route.IRouteService, private $q: angular.IQService, private organizationService: OrganizationService, private settingsService: SettingsService,
            private validationService: ValidationService, private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService)
        {
            $scope.controller = this;
            $scope.model = this.model;

            this.displayOrganizationDeleteConfirm = false;

            this.validBusinessId = true;
            this.validValidity = true;
            this.validPhoneNumber = true;
            this.validEmailAddress = true;
            this.validWebPageUrl = true;
            this.validEditedWebPageUrl = true;
            this.validVisitingAddressPostalCode = true;
            this.validPostalStreetAddressPostalCode = true;
            this.validPostalPostOfficeBoxAddressPostalCode = true;
            this.validHomepageUrls = true;
            this.webPageEditModeOn = false;
            this.isValidFromCalendarShown = false;
            this.isValidToCalendarShown = false;
            this.validPtvId = true;

            this.fetchServerData($routeParams);
            this.initializeEditedSection($route);
        }

        public showDeleteOrganizationConfirm(display: boolean): void
        {
            this.displayOrganizationDeleteConfirm = display;
        };

        public get editBusinessIdentifierLabel(): string
        {
            var label: string = "Y-tunnus";
            if (this.isSubOrganization())
            {
                return label;
            }
            return label + "*";
        }


       
        public canSaveContactInformation(): boolean
        {
            return this.validPhoneNumber && this.validEmailAddress && this.validHomepageUrls;
        }

        public canAddNewWebPage(): boolean
        {
            return this.model != null && this.model.hasWebPage() && this.validWebPageUrl && !this.webPageEditModeOn;
        }

        public canSaveEditedWebPage(): boolean
        {
          
            return this.webPageEditModeOn && this.model.hasEditedWebPage() && this.validEditedWebPageUrl;
        }

        public showEditWebPageErrorMessage(url: string): boolean
        {
            return this.webPageEditModeOn && this.webPageUrlBeforeEditing === url && !this.validEditedWebPageUrl;
        }


        public canSaveAuthorizationGroup(): boolean {
            return this.authorizationGroupEditModeOn && this.model.hasEditedAuthorizationGroup();
        }

        public showPostOfficeBoxPostalAddress(): boolean
        {
            if (this.model == null)
            {
                return false;
            }
            return this.model.postalAddressTypes.contains(PostalAddressType.PostOfficeBoxAddress);
        }

        public showValidFromCalendar(): void
        {
            this.isValidFromCalendarShown = true;
        }

        public showValidToCalendar(): void
        {
            this.isValidToCalendarShown = true;
        }

        public showSeparateStreetPostalAddress(): boolean
        {
            if (this.model == null)
            {
                return false;
            }
            return this.model.postalAddressTypes.contains(PostalAddressType.SeparateStreetAddress);
        }

        public showSameAsVisitingAddressPostalAddress(): boolean
        {
            if (this.model == null)
            {
                return false;
            }
            return this.model.postalAddressTypes.contains(PostalAddressType.SameAsVisitingAddress) && this.model.hasVisitingAddressParts();
        }

        public isWebPageBeingEdited(url: string)
        {
            return this.webPageEditModeOn && this.webPageUrlBeforeEditing === url;
        }

        public isAuthorizationGroupBeingEdited(groupName: string) {
            return this.authorizationGroupEditModeOn && this.authorizationGroupNameBeforeEditing === groupName;
        }

        public isOtherWebPageBeingEdited(url: string)
        {
            return this.webPageEditModeOn && this.webPageUrlBeforeEditing !== url;
        }

        public isOrganizationBeingEdited(): boolean
        {
            return this.editedSection !== EditedOrganizationSection.None;
        }

        public isBasicInformationBeingEdited(): boolean
        {
            return this.editedSection === EditedOrganizationSection.BasicInfromation;
        }

        public isContactInformationBeingEdited(): boolean
        {
            return this.editedSection === EditedOrganizationSection.ContactInformation;
        }

        public isVisitingAddressBeingEdited(): boolean
        {
            return this.editedSection === EditedOrganizationSection.VisitingAddress;
        }

        public isPostalAddressBeingEdited(): boolean
        {
            return this.editedSection === EditedOrganizationSection.PostalAddress;
        }

        public isAuthorizationInformationBeingEdited(): boolean {
            return this.editedSection === EditedOrganizationSection.Authorization;
        }

        public isOtherAuthorizationGroupBeingEdited(groupName: string) {
            return this.authorizationGroupEditModeOn && this.authorizationGroupNameBeforeEditing !== groupName;
        }


        


        public isSubOrganization(): boolean
        {
            return this.parentOrganizationId != null || (this.model != null && this.model.isSubOrganization);
        }

       

        public canAddingBeCancelled(): boolean
        {
            return this.model == null || !this.model.isAdded();
        }

        public canAddPostalAddress(): boolean
        {
            if (this.model == null)
            {
                return false;
            }
            return this.model.canAddPostalAddress();
        }

        public resetAllFormFieldsAsValid(): void
        {
            this.setFormFieldValidity(this.basicInformationForm, "businessId", true);
            this.setFormFieldValidity(this.basicInformationForm, "ptvId", true);
            this.setValidityValidity(true);
            this.setEmailAddressValidity(true);
            this.setPhoneNumberValidity(true);
            this.setVisitingAddressPostalCodeValidity(true);
            this.setPostalStreetAddressPostalCodeValidity(true);
            this.setPostalPostOfficeBoxAddressPostalCodeValidity(true);
            this.setWebPageUrlValidity(true);
            this.setEditedWebPageUrlValidity(true);
            this.setHomepageUrlsValidity(true);
           
        }

        public cancelEditing(): void
        {
            this.resetAllFormFieldsAsValid();
            this.model = this.originalModel;
            this.basicInformationForm.$setPristine();
            this.basicInformationForm.$setUntouched();
            this.editedSection = EditedOrganizationSection.None;
        }

        public editOrganizationBasicInformation(): void
        {
            this.businessIdErrorMessage = null;
            this.ptvIdErrorMessage = null;
            this.originalModel = angular.copy(this.model);
            this.basicInformationForm.$setPristine();
            this.basicInformationForm.$setUntouched();
            this.editedSection = EditedOrganizationSection.BasicInfromation;
        }

        public saveEditOrganizationBasicInformation(): angular.IPromise<void>
        {
            
            this.editedSection = EditedOrganizationSection.None;
            if (this.isModelChanged())
            {
                return this.saveOrganizationBasicInformation(false);
            }
        }

        public saveOrganizationBasicInformationAndMoveToNextStep(): angular.IPromise<void>
        {
            this.editedSection = EditedOrganizationSection.ContactInformation;
            if (this.isModelChanged())
            {
                if (this.model.isAdded())
                {
                    return this.saveOrganizationBasicInformation(false);
                }
                
                return this.addOrganizationWithBasicAndContactInformation(false);
            }
        }

        public saveOrganizationBasicInformationAndQuit(): angular.IPromise<void>
        {
            if (this.isModelChanged())
            {
                if (this.model.isAdded())
                {
                    return this.saveOrganizationBasicInformation(true);
                }
                return this.addOrganizationWithBasicAndContactInformation(true);
            }
            else
            {
                this.goToHomePage();
            }
        }

        public saveOrganizationContactInformationAndMoveToPreviousStep(): angular.IPromise<void>
        {
            this.editedSection = EditedOrganizationSection.BasicInfromation;
            if (this.isModelChanged())
            {
                return this.saveOrganizationContactInformation(false);
            }
        }

        public editOrganizationContactInformation(): void
        {
            this.originalModel = angular.copy(this.model);
            this.contactInformationForm.$setPristine();
            this.editedSection = EditedOrganizationSection.ContactInformation;
        }

        public saveEditedOrganizationContactInformation(): angular.IPromise<void>
        {
            this.editedSection = EditedOrganizationSection.None;
            if (this.isModelChanged())
            {
                return this.saveOrganizationContactInformation(false);
            }
        }

        public saveEditedOrganizationAuthorizationInformation(): angular.IPromise<void> {
            this.editedSection = EditedOrganizationSection.None;
            if (this.isModelChanged()) {
                return this.saveOrganizationContactInformation(false);
            }
        }

        public saveOrganizationContactInformationAndMoveToNextStep(): angular.IPromise<void>
        {
            this.editedSection = EditedOrganizationSection.VisitingAddress;
            if (this.isModelChanged())
            {
                return this.saveOrganizationContactInformation(false);
            }
        }

        public saveOrganizationContactInformationAndQuit(): angular.IPromise<void>
        {
            if (this.isModelChanged())
            {
                if (this.model.isAdded())
                {
                    return this.saveOrganizationContactInformation(true);
                }
            }
            else
            {
                this.goToHomePage();
            }
        }

        public editOrganizationVisitingAddress(): void
        {
            this.originalModel = angular.copy(this.model);
            this.visitingAddressForm.$setPristine();
            this.editedSection = EditedOrganizationSection.VisitingAddress;
        }

        public saveEditedOrganizationVisitingAddress(): angular.IPromise<void>
        {
            this.editedSection = EditedOrganizationSection.None;
            if (this.isModelChanged())
            {
                return this.saveOrganizationVisitingAddress(false);
            }
        }

        public saveOrganizationVisitingAddressAndMoveToPreviousStep(): angular.IPromise<void>
        {
            this.editedSection = EditedOrganizationSection.ContactInformation;
            if (this.isModelChanged())
            {
                return this.saveOrganizationVisitingAddress(false);
            }
        }

        public saveOrganizationVisitingAddressAndMoveToNextStep(): angular.IPromise<void>
        {
            this.editedSection = EditedOrganizationSection.PostalAddress;
            if (this.isModelChanged())
            {
                return this.saveOrganizationVisitingAddress(false);
            }
        }

        public saveOrganizationVisitingAddressAndQuit(): angular.IPromise<void>
        {
            if (this.isModelChanged())
            {
                if (this.model.isAdded())
                {
                    return this.saveOrganizationVisitingAddress(true);
                }
            }
            else
            {
                this.goToHomePage();
            }
        }

        public editOrganizationPostalAddress(): void
        {
            this.originalModel = angular.copy(this.model);
            this.postalAddressForm.$setPristine();
            this.editedSection = EditedOrganizationSection.PostalAddress;
        }

        public saveEditedOrganizationPostalAddress(): angular.IPromise<void>
        {
            this.editedSection = EditedOrganizationSection.None;
            if (this.isModelChanged())
            {
                return this.saveOrganizationPostalAddress(false);
            }
        }

        public saveOrganizationPostalAddressAndMoveToPreviousStep(): angular.IPromise<void>
        {
            this.editedSection = EditedOrganizationSection.VisitingAddress;
            if (this.isModelChanged())
            {
                return this.saveOrganizationPostalAddress(false);
            }
        }

        public saveOrganizationPostalAddressAndMoveToNextStep(): angular.IPromise<void> {
            this.editedSection = EditedOrganizationSection.Authorization;
            if (this.isModelChanged()) {
                return this.saveOrganizationPostalAddress(false);
            }
        }


        public saveOrganizationPostalAddressAndQuit(): angular.IPromise<void>
        {
            if (this.isModelChanged())
            {
                if (this.model.isAdded())
                {
                    return this.saveOrganizationPostalAddress(true);
                }
            }
            this.goToHomePage();
        }



        public saveOrganizationAuthorizationInformationAndMoveToPreviousStep(): angular.IPromise<void> {
            this.editedSection = EditedOrganizationSection.PostalAddress;
            if (this.isModelChanged()) {
                return this.saveOrganizationAuthorizationInformation(false);
            }
        }

       
        public saveOrganizationAuthorizationInformationAndQuit(): angular.IPromise<void> {
            if (this.isModelChanged()) {
                if (this.model.isAdded()) {
                    return this.saveOrganizationAuthorizationInformation(true);
                }
            }
            this.goToHomePage();
        }



        public editAuthorizatioInformation(): void
        {
            this.originalModel = angular.copy(this.model);
            this.authorizationInformationForm.$setPristine();
            this.editedSection = EditedOrganizationSection.Authorization;
        }

        public addNewAuthorizationGroup(): void
        {

            this.model.addAuthorizationGroup(this.model.authorizationGroupName, this.model.authorizationGroupRole.id, this.model.authorizationGroupRole.name);
            this.model.authorizationGroupName = null;
            this.model.authorizationGroupRole = null;
            
        }


        public editAuthorizationGroup(groupName: string, groupRoleId: string): void
        {
            this.authorizationGroupNameBeforeEditing = groupName;
            this.model.editedAuthorizationGroupName = groupName;
           
            for (let i = 0; i < this.groupRoles.length; i++) {
                if (this.groupRoles[i].id === groupRoleId)
                {
                    this.model.editedAuthorizationGroupRole = this.groupRoles[i];
                    break;
                }
            }
            
            this.authorizationGroupEditModeOn = true;
            
        }


        public canAddNewAuthorizationGroup(): boolean
        {
           

            return this.model != null && this.model.hasAuthorizationGroup() && !this.model.authorizationGroupExists() && !this.authorizationGroupEditModeOn;
        }


        public addNewWebPage(): void
        {
            if (this.model.hasWebPageUrl())
            {
                this.validationService.validateWebAddress(this.model.webPageUrl)
                    .then((isValid: boolean) =>
                    {
                        this.setWebPageUrlValidity(isValid);
                        if (this.canAddNewWebPage())
                        {
                            this.model.addWebPage(this.model.webPageName, this.model.webPageUrl, this.model.webPageType);
                            this.model.webPageName = null;
                            this.model.webPageUrl = null;
                            this.model.webPageType = null;
                        }
                    });
            }
            else
            {
                this.setWebPageUrlValidity(true);
            }
        }


        public editWebPage(name: string, url: string, pageType: string): void
        {
            this.webPageUrlBeforeEditing = url;
            this.model.editedWebPageName = name;
            this.model.editedWebPageUrl = url;
            this.model.editedWebPageType = pageType;
            this.webPageEditModeOn = true;
            this.setEditedWebPageUrlValidity(true);
        }

        public saveEditedWebPage(url: string): void
        {
            if (this.model.hasEditedWebPageUrl())
            {
                this.validationService.validateWebAddress(this.model.editedWebPageUrl)
                    .then((isValid: boolean) =>
                    {
                        this.setEditedWebPageUrlValidity(isValid);
                        if (this.canSaveEditedWebPage())
                        {
                            this.model.removeWebPage(this.webPageUrlBeforeEditing);
                            this.model.addWebPage(this.model.editedWebPageName, this.model.editedWebPageUrl, this.model.editedWebPageType);
                            this.webPageEditModeOn = false;
                        }
                    });
            }
            else
            {
                this.setEditedWebPageUrlValidity(false);
            }
        }

        public saveEditedAuthorizationGroup(): void
        {
            if (this.canSaveAuthorizationGroup())
            {
                this.model.removeAuthorizationGroup(this.authorizationGroupNameBeforeEditing);

                //this.model.addAuthorizationGroup(this.model.editedAuthorizationGroupName, this.model.editedAuthorizationGroupRole);

                this.model.addAuthorizationGroup(this.model.editedAuthorizationGroupName, this.model.editedAuthorizationGroupRole.id, this.model.editedAuthorizationGroupRole.name);
                this.authorizationGroupEditModeOn = false;
            }
        }

        public removeAuthorizationGroup(groupName: string): void {
            this.model.removeAuthorizationGroup(groupName);
        }


        public cancelWebPageEditing(): void
        {
            this.webPageEditModeOn = false;
        }

        public removeWebPage(url: string): void
        {
            this.model.removeWebPage(url);
        }


        public cancelAuthorizationGroupEditing(): void {
            this.authorizationGroupEditModeOn = false;
        }

        public addOrganizationPostalAddress(): void
        {
            if (this.toBeAddedPostalAddressType === PostalAddressType[PostalAddressType.SameAsVisitingAddress])
            {
                this.postalAddressForm.$setDirty();  
            }
            this.model.addPostalAddressType(this.toBeAddedPostalAddressType);
            this.toBeAddedPostalAddressType = this.model.postalAddressTypes.available.firstOrDefault();
        }

        public removePostOfficeBoxPostalAddress(): void
        {
            this.postalAddressForm.$setDirty();
            this.model.removePostOfficeBoxPostalAddress();
            this.toBeAddedPostalAddressType = this.model.postalAddressTypes.available.firstOrDefault();
        }

        public removeSeparateStreetPostalAddress(): void
        {
            this.postalAddressForm.$setDirty();
            this.model.removeSeparateStreetPostalAddress();
            this.toBeAddedPostalAddressType = this.model.postalAddressTypes.available.firstOrDefault();
        }

        public removeSameAsVisitingAddressPostalAddress(): void
        {
            this.model.removeSameAsVisitingAddressPostalAddress();
            this.postalAddressForm.$setDirty();
            this.toBeAddedPostalAddressType = this.model.postalAddressTypes.available.firstOrDefault();
        }

        //public validatePtvId(): void
        //{
        //    if (this.model.ptvId != "")
        //    {
        //        this.setPtvIdValidity(false);
        //    }
        //    else
        //    {
        //        this.setPtvIdValidity(true);
        //    }
        //}

        public validatePtvId(): angular.IPromise<void> {
            if (this.model.hasPtvId()) {
                return this.validationService.validatePtvId(this.model.ptvId, this.model.id, false)
                    .then(this.setPtvIdValidity);
            }
            else {
                this.setPtvIdValidity(new BusinessIdentifierValidationResult(true, null));
            }
        }


        public validateBusinessId(): angular.IPromise<void>
        {
            if (this.model.hasBusinessId())
            {
                //alert('validateBusinessId this.model.id:' + this.model.id);
                return this.validationService.validateBusinessId(this.model.businessId, this.model.id, this.isSubOrganization())
                    .then(this.setBusinessIdValidity);
            }
            else
            {
                this.setBusinessIdValidity(new BusinessIdentifierValidationResult(true, null));
            }
        }

        public validateEmailAddress(): void
        {
            if (this.model.hasEmailAddress())
            {
                this.validationService.validateEmailAddress(this.model.emailAddress)
                    .then(this.setEmailAddressValidity);
            }
            else
            {
                this.setEmailAddressValidity(true);
            }
        }

        public validateValidity(): void
        {
            this.setValidityValidity(this.model.isValidValidity());
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

        public validateWebPageUrl(): void
        {
            if (this.model.hasWebPageUrl())
            {
                this.validationService.validateWebAddress(this.model.webPageUrl)
                    .then(this.setWebPageUrlValidity);
            }
            else
            {
                this.setWebPageUrlValidity(true);
            }
        }


        public validateHomepageUrls(): void
        {
            if (this.model.hasHomepageUrls())
            {
                this.model.homepageUrls.forEach((url) =>
                {
                    if (url.localizedValue != null && url.localizedValue !== "" ) {
                        this.validationService.validateWebAddress(url.localizedValue)
                            .then(this.setHomepageUrlsValidity);
                    }
                });
            }
            else
            {
                this.setHomepageUrlsValidity(true);
            }
        }

        public validateEditedWebPageUrl(): void
        {
            if (this.model.hasEditedWebPageUrl())
            {
                this.validationService.validateWebAddress(this.model.editedWebPageUrl)
                    .then(this.setEditedWebPageUrlValidity);
            }
            else
            {
                this.setEditedWebPageUrlValidity(false);
            }
        }

        public validateVisitingAddressPostalCode(): void
        {
            if (this.model.hasVisitingAddressPostalCode())
            {
                this.validationService.validatePostalCode(this.model.visitingAddressPostalCode)
                    .then(this.setVisitingAddressPostalCodeValidity);
            }
            else
            {
                this.setVisitingAddressPostalCodeValidity(true);
            }
        }

        public validatePostalStreetAddressPostalCode(): void
        {
            if (this.model.hasPostalStreetAddressPostalCode())
            {
                this.validationService.validatePostalCode(this.model.postalStreetAddressPostalCode)
                    .then(this.setPostalStreetAddressPostalCodeValidity);
            }
            else
            {
                this.setPostalStreetAddressPostalCodeValidity(true);
            }
        }

        public validatePostalPostOfficeBoxAddressPostalCode(): void
        {
            if (this.model.hasPostalPostOfficeBoxAddressPostalCode())
            {
                this.validationService.validatePostOfficeBoxPostalCode(this.model.postalPostOfficeBoxAddressPostalCode)
                    .then(this.setPostalPostOfficeBoxAddressPostalCodeValidity);
            }
            else
            {
                this.setPostalPostOfficeBoxAddressPostalCodeValidity(true);
            }
        }

        public deleteOrganization(): void
        {
            this.busyIndicationService.showBusyIndicator("Poistetaan organisaatiota...");
            this.organizationService.deactivateOrganization(this.model.id)
                .then(() =>
                {
                    this.busyIndicationService.hideBusyIndicator();
                    this.goToHomePage();
                });
        }

        public areWebPagesChanged(): boolean
        {
            if (this.model != null && this.originalModel != null)
            {
                return !angular.equals(this.model.webPages, this.originalModel.webPages);
            }
            return false;
        }

        private isModelChanged(): boolean
        {
            return !angular.equals(this.model, this.originalModel);
        }

        public goToHomePage(): void
        {
            this.$location.path("/");
        }

        private saveOrganizationPostalAddress(goToHomePageAfterSaving: boolean): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan organisaation postiosoitetta...");
            this.model.generatePostalAddressLocalizedTexts();
            this.originalModel = angular.copy(this.model);
            return this.organizationService.setOrganizationPostalAddress(this.model)
                .then(() =>
                {
                    this.model.initializeLocalizedTexts();
                    this.busyIndicationService.hideBusyIndicator();
                    if (goToHomePageAfterSaving)
                    {
                        this.goToHomePage();
                    }
                });
        }

        private saveOrganizationVisitingAddress(goToHomePage: boolean): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan organisaation käyntiosoitetta...");
            this.model.generateVisitingAddressLocalizedTexts();
            if (!this.model.hasVisitingAddressParts())
            {
                this.model.useVisitingAddressAsPostalAddress = false;
            }
            this.originalModel = angular.copy(this.model);
            this.model.setAvailablePostalStreetAddressTypes();
            return this.organizationService.setOrganizationVisitingAddress(this.model)
                .then(() =>
                {
                    this.model.initializeLocalizedTexts();
                    this.busyIndicationService.hideBusyIndicator();
                    if (goToHomePage)
                    {
                        this.goToHomePage();
                    }
                });
        }

        private saveOrganizationContactInformation(goToHomePage: boolean): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan organisaation yhteystietoja...");

            this.model.generateContactinformationLocalizedTexts();
            this.originalModel = angular.copy(this.model);
            return this.organizationService.setOrganizationContactInformation(this.model)
                .then(() =>
                {
                    this.model.initializeLocalizedTexts();
                    this.busyIndicationService.hideBusyIndicator();
                    if (goToHomePage)
                    {
                        this.goToHomePage();
                    }
                });
        }

        private saveOrganizationAuthorizationInformation(goToHomePage: boolean): angular.IPromise<void> {
            this.busyIndicationService.showBusyIndicator("Tallennetaan organisaation käyttöoikeustietoja...");
            this.originalModel = angular.copy(this.model);
            return this.organizationService.setOrganizationAuthorizationtInformation(this.model)
                .then(() => {
                    this.model.initializeLocalizedTexts();
                    this.busyIndicationService.hideBusyIndicator();
                    if (goToHomePage) {
                        this.goToHomePage();
                    }
                });
        }



        private saveOrganizationBasicInformation(goToHomePage: boolean)
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan organisaation perustietoja...");
            this.model.generateBasicInformationLocalizedAndFormattedTexts();
            this.model.descriptionsAsHtml.forEach((desc, index) =>
            {
                this.model.descriptionsAsHtml[index] = this.$sce.trustAsHtml(desc.localizedValue);
            });


            this.originalModel = angular.copy(this.model);
            return this.organizationService.setOrganizationBasicInformation(this.model)
                .then(() =>
                {
                    this.model.initializeLocalizedTexts();
                    this.busyIndicationService.hideBusyIndicator();
                    if (goToHomePage)
                    {
                        this.goToHomePage();
                    }
                });
        }





        private addOrganizationWithBasicAndContactInformation(goToHomePage: boolean): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan organisaation perustietoja...");
            this.model.generateBasicInformationLocalizedAndFormattedTexts();
           
            this.model.descriptionsAsHtml.forEach((desc,index) =>
            {
                this.model.descriptionsAsHtml[index] = this.$sce.trustAsHtml(desc.localizedValue);
            });
            return this.organizationService.addOrganization(this.model, this.parentOrganizationId)
                .then((organizationId: string) =>
                {

                    this.model.initializeLocalizedTexts();
                    this.model.id = organizationId;
                    if (this.parentOrganizationId != null)
                    {
                        this.model.isSubOrganization = true;
                    }
                    this.originalModel = angular.copy(this.model);
                    this.busyIndicationService.hideBusyIndicator();
                    if (this.model.hasContactInformation())
                    {
                        this.saveOrganizationContactInformation(false)
                            .then(() =>
                            {
                                
                                if (goToHomePage)
                                {
                                    this.goToHomePage();
                                }
                            });
                    }
                    else if (goToHomePage)
                    {
                        this.goToHomePage();
                    }
                });
        }

        private setBusinessIdValidity = (validationResult: BusinessIdentifierValidationResult): void =>
        {
            this.validBusinessId = validationResult.isValid;
            if (this.validBusinessId)
            {
                this.businessIdErrorMessage = null;
            }
            else
            {
                switch (validationResult.reasonForInvalidity)
                {
                    case InvalidBusinessIdentifierReason.checkSum:
                        this.businessIdErrorMessage = "Y-tunnuksen tarkistusmerkki on virheellinen";
                        break;
                    case InvalidBusinessIdentifierReason.orderNumber:
                        this.businessIdErrorMessage = "Y-tunnuksen järjestysnumero on virheellinen";
                        break;
                    case InvalidBusinessIdentifierReason.alreadyExists:
                        this.businessIdErrorMessage = "Y-tunnus on jo käytössä aiemmin lisätyllä organisaatiolla";
                        break;
                    case InvalidBusinessIdentifierReason.format:
                    default:
                        this.businessIdErrorMessage = "Y-tunnus on seitsemän numeroa, väliviiva ja tarkistusmerkki";
                }
            }

            this.setFormFieldValidity(this.basicInformationForm, "businessId", validationResult.isValid);
        }

        private setPtvIdValidity = (validationResult: BusinessIdentifierValidationResult): void => {
            this.validPtvId = validationResult.isValid;

            if (validationResult.isValid)
            {
                this.ptvIdErrorMessage = null;
            }
            else
            {
                this.ptvIdErrorMessage = "Tunniste on käytössä myös organisaatioissa: " + validationResult.reasonForInvalidity;

            }

            this.validPtvId = true;
            this.setFormFieldValidity(this.basicInformationForm, "ptvId", true);
        }




        private setPhoneNumberValidity = (isValid: boolean): void =>
        {
            this.validPhoneNumber = isValid;
            this.setFormFieldValidity(this.contactInformationForm, "phoneNumber", isValid);
        }

        private setVisitingAddressPostalCodeValidity = (isValid: boolean): void =>
        {
            this.validVisitingAddressPostalCode = isValid;
            this.setFormFieldValidity(this.visitingAddressForm, "postalCode", isValid);
        }

        private setPostalStreetAddressPostalCodeValidity = (isValid: boolean): void =>
        {
            this.validPostalStreetAddressPostalCode = isValid;
            this.setFormFieldValidity(this.postalAddressForm, "postalStreetAddressPostalCode", isValid);
        }

        private setPostalPostOfficeBoxAddressPostalCodeValidity = (isValid: boolean): void =>
        {
            this.validPostalPostOfficeBoxAddressPostalCode = isValid;
            this.setFormFieldValidity(this.postalAddressForm, "postalPostOfficeBoxAddressPostalCode", isValid);
        }

        private setEmailAddressValidity = (isValid: boolean): void =>
        {
            this.validEmailAddress = isValid;
            this.setFormFieldValidity(this.contactInformationForm, "email", isValid);
        }

        private setWebPageUrlValidity = (isValid: boolean): void =>
        {
            this.validWebPageUrl = isValid;
            this.setFormFieldValidity(this.contactInformationForm, "webPage", isValid);
        }

        private setEditedWebPageUrlValidity = (isValid: boolean): void =>
        {
            this.validEditedWebPageUrl = isValid;
            this.setFormFieldValidity(this.contactInformationForm, "editedWebPage", isValid);
        }

        private setHomepageUrlsValidity = (isValid: boolean): void =>
        {
            this.validHomepageUrls = isValid;
            this.setFormFieldValidity(this.contactInformationForm, "homePageUrls", isValid);
        }

        private setValidityValidity(isValid: boolean): void
        {
            this.validValidity = isValid;
            this.setFormFieldValidity(this.basicInformationForm, "validFrom", isValid);
            this.setFormFieldValidity(this.basicInformationForm, "validTo", isValid);
        }

        private setFormFieldValidity = (form: angular.IFormController, fieldName: string, isValid: boolean): void =>
        {
            if (form[fieldName] != null)
            {
                form[fieldName].$setValidity("format", isValid);
            }
        }

        private fetchTypeLists(): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Haetaan valintalistojen sisältöä...");
            return this.$q.all([this.settingsService.getOrganizationTypes(), this.settingsService.getWebPageTypes(), this.settingsService.getCallChargeTypes()])
                .then((result: Array<any>) =>
                {
                    [this.organizationTypes, this.webPageTypes, this.callChargeTypes] = result;
                    this.busyIndicationService.hideBusyIndicator();
                });
        }

        private fetchOrganizationAndTypeLists($routeParams: IOrganizationRoute): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Haetaan organisaation tietoja...");
            return this.$q.all([this.organizationService.getOrganization($routeParams.organizationId), this.settingsService.getOrganizationTypes(),
                this.settingsService.getWebPageTypes(), this.settingsService.getCallChargeTypes(), this.settingsService.getUserRoles()])
                .then((result: Array<any>) =>
                {
                    var organization: Organization = result[0];
                    organization.descriptionsAsHtml.forEach((desc, index) =>
                    {
                        organization.descriptionsAsHtml[index].localizedValue = this.$sce.trustAsHtml(desc.localizedValue);
                    });

                    this.model = organization;
                    this.toBeAddedPostalAddressType = this.model.postalAddressTypes.available.firstOrDefault();
                    this.organizationTypes = result[1];
                    this.webPageTypes = result[2];
                    this.callChargeTypes = result[3];
                    this.groupRoles = result[4].filter((item: UserRole) => item.name !== Role.systemAdmin);  // TODO: Handle system admin role
                    this.fetchAuthorizationGroupRoleNames();

                    this.busyIndicationService.hideBusyIndicator();
                });            
        }

        private fetchAuthorizationGroupRoleNames(): void
        {
            this.model.authorizationGroups.forEach((desc, index) => {
                for (let i = 0; i < this.groupRoles.length; i++) {
                    if (this.groupRoles[i].id === this.model.authorizationGroups[index].roleId) {
                        this.model.authorizationGroups[index].roleName = this.groupRoles[i].name;
                        break;
                    }
                }
            });
        }

        private fetchParentOrganizationAndTypeLists($routeParams: IOrganizationRoute): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Haetaan organisaation esitietoja...");
            this.parentOrganizationId = $routeParams.parentOrganizationId;
            return this.$q.all([this.organizationService.getOrganization(this.parentOrganizationId), this.settingsService.getOrganizationTypes(),
                this.settingsService.getWebPageTypes(), this.settingsService.getCallChargeTypes(), this.settingsService.getUserRoles()])
                .then((result: Array<any>) =>
                {
                    var organization: Organization = result[0];
                    organization.descriptionsAsHtml.forEach((desc, index) =>
                    {
                        organization.descriptionsAsHtml[index].localizedValue = this.$sce.trustAsHtml(desc.localizedValue);
                    });

                    this.createSubOrganization(organization);
                    this.toBeAddedPostalAddressType = this.model.postalAddressTypes.available.firstOrDefault();
                    this.organizationTypes = result[1];
                    this.webPageTypes = result[2];
                    this.callChargeTypes = result[3];
                    var roles: Array<UserRole> = result[4];
                    // TODO: Handle system admin role
                    this.groupRoles = roles.filter((item: UserRole) => item.name !== Role.systemAdmin);

                    this.busyIndicationService.hideBusyIndicator();
                });            
        }

        private fetchServerData($routeParams: IOrganizationRoute): angular.IPromise<void>
        {
            if ($routeParams.organizationId == null && $routeParams.parentOrganizationId == null)
            {
                this.model = new Organization();
                this.model.canBeTransferredToFsc = true;
                return this.fetchTypeLists();
            }
            else if ($routeParams.organizationId == null && $routeParams.parentOrganizationId != null)
            {
                return this.fetchParentOrganizationAndTypeLists($routeParams);
            }
            else
            {
                return this.fetchOrganizationAndTypeLists($routeParams);
            }
        }

        private createSubOrganization(parent: Organization): void
        {
            this.parentOrganizationNames = parent.names;
            this.model = new Organization();
            this.model.type = parent.type;
            if (parent.isMunicipality())
            {
                this.model.municipalityCode = parent.municipalityCode;
            }
            this.model.businessId = parent.businessId;
            this.model.phoneNumber = parent.phoneNumber;
            this.model.phoneCallChargeType = parent.phoneCallChargeType;
            this.model.phoneCallChargeInfos = parent.phoneCallChargeInfos;
            this.model.canBeTransferredToFsc = parent.canBeTransferredToFsc;
            this.model.isSubOrganization = true;
            this.model.canBeResponsibleDeptForService = false;
        }

        private initializeEditedSection($route: any): void
        {
            if ($route.current.locals.editedSection != null)
            {
                this.editedSection = $route.current.locals.editedSection;
            }
            else
            {
                this.editedSection = EditedOrganizationSection.None;
            }
        }
    }
}
