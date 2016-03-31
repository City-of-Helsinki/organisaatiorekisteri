"use strict";

module OrganizationRegister
{
    export class ServiceClassificationCommand
    {
        constructor(public serviceClasses: Array<string>, public ontologyTerms: Array<string>, public targetGroups: Array<string>, public lifeEvents: Array<string>,
            public keywords: Array<LocalizedText>)
        {
        }
    }
}