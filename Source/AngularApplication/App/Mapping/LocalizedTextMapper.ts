"use strict";

module OrganizationRegister
{
    export class LocalizedTextMapper
    {
        public static map(data: any): Array<LocalizedText>
        {
            const langs = DataLocalization.languageCodes;
            var result: Array<LocalizedText> = new Array<LocalizedText>();
            data.forEach((item: any) =>
            {
                result.push(new LocalizedText(item.languageCode, item.localizedValue));
            });
            return result.sort((a, b) =>
            {
                if (langs.indexOf(a.languageCode) > langs.indexOf(b.languageCode))
                {
                    return 1;
                }
                if (langs.indexOf(a.languageCode) < langs.indexOf(b.languageCode))
                {
                    return -1;
                }
                return 0;
            });
        }
    }
}
 