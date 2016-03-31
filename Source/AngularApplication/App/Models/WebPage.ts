"use strict";

module OrganizationRegister
{
    export class WebPage
    {
        public address: string;

        constructor(public name: string, addressWithOptionalProtocol: string, public type: string)
        {
            if (addressWithOptionalProtocol.indexOf("http") === 0)
            {
                this.address = addressWithOptionalProtocol;
            }
            else
            {
                this.address = "http://" + addressWithOptionalProtocol;
            }
        }
    }
}