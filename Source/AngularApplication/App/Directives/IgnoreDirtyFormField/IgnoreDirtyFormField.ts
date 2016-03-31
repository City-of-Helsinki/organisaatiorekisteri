"use strict";

module OrganizationRegister
{
    export var IgnoreDirtyFormFieldFactory: angular.IDirectiveFactory = (): IgnoreDirtyFormField =>
    {
        return new IgnoreDirtyFormField();
    }

    export class IgnoreDirtyFormField extends Affecto.Base.Directive
    {
        constructor()
        {
            super(Affecto.Base.DirectiveRestriction.Attribute, null, null, "ngModel");
        }

        protected linkDirective($scope: angular.IScope, element: JQuery, attributes?: angular.IAttributes, controller?: any): any
        {
            controller.$setPristine = () => { };
            controller.$pristine = false;
        }
    }
}
