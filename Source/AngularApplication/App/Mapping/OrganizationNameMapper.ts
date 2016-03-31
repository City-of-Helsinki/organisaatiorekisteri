"use strict";

module OrganizationRegister
{
    export class OrganizationNameMapper
    {
        public static map(data: any): Array<OrganizationName>
        {
            var result: Array<OrganizationName> = new Array<OrganizationName>();
            data.forEach((item: any) =>
            {
                result.push(this.mapSingle(item));
            });
            return result;
        }

        public static mapSingle(data: any): OrganizationName
        {
            var names: Array<LocalizedText> = LocalizedTextMapper.map(data.names);
            return new OrganizationName(data.id, names);   
        }
    }
} 