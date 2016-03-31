"use strict";

module OrganizationRegister
{
    export class ServiceController implements Affecto.Base.IController
    {
        public static $inject = [
            "$scope", "$routeParams", "$location", "$route", "$sce", "$q", "busyIndicationService", "serviceService", "settingsService", "classificationService"
        ];

        public model: Service;
        public editedSection: EditedServiceSection;
        public basicInformationForm: angular.IFormController;
        public classificationAndOnthologyTermsForm: angular.IFormController;
        public languages: Languages;
        public selectedLanguages: Array<Language>;
        public serviceClasses: Classification;
        public ontologyTerms: OntologyTerms;
        public targetGroups: Classification;
        public lifeEvents: Classification;
        public serviceClassPredicate: string;
        public ontologyTermPredicate: string;
        public targetGroupPredicate: string;
        public lifeEventPredicate: string;

        private organizationId: string;
        private originalModel: Service;
        private originalServiceClasses: Classification;
        private originalOntologyTerms: OntologyTerms;
        private originalTargetGroups: Classification;
        private originalLifeEvents: Classification;

        constructor(private $scope: Affecto.Base.IViewScope, $routeParams: IServiceRoute, private $location: angular.ILocationService, $route: angular.route.IRouteService,
            private $sce: angular.ISCEService, private $q: angular.IQService, private busyIndicationService: Affecto.BusyIndication.IBusyIndicationService,
            private serviceService: ServiceService, private settingsService: SettingsService, private classificationService: ClassificationService)
        {
            $scope.controller = this;
            $scope.model = this.model;

            this.initializeClassificationPredicates();
            this.initializeService($routeParams);
            this.initializeEditedSection($route);
        }

        public setExpandedServiceClasses(): void
        {
            this.setExpandedClassification(this.serviceClasses, this.serviceClassPredicate);
        }

        public setExpandedTargetGroups(): void
        {
            this.setExpandedClassification(this.targetGroups, this.targetGroupPredicate);
        }

        public setExpandedLifeEvents(): void
        {
            this.setExpandedClassification(this.lifeEvents, this.lifeEventPredicate);
        }

        public searchOntologyTerms(): void
        {
            if (this.ontologyTermPredicate.length < 2)
            {
                this.ontologyTerms.available = new Array<Class>();
            }
            else
            {
                this.classificationService.searchOntologyTerms(this.ontologyTermPredicate)
                    .then((result: OntologyTermsSearchResult) =>
                    {
                        if (result.searchText === this.ontologyTermPredicate)
                        {
                            this.ontologyTerms.available = result.ontologyTerms;
                        }
                    });                
            }
        }

        public addSelectedServiceClass(): void
        {
            this.serviceClasses.addSelected();
            this.classificationAndOnthologyTermsForm.$setDirty();
        }

        public removeServiceClass(id: string): void
        {
            this.serviceClasses.remove(id);
            this.classificationAndOnthologyTermsForm.$setDirty();
        }

        public addSelectedOntologyTerm(): void
        {
            this.ontologyTerms.addSelected();
            this.classificationAndOnthologyTermsForm.$setDirty();
        }

        public removeOntologyTerm(id: string): void
        {
            this.ontologyTerms.remove(id);
            this.classificationAndOnthologyTermsForm.$setDirty();
        }

        public addSelectedTargetGroup(): void
        {
            this.targetGroups.addSelected();
            this.classificationAndOnthologyTermsForm.$setDirty();
        }

        public removeTargetGroup(id: string): void
        {
            this.targetGroups.remove(id);
            this.classificationAndOnthologyTermsForm.$setDirty();
        }

        public addSelectedLifeEvent(): void
        {
            this.lifeEvents.addSelected();
            this.classificationAndOnthologyTermsForm.$setDirty();
        }

        public removeLifeEvent(id: string): void
        {
            this.lifeEvents.remove(id);
            this.classificationAndOnthologyTermsForm.$setDirty();
        }

        public goToHomePage(): void
        {
            this.$location.path("/Organizations/" + this.organizationId + "/Services");
        }

        public canAddingBeCancelled(): boolean
        {
            return this.model == null || !this.model.isAdded();
        }

        public isServiceBeingEdited(): boolean
        {
            return this.editedSection !== EditedServiceSection.None;
        }

        public isBasicInformationBeingEdited(): boolean
        {
            return this.editedSection === EditedServiceSection.BasicInfromation;
        }

        public areClassificationAndOnthologyTermsBeingEdited(): boolean
        {
            return this.editedSection === EditedServiceSection.ClassificationAndOnthologyTerms;
        }

        public canSaveClassificationAndOnthologyTerms(): boolean
        {
            return this.serviceClasses != null && this.serviceClasses.areClassesAdded() &&
                this.ontologyTerms != null && this.ontologyTerms.areTermsAdded() &&
                this.targetGroups != null && this.targetGroups.areClassesAdded();
        }

        public editBasicInformation(): void
        {
            this.copyModelAndClassificationObjects();
            this.basicInformationForm.$setPristine();
            this.editedSection = EditedServiceSection.BasicInfromation;
        }

        public editClassificationAndOnthologyTerms(): void
        {
            this.copyModelAndClassificationObjects();
            this.initializeClassificationPredicates();
            this.classificationAndOnthologyTermsForm.$setPristine();
            this.editedSection = EditedServiceSection.ClassificationAndOnthologyTerms;
        }

        public cancelEditing(): void
        {
            this.resetModelAndClassificationObjects();
            this.selectedLanguages = this.languages.filter(this.model.languageCodes);
            this.editedSection = EditedServiceSection.None;
        }

        public saveEditedBasicInformation(): angular.IPromise<void>
        {
            this.editedSection = EditedServiceSection.None;
            this.model.languagesProperty = this.selectedLanguages;
            if (this.isModelChanged())
            {
                return this.saveBasicInformation();
            }
        }

        public saveBasicInformationAndQuit(): angular.IPromise<void>
        {
            if (this.isModelChanged())
            {
                return this.addServiceWithBasicInformation(true);
            }
            else
            {
                this.goToHomePage();
            }
        }

        public saveBasicInformationAndMoveToNextStep(): angular.IPromise<void>
        {
            this.editedSection = EditedServiceSection.ClassificationAndOnthologyTerms;
            if (this.isModelChanged())
            {
                return this.addServiceWithBasicInformation(false);
            }
        }

        public saveClassificationAndOnthologyTermsAndMoveToPreviousStep(): angular.IPromise<void>
        {
            this.editedSection = EditedServiceSection.BasicInfromation;
            this.setClassificationAndOntologyTerms();
            if (this.isModelChanged())
            {
                if (this.canSaveClassificationAndOnthologyTerms())
                {
                    return this.saveClassificationAndOnthologyTerms(false);
                }
                this.originalModel = angular.copy(this.model);
            }
        }

        public saveClassificationAndOnthologyTermsAndQuit(): angular.IPromise<void>
        {
            this.setClassificationAndOntologyTerms();
            if (this.isModelChanged())
            {
                return this.saveClassificationAndOnthologyTerms(true);
            }
            else
            {
                this.goToHomePage();
            }
        }

        public saveEditedClassificationAndOnthologyTerms(): angular.IPromise<void>
        {
            this.editedSection = EditedServiceSection.None;
            this.setClassificationAndOntologyTerms();
            if (this.isModelChanged())
            {
                return this.saveClassificationAndOnthologyTerms(false);
            }
        }

        private isModelChanged(): boolean
        {
            return !angular.equals(this.model, this.originalModel);
        }

        private addServiceWithBasicInformation(goToHomePage: boolean): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan palvelun perustietoja...");
            this.model.languagesProperty = this.selectedLanguages;
            this.model.generateBasicInformationLocalizedAndFormattedTexts();
            this.trustModelHtmlData();
            return this.serviceService.addService(this.organizationId, this.model)
                .then((serviceId: string) =>
                {
                    this.model.id = serviceId;
                    this.originalModel = angular.copy(this.model);
                    this.busyIndicationService.hideBusyIndicator();
                    if (goToHomePage)
                    {
                        this.goToHomePage();
                    }
                });
        }

        private saveClassificationAndOnthologyTerms(goToHomePage: boolean): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan palvelun luokitteluita ja ontologiasanoja...");
            this.model.generateKeywordsLocalizedTexts();
            this.originalModel = angular.copy(this.model);
            return this.serviceService.setServiceClassification(this.organizationId, this.model)
                .then(() =>
                {
                    this.busyIndicationService.hideBusyIndicator();
                    if (goToHomePage)
                    {
                        this.goToHomePage();
                    }
                });
        }

        private saveBasicInformation(): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Tallennetaan palvelun perustietoja...");
            this.model.generateBasicInformationLocalizedAndFormattedTexts();
            this.trustModelHtmlData();
            return this.serviceService.setServiceBasicInformation(this.organizationId, this.model)
                .then(() =>
                {
                    this.busyIndicationService.hideBusyIndicator();
                });
        }

        private initializeService($routeParams: IServiceRoute): angular.IPromise<void>
        {
            this.organizationId = $routeParams.organizationId;

            if ($routeParams.serviceId == null)
            {
                this.model = new Service();
                return this.fetchSelectionLists();
            }
            return this.fetchServiceAndSelectionLists($routeParams.serviceId);
        }

        private fetchServiceAndSelectionLists(serviceId: string): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Haetaan palvelun tietoja...");
            return this.$q.all([
                    this.serviceService.getService(this.organizationId, serviceId), this.settingsService.getServiceLanguages(), this.classificationService.getLifeEvents(),
                    this.classificationService.getServiceClasses(), this.classificationService.getTargetGroups()
                ])
                .then((results: Array<any>) =>
                {
                    this.model = results[0];
                    this.languages = new Languages(results[1]);
                    this.lifeEvents = new HierarchicalSelectionClassification(new Tree(results[2]), this.model.lifeEvents);
                    this.serviceClasses = new HierarchicalSelectionClassification(new Tree(results[3]), this.model.serviceClasses);
                    this.targetGroups = new HierarchicalSelectionClassification(new Tree(results[4]), this.model.targetGroups);
                    this.ontologyTerms = new OntologyTerms(this.model.ontologyTerms);
                    this.selectedLanguages = this.languages.filter(this.model.languageCodes);
                    this.trustModelHtmlData();
                    this.busyIndicationService.hideBusyIndicator();
                });
        }

        private fetchSelectionLists(): angular.IPromise<void>
        {
            this.busyIndicationService.showBusyIndicator("Haetaan palvelun tietoja...");
            this.ontologyTerms = new OntologyTerms();
            return this.$q.all([
                    this.settingsService.getServiceLanguages(), this.classificationService.getLifeEvents(),
                    this.classificationService.getServiceClasses(), this.classificationService.getTargetGroups()
                ])
                .then((results: Array<any>) =>
                {
                    this.languages = new Languages(results[0]);
                    this.selectedLanguages = new Array<Language>();
                    this.lifeEvents = new HierarchicalSelectionClassification(new Tree(results[1]));
                    this.serviceClasses = new HierarchicalSelectionClassification(new Tree(results[2]));
                    this.targetGroups = new HierarchicalSelectionClassification(new Tree(results[3]));
                    this.busyIndicationService.hideBusyIndicator();
                });
        }

        private initializeEditedSection($route: any): void
        {
            if ($route.current.locals.editedSection != null)
            {
                this.editedSection = $route.current.locals.editedSection;
            }
            else
            {
                this.editedSection = EditedServiceSection.None;
            }
        }

        private trustModelHtmlData(): void
        {
            this.model.descriptionAsHtml = this.$sce.trustAsHtml(this.model.descriptionAsHtml);
            this.model.shortDescriptionAsHtml = this.$sce.trustAsHtml(this.model.shortDescriptionAsHtml);
            this.model.userInstructionAsHtml = this.$sce.trustAsHtml(this.model.userInstructionAsHtml);
            this.model.requirementAsHtml = this.$sce.trustAsHtml(this.model.requirementAsHtml);
        }

        private initializeClassificationPredicates(): void
        {
            this.serviceClassPredicate = "";
            this.ontologyTermPredicate = "";
            this.targetGroupPredicate = "";
            this.lifeEventPredicate = "";
        }

        private copyModelAndClassificationObjects(): void
        {
            this.originalModel = angular.copy(this.model);
            this.originalLifeEvents = angular.copy(this.lifeEvents);
            this.originalOntologyTerms = angular.copy(this.ontologyTerms);
            this.originalServiceClasses = angular.copy(this.serviceClasses);
            this.originalTargetGroups = angular.copy(this.targetGroups);
        }

        private resetModelAndClassificationObjects(): void
        {
            this.model = this.originalModel;
            this.lifeEvents = this.originalLifeEvents;
            this.ontologyTerms = this.originalOntologyTerms;
            this.serviceClasses = this.originalServiceClasses;
            this.targetGroups = this.originalTargetGroups;
        }

        private setClassificationAndOntologyTerms(): void
        {
            this.model.setClassification(this.serviceClasses.addedCollection, this.targetGroups.addedCollection, this.lifeEvents.addedCollection);
            this.model.ontologyTerms = this.ontologyTerms.added;
        }

        private setExpandedClassification(classification: Classification, predicate: string)
        {
            if (predicate == null || predicate.length === 0)
            {
                classification.collapseAvailable();
            }
            else
            {
                classification.expandAvailable();
            }
        }
    }
}