"use strict";

module OrganizationRegister
{
    export interface IHelpPopupScope extends angular.IScope
    {
        paragraphs: Array<string>;
        helpTextVisible: boolean;
    }
} 