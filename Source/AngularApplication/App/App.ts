"use strict";

var organizationRegisterApplication: angular.IModule = createApplication();

organizationRegisterApplication.config([
    "$httpProvider", ($httpProvider: angular.IHttpProvider) =>
    {
        //initialize common if not there
        if (!$httpProvider.defaults.headers.common)
        {
            $httpProvider.defaults.headers.common = { };
        }    
  
        //disable IE xhr request caching
        $httpProvider.defaults.cache = false;
        $httpProvider.defaults.headers.common["If-Modified-Since"] = "Mon, 26 Jul 1997 05:00:00 GMT";
        $httpProvider.defaults.headers.common["Cache-Control"] = "no-cache";
        $httpProvider.defaults.headers.common.Pragma = "no-cache";
    }
]);

organizationRegisterApplication.config(["$routeProvider", ($routeProvider: angular.route.IRouteProvider) =>
    {
        registerRoutes($routeProvider);
    }
]);


organizationRegisterApplication.config([
    "$translateProvider", ($translateProvider: angular.translate.ITranslateProvider) =>
    {
        $translateProvider
            .translations("fi", createTranslations())
            .preferredLanguage("fi");
    }
]);  

organizationRegisterApplication.config([
    "unsavedWarningsConfigProvider", (unsavedWarningsConfigProvider: any) =>
    {
        unsavedWarningsConfigProvider.navigateMessage = "UNSAVED_CHANGES_ON_PAGE_LEAVE";
        unsavedWarningsConfigProvider.reloadMessage = "UNSAVED_CHANGES_ON_PAGE_RELOAD";
    }
]);

organizationRegisterApplication.constant("handledErrorCodes", []);
initializeLoginConstants(OrganizationRegister.UrlParameter.requestedRoute, OrganizationRegister.Route.login);

organizationRegisterApplication.run([
    "$rootScope", "authenticationService", "requestedRouteService", "$location",
    ($rootScope: any, authenticationService: Affecto.Login.IAuthenticationService, requestedRouteService: Affecto.Login.RequestedRouteService,
        $location: angular.ILocationService) =>
    {
        $rootScope.EditedOrganizationSection = OrganizationRegister.EditedOrganizationSection;
        $rootScope.$on("$routeChangeStart", (event: angular.IAngularEvent, next: any, current: any) =>
        {
            var currentPath: string = $location.path();
            if (currentPath !== Affecto.ExceptionHandling.Routes.error && currentPath !== OrganizationRegister.Route.login && !authenticationService.isAuthenticated())
            {
                event.preventDefault();
                requestedRouteService.route = currentPath;
                authenticationService.redirectToLoginPage();
            }
        });
    }
]);

registerControllers();
registerServices();
registerDirectives();

function createApplication(): angular.IModule
{
    var applicationModules: Array<string> = new Array<string>();
    var referenceModules: Array<string> = [
        "ngRoute", "ngResource", "ngCookies", "pascalprecht.translate", "unsavedChanges", "localytics.directives", "Affecto.BusyIndication", "OrganizationRegister.Settings",
        "Affecto.ExceptionHandling", "Affecto.Login", "treeControl"
    ];
    applicationModules.forEach(mod => referenceModules.push(mod));

    return angular.module("OrganizationRegister", referenceModules);
}

function registerRoutes($routeProvider: angular.route.IRouteProvider): void
{
    $routeProvider
        .when("/",
        {
            controller: "OrganizationRegister.OrganizationTreeController",
            templateUrl: "App/Views/Index.html"
        })
        .when("/Organizations/:organizationId/NewService",
            {
            controller: "OrganizationRegister.ServiceController",
            templateUrl: "App/Views/AddServiceWizard.html",
            resolve:
                {
                    editedSection: () =>
                    {
                        return OrganizationRegister.EditedServiceSection.BasicInfromation;
                    }
                }
        })
        .when("/Organizations/:organizationId/Services",
        {
            controller: "OrganizationRegister.ServiceSearchController",
            templateUrl: "App/Views/Services.html"
        })
        .when("/Organizations/:organizationId/Services/:serviceId",
        {
            controller: "OrganizationRegister.ServiceController",
            templateUrl: "App/Views/Service.html"
        })
        .when(OrganizationRegister.Route.login,
        {
            controller: "OrganizationRegister.LoginController",
            templateUrl: "App/Views/Login.html"
        })
        .when("/Organizations/:organizationId/Users",
        {
            controller: "OrganizationRegister.UserSearchController",
            templateUrl: "App/Views/Users.html"
        })
        .when("/Organizations/:organizationId/NewUser",
        {
            controller: "OrganizationRegister.UserController",
            templateUrl: "App/Views/AddUser.html"
        })
        .when("/Organizations",
        {
            controller: "OrganizationRegister.OrganizationController",
            templateUrl: "App/Views/AddOrganizationWizard.html",
            resolve:
            {
                editedSection: () =>
                {
                    return OrganizationRegister.EditedOrganizationSection.BasicInfromation;
                }
            }
})
        .when("/Organizations/:organizationId",
        {
            templateUrl: "App/Views/Organization.html"
        })
        .when("/Organizations/:parentOrganizationId/Organizations",
        {
            controller: "OrganizationRegister.OrganizationController",
            templateUrl: "App/Views/AddOrganizationWizard.html",
            resolve:
            {
                editedSection: () =>
                {
                    return OrganizationRegister.EditedOrganizationSection.BasicInfromation;
                }
            }
        })
        .when(Affecto.ExceptionHandling.Routes.error,
        {
            controller: "Affecto.ExceptionHandling.ErrorController",
            templateUrl: "App/Views/Error.html"
        })
        .otherwise(
        {
            redirectTo: "/"
        });
}

function registerControllers(): void
{
    Affecto.Registration.registerController(OrganizationRegister.MainNavigationController, "OrganizationRegister.MainNavigationController");
    Affecto.Registration.registerController(OrganizationRegister.OrganizationController, "OrganizationRegister.OrganizationController");
    Affecto.Registration.registerController(OrganizationRegister.OrganizationTreeController, "OrganizationRegister.OrganizationTreeController");
    Affecto.Registration.registerController(OrganizationRegister.UserController, "OrganizationRegister.UserController");
    Affecto.Registration.registerController(OrganizationRegister.ServiceController, "OrganizationRegister.ServiceController");
    Affecto.Registration.registerController(OrganizationRegister.ServiceSearchController, "OrganizationRegister.ServiceSearchController");
    Affecto.Registration.registerController(OrganizationRegister.LoginController, "OrganizationRegister.LoginController");
    Affecto.Registration.registerController(OrganizationRegister.UserSearchController, "OrganizationRegister.UserSearchController");
}

function registerServices(): void
{
    Affecto.Registration.registerService(OrganizationRegister.OrganizationService, "OrganizationRegister.OrganizationService");
    Affecto.Registration.registerService(OrganizationRegister.SettingsService, "OrganizationRegister.SettingsService");
    Affecto.Registration.registerService(OrganizationRegister.ValidationService, "OrganizationRegister.ValidationService");
    Affecto.Registration.registerService(OrganizationRegister.UserService, "OrganizationRegister.UserService");
    Affecto.Registration.registerService(OrganizationRegister.AuthenticatedUserFactory, "OrganizationRegister.AuthenticatedUserFactory");
    Affecto.Registration.registerService(OrganizationRegister.ServiceService, "OrganizationRegister.ServiceService");
    Affecto.Registration.registerService(OrganizationRegister.ClassificationService, "OrganizationRegister.ClassificationService");
}

function registerDirectives(): void
{
    Affecto.Registration.registerDirectiveFactory(OrganizationRegister.IgnoreDirtyFormFieldFactory, "OrganizationRegister.IgnoreDirtyFormField");
    Affecto.Registration.registerDirectiveFactory(OrganizationRegister.HelpPopupFactory, "OrganizationRegister.HelpPopup");
};

function createTranslations(): angular.translate.ITranslationTable
{
    var translations: angular.translate.ITranslationTable = {};
    translations["ERROR_UNDEFINED"] = "Tapahtui tunnistamaton virhe";
    translations["ERROR_" + OrganizationRegister.ErrorCode.insufficientPermissions] = "Ei käyttöoikeuksia";
    translations["UNSAVED_CHANGES_ON_PAGE_LEAVE"] = "Tallentamattomat muutokset menetetään poistuttaessa sivulta.";
    translations["UNSAVED_CHANGES_ON_PAGE_RELOAD"] = "Tallentamattomat muutokset menetetään sivun uudelleenlatauksessa.";
    translations[OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SameAsVisitingAddress]] = "Sama kuin käyntiosoite";
    translations[OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SeparateStreetAddress]] = "Muu osoite";
    translations[OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.PostOfficeBoxAddress]] = "PL-osoite";
    return translations;
}