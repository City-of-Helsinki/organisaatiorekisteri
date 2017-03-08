"use strict";

module OrganizationRegister
{
    export class Hierarchical implements Affecto.Base.IModel
    {
        constructor(public id: string, public name: string, public children: Array<Hierarchical>, public validFrom: string, public validTo: string)
        {
        }
    }
}   