"use strict";

module OrganizationRegister
{
    export class LocalizedTextMapper
    {
        public static map(data: any): Array<LocalizedText>
        {
            var result: Array<LocalizedText> = new Array<LocalizedText>();
            data.forEach((item: any) =>
            {
                result.push(new LocalizedText(item.languageCode, item.localizedValue));
            });
            return result.sort((a, b) =>
           {
               if (DataLocalization.languageCodes.indexOf(a.languageCode) > DataLocalization.languageCodes.indexOf(b.languageCode))
               {
                   return 1;
               }
               if (DataLocalization.languageCodes.indexOf(a.languageCode) < DataLocalization.languageCodes.indexOf(b.languageCode))
               {
                   return -1;
               }
               return 0;
           });;;
        }
    }
}
 