"use strict";

var loginModule: angular.IModule = angular.module("Affecto.Login", ["LocalStorageModule"]);

loginModule.config([
    "$httpProvider", ($httpProvider: angular.IHttpProvider) =>
    {
        $httpProvider.interceptors.push("authorizationInterceptorService");
    }
]);

loginModule.config([
    "localStorageServiceProvider", (localStorageServiceProvider: angular.local.storage.ILocalStorageServiceProvider) =>
    {
        localStorageServiceProvider
            .setStorageType("sessionStorage")
            .setPrefix("OrganizationRegister")
            .setNotify(false, false);
    }
]);

Affecto.Registration.registerService(Affecto.Login.RequestedRouteService, "Affecto.Login.RequestedRouteService");
Affecto.Registration.registerService(Affecto.Login.AuthenticationService, "Affecto.Login.AuthenticationService");
Affecto.Registration.registerService(Affecto.Login.AuthorizationInterceptorService, "Affecto.Login.AuthorizationInterceptorService");
Affecto.Registration.registerService(Affecto.Login.AuthenticatedUserFactory, "Affecto.Login.AuthenticatedUserFactory");

function initializeLoginConstants(requestedRouteUrlParameter: string, loginRoute: string): void
{
    loginModule.constant("requestedRouteUrlParameter", requestedRouteUrlParameter);
    loginModule.constant("loginRoute", loginRoute);
    loginModule.constant("useExternalLogin", false);
    loginModule.constant("externalLoginPage", "");
}