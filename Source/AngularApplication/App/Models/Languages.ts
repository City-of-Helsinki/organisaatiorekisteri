"use strict";

module OrganizationRegister
{
    export class Languages
    {
        constructor(public value: Array<Language>)
        {
        }

        public get codes(): Array<string>
        {
            if (this.value != null)
            {
                return this.value.map((lang: Language) =>
                {
                    return lang.code;
                });                
            }
            return new Array<string>();
        }

        public get commaSeparatedNames(): string
        {
            if (this.value != null)
            {
                return this.value.map((lang: Language) =>
                {
                    return lang.name;
                }).join(", ");                
            }
            return "";
        }

        public filter(codes: Array<string>): Array<Language>
        {
            if (this.value != null && codes != null)
            {
                return this.value.filter((lang: Language) =>
                {
                    return codes.some((code: string) => code === lang.code);
                });                
            }
            return new Array<Language>();
        }
    }
}
 