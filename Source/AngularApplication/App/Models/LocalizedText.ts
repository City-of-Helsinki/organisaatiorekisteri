"use strict";

module OrganizationRegister
{
    export class LocalizedText implements Affecto.Base.IModel
    {
        constructor(public languageCode: string, public localizedValue: string, public isRequired?: boolean)
        {
        }

        public get localizedTitle(): string
        {

            // TODO: Get these from Api
            let title: string = "";
            switch (this.languageCode)
            {
                case "fi":
                    title = "suomeksi";
                    break;
                case "sv":
                    title = "ruotsiksi";
                    break;
                case "en":
                    title = "englanniksi";
                    break;
            }
            if (this.isRequired)
                title += "*";
            return title;
        }

    }
}