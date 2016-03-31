"use strict";

module OrganizationRegister
{
    export var HelpPopupFactory: angular.IDirectiveFactory = ($sce: angular.ISCEService): HelpPopup =>
    {
        return new HelpPopup($sce);
    }

    HelpPopupFactory.$inject = ["$sce"];

    export class HelpPopup extends Affecto.Base.Directive
    {
        constructor(private $sce: angular.ISCEService)
        {
            super(Affecto.Base.DirectiveRestriction.Element, "App/Directives/HelpPopup/HelpPopup.html", true);
        }

        protected linkDirective($scope: IHelpPopupScope, element: JQuery, attributes?: angular.IAttributes, controller?: any): any
        {
            $scope.helpTextVisible = false;
            if (attributes != null)
            {
                $scope.paragraphs = new Array<string>();
                JSON.parse(attributes["paragraphs"]).forEach((paragraph: string) =>
                {
                    $scope.paragraphs.push(this.$sce.trustAsHtml(paragraph));
                });
            }
        }
    }
}
