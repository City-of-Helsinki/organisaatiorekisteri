"use strict";

module Affecto.Login
{
    export interface IAuthenticationService
    {
        logInWithCredentials(userName: string, password: string): angular.IPromise<void>;
        logInWithCookie(): angular.IPromise<void>;
        logInWithAccessToken(accessToken: string): angular.IPromise<void>;
        logOut(): void;
        isAuthenticated(): boolean;
        getAccessToken(): string;
        redirectToLoginPage(): void;
        getUser<T extends AuthenticatedUser>(): T;
    }

    export class AuthenticationService implements IAuthenticationService
    {
        public static $inject = ["$rootScope", "$http", "$q", "$location", "$window", "localStorageService", "tokenServiceUrl", "tokenServiceClientId", "tokenServiceClientSecret",
            "tokenServiceScope", "requestedRouteService", "useExternalLogin", "externalLoginPage", "requestedRouteUrlParameter", "loginRoute", "apiGetUserUrl",
            "authenticatedUserFactory"];

        private static authenticationStateStorageKey: string = "authenticationState";
        private authenticationState: AuthenticationState;

        constructor(private $rootScope: angular.IRootScopeService, private $http: angular.IHttpService, private $q: angular.IQService,
            private $location: angular.ILocationService, private $window: Window, private localStorageService: angular.local.storage.ILocalStorageService,
            private tokenServiceUrl: string, private tokenServiceClientId: string, private tokenServiceClientSecret: string, private tokenServiceScope: string,
            private requestedRouteService: RequestedRouteService, private useExternalLogin: boolean, private externalLoginPage: string,
            private requestedRouteUrlParameter: string, private loginRoute: string, private apiGetUserUrl: string, private authenticatedUserFactory: IAuthenticatedUserFactory)
        {
            this.authenticationState = null;
        }

        public logInWithCredentials(userName: string, password: string): angular.IPromise<void>
        {
            var grant: string = "grant_type=password&username=" + encodeURIComponent(userName)
                + "&password=" + encodeURIComponent(password);

            if (this.tokenServiceClientId != null && this.tokenServiceClientId !== "")
            {
                grant += "&client_id=" + encodeURIComponent(this.tokenServiceClientId);
            }

            if (this.tokenServiceClientSecret != null && this.tokenServiceClientSecret !== "")
            {
                grant += "&client_secret=" + encodeURIComponent(this.tokenServiceClientSecret);
            }

            if (this.tokenServiceScope != null && this.tokenServiceScope !== "")
            {
                grant += "&scope=" + encodeURIComponent(this.tokenServiceScope);
            }

            return this.logIn(grant, false);
        }

        public logInWithCookie(): angular.IPromise<void>
        {
            return this.logIn("grant_type=fedauth", true);
        }

        public logInWithAccessToken(accessToken: string): angular.IPromise<void>
        {
            this.setAccessToken(accessToken);
            return this.$http.get(this.apiGetUserUrl)
                .then((response: angular.IHttpPromiseCallbackArg<any>) =>
                {
                    if (response != null && response.data != null)
                    {
                        this.setAuthenticatedUser(this.authenticatedUserFactory.createUser(response.data));
                        this.$rootScope.$broadcast(Events.userLoggedIn);
                    }
                });
        }

        public logOut(): void
        {
            this.clearAuthenticationState();
            this.requestedRouteService.removeRoute();
            this.$rootScope.$broadcast(Events.userLoggedOut);
        }

        public isAuthenticated(): boolean
        {
            return (this.getAccessToken() != null);
        }

        public getAccessToken(): string
        {
            return this.getAuthenticationState().accessToken;
        }

        public getUser<T extends AuthenticatedUser>(): T
        {
            return <T> this.getAuthenticationState().user;
        }

        public redirectToLoginPage(): void
        {
            if (!this.useExternalLogin)
            {
                var location: angular.ILocationService = this.$location.path(this.loginRoute);
                var requestedRoute: string = this.requestedRouteService.route;

                if (requestedRoute != null)
                {
                    location.search(this.requestedRouteUrlParameter, requestedRoute);
                }
            }
            else
            {
                this.$window.location.href = HtmlContent.unescape(this.externalLoginPage);
            }
        }

        private logIn(grant: string, includeCredentials: boolean): angular.IPromise<void>
        {
            this.clearAuthenticationState();

            return this.$http
                .post(this.tokenServiceUrl, grant,
                {
                    headers: { "Content-Type": "application/x-www-form-urlencoded" },
                    withCredentials: includeCredentials
                })
                .error((data: any): void =>
                {
                    var deferred: angular.IDeferred<any> = this.$q.defer();
                    deferred.reject(data);
                })
                .then((response: angular.IHttpPromiseCallbackArg<any>) =>
                {
                    return this.logInWithAccessToken(response.data.access_token);
                });
        }

        private getAuthenticationState(): AuthenticationState
        {
            if (this.authenticationState != null)
            {
                return this.authenticationState;
            }

            var state: AuthenticationState = this.localStorageService.get<AuthenticationState>(AuthenticationService.authenticationStateStorageKey);
            if (state != null && state.user != null)
            {
                var user: AuthenticatedUser = this.authenticatedUserFactory.createUser(state.user);
                this.authenticationState = new AuthenticationState(state.accessToken, user);
            }

            if (this.authenticationState == null)
            {
                this.authenticationState = new AuthenticationState();
            }

            return this.authenticationState;
        }

        private setAuthenticationState(state: AuthenticationState): void
        {
            this.localStorageService.set(AuthenticationService.authenticationStateStorageKey, state);
        }

        private setAccessToken(token: string): void
        {
            var authenticationState: AuthenticationState = this.getAuthenticationState();
            authenticationState.accessToken = token;
            this.setAuthenticationState(authenticationState);
        }

        private setAuthenticatedUser(user: AuthenticatedUser): void
        {
            var authenticationState: AuthenticationState = this.getAuthenticationState();
            authenticationState.user = user;
            this.setAuthenticationState(authenticationState);
        }

        private clearAuthenticationState(): void
        {
            this.authenticationState = null;
            this.localStorageService.remove(AuthenticationService.authenticationStateStorageKey);
        }
    }

    class AuthenticationState
    {
        constructor(public accessToken?: string, public user?: AuthenticatedUser)
        {
        }

        public get isAuthenticated()
        {
            return (this.accessToken != null && this.accessToken !== "" && this.user != null);
        }
    }
}