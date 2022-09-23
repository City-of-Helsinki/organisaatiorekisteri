"use strict";

module Affecto.Login
{
    export interface IAuthenticationService
    {
        tunnistamoLogInWithCode(code: string): angular.IPromise<void>;

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

        public tunnistamoLogInWithCode(code: string): angular.IPromise<void>
        {
            console.log("tunnistamologInWithCode");
            this.setAccessToken("yJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IlJ1UjRBVWNrelZQcGlsOWdBcHZRZUNjWHNlbyIsImtpZCI6IlJ1UjRBVWNrelZQcGlsOWdBcHZRZUNjWHNlbyJ9.eyJpc3MiOiJodHRwczovL2hraWRldnB0di5kZXYubG9jYWwvQXV0aGVudGljYXRpb25TZXJ2ZXIvY29yZSIsImF1ZCI6Imh0dHBzOi8vaGtpZGV2cHR2LmRldi5sb2NhbC9BdXRoZW50aWNhdGlvblNlcnZlci9jb3JlL3Jlc291cmNlcyIsImV4cCI6MTYyODA4MDE1NiwibmJmIjoxNjI3OTkzNzU3LCJjbGllbnRfaWQiOiJPckludGVybmFsVUkiLCJzY29wZSI6Ik9yQXBpIiwic3ViIjoiZDdjZThjMTQtOTMxZC00MzNiLWI3NDEtNmRkODFjZTAyMzIwIiwiYXV0aF90aW1lIjoxNjI3OTkzNzU3LCJpZHAiOiJpZHNydiIsIm5hbWUiOiJIS0kgSsOkcmplc3RlbG3DpG4gcMOkw6Rrw6R5dHTDpGrDpCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiZDdjZThjMTQtOTMxZC00MzNiLWI3NDEtNmRkODFjZTAyMzIwIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkhLSSBKw6RyamVzdGVsbcOkbiBww6TDpGvDpHl0dMOkasOkIiwiaHR0cDovL2FmZmVjdG8uY29tL2NsYWltcy9hY2NvdW50bmFtZSI6InN1cGVyYWRtaW5AaGVsLmZpIiwiaHR0cDovL2FmZmVjdG8uY29tL2NsYWltcy9jdXN0b21wcm9wZXJ0aWVzL09yZ2FuaXphdGlvbklkIjoiODNlNzQ2NjYtMDgzNi00YzFkLTk0OGEtNGIzNGE4YjkwMzAxIiwiaHR0cDovL2FmZmVjdG8uY29tL2NsYWltcy9jdXN0b21wcm9wZXJ0aWVzL0xhc3ROYW1lIjoiSEtJIiwiaHR0cDovL2FmZmVjdG8uY29tL2NsYWltcy9jdXN0b21wcm9wZXJ0aWVzL0VtYWlsQWRkcmVzcyI6InN1cGVyYWRtaW5AaGVsLmZpIiwiaHR0cDovL2FmZmVjdG8uY29tL2NsYWltcy9jdXN0b21wcm9wZXJ0aWVzL0ZpcnN0TmFtZSI6IkrDpHJqZXN0ZWxtw6RuIHDDpMOka8OkeXR0w6Rqw6QiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJKw6RyamVzdGVsbcOkbiBww6TDpGvDpHl0dMOkasOkIiwiaHR0cDovL2FmZmVjdG8uY29tL2NsYWltcy9wZXJtaXNzaW9uIjpbIk1BSU5URU5BTkNFX09GX09XTl9PUkdBTklaQVRJT05fVVNFUlMiLCJNQUlOVEVOQU5DRV9PRl9BTExfVVNFUlMiLCJVU0VSX01BSU5URU5BTkNFIiwiT1JHQU5JWkFUSU9OX0RBVEFfTUFJTlRFTkFOQ0UiLCJNQUlOVEVOQU5DRV9PRl9PV05fT1JHQU5JWkFUSU9OX0RBVEEiLCJNQUlOVEVOQU5DRV9PRl9BTExfT1JHQU5JWkFUSU9OX0RBVEEiXSwiYW1yIjpbInBhc3N3b3JkIl19.bG - yr7 - i6fvtb6ENaMpjEYQecXjIQ_5hjib7EULL1gIEhbFaFv6xCGQw0oCj2bH6rBa6gEMzYByp0WHLd4SSFuMSBjgy7aGfNl82 - _TqHJcLb - PmRHgzqtk8DIHTw7YwhhfwhtQBoR1jyAtHDX6p0PLQDF6b6cBuZrwtXHxmwoMPhEqT367yYkcePlOKS4Oa2B1uWWyO2dy35_6WAz5jbItPo5mmg7jz8AJevVB7MyevCj-jln8cXzveMn_Pbdv84mTj0OOcS0IJb-3vSzgto9xO4wJ3VrxRnVqE0hoB8VucNGI1EO4Z3jv9kJVA9-15QVBHPZST6OgEWrfuhaAo1A");
            var user = this.authenticatedUserFactory.createUser(code);
            console.log("User");
            console.log(user);
            var authenticatedUser = this.setAuthenticatedUser(this.authenticatedUserFactory.createUser(user));
            console.log("AuthenticatedUser");
            console.log(authenticatedUser);
            this.$rootScope.$broadcast(Events.userLoggedIn);



            return 
        }

        public logInWithCredentials(userName: string, password: string): angular.IPromise<void>
        {
            console.log("logInWithCredentials");

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
            console.log("logInWithAccessToken");
            console.log("accessToken: " + accessToken);
            console.log("apiGetUserUrl: " + this.apiGetUserUrl);

            this.setAccessToken(accessToken);
            return this.$http.get(this.apiGetUserUrl)
                .then((response: angular.IHttpPromiseCallbackArg<any>) =>
                {
                    if (response != null && response.data != null)
                    {
                        console.log("response.data: " + response.data);
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

            console.log("logIn");
            console.log("tokenServiceUrl: " + this.tokenServiceUrl);
            console.log("grant: " + grant);

          

            var r = this.$http
                .post(this.tokenServiceUrl, grant,
                {
                    headers: { "Content-Type": "application/x-www-form-urlencoded", "Accept": "application/json" },
                    withCredentials: includeCredentials
                })
                .error((data: any): void =>
                {
                    console.log("logIn Error");
                    var deferred: angular.IDeferred<any> = this.$q.defer();
                    deferred.reject(data);
                })
                .then((response: angular.IHttpPromiseCallbackArg<any>) =>
                {
                    console.log("logIn success");
                    return this.logInWithAccessToken(response.data.access_token);
                });

            console.log(r);

            return r;
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