"use strict";

module OrganizationRegister
{
    export class LanguageMapper
    {
        public static map(data: any): Array<Language>
        {
            var result: Array<Language> = new Array<Language>();
            data.forEach((item: any) =>
            {
                result.push(new Language(item.code, item.name));
            });
            return result;
        }
    }
}
 