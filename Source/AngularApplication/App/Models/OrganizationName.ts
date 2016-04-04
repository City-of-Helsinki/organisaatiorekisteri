"use strict";

module OrganizationRegister
{
    export class OrganizationName implements Affecto.Base.IModel
    {
        public name: string;

        constructor(public id: string, private names: Array<LocalizedText>)
        {
            if (names != null)
            {
                this.name = names[0].localizedValue;
            }
        }
    }
}  