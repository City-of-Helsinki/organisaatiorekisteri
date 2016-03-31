"use strict";

module OrganizationRegister
{
    export class ServiceListItem
    {
        public name: string;

        constructor(public id: string, names: Array<LocalizedText>, public serviceClasses: string, public ontologyTerms: string)
        {
            if (names != null)
            {
                this.name = names[0].localizedValue;
            }
        }
    }
}  