"use strict";

module Affecto.ExceptionHandling
{
    export class ErrorController extends Base.Controller
    {
        public static $inject = ["$scope", "$location", "$translate", "busyIndicationService"];

        public userName: string;
        public password: string;
        private errorCode: string;

        constructor(private $scope: Base.IControllerScope, private $location: angular.ILocationService, private $translate: angular.translate.ITranslateService,
            busyIndicationService: BusyIndication.IBusyIndicationService)
        {
            super($scope);
            $scope.controller = this;
            busyIndicationService.hideBusyIndicator();
            this.errorCode = $location.search()[UrlParameters.errorCode];
        }

        public get errorMessage(): string
        {
          
           
            var code: string = "ERROR_" + this.errorCode;
            var error: string = this.$translate.instant(code);

            if (this.errorCode == null || error === code)
            {
                error = this.$translate.instant("ERROR_UNDEFINED");
            }
            return error;
        }
    }
}