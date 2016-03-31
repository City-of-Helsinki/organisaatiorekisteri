"use strict";

module OrganizationRegister
{
    export class ServiceListItemMapper
    {
        public static map(data: any): Array<ServiceListItem>
        {
            var result: Array<ServiceListItem> = new Array<ServiceListItem>();
            data.forEach((item: any) =>
            {
                var names: Array<LocalizedText> = LocalizedTextMapper.map(item.names);
                result.push(new ServiceListItem(item.id, names, item.serviceClasses, item.ontologyTerms));
            });
            return result;
        }
    }
} 