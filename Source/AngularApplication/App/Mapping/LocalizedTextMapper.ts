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
            return result;
        }
    }
}
 