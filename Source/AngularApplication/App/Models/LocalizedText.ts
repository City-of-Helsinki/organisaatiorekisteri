"use strict";

module OrganizationRegister
{
    export class LocalizedText implements Affecto.Base.IModel
    {
        private locTitle: string = "";
        
        constructor(public languageCode: string, public localizedValue: string, public isRequired?: boolean)
        {
        }

        public get localizedTitle(): string
        {

            // TODO: Get these from Api

            let title: string = "";

            if (this.locTitle === "")
            {
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
            }
            else
            {
                title = this.locTitle;
            }

            if (this.isRequired)
                title += "*";
           
            return title;
        }

        public set localizedTitle(value: string)
        {
            this.locTitle = value;
        }

       
    }
}