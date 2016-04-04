"use strict";

module OrganizationRegister
{
    export class LocalizedText implements Affecto.Base.IModel
    {
        constructor(public languageCode: string, public localizedValue: string)
        {
        }
    }
}
 