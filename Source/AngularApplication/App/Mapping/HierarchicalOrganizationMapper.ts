"use strict";

module OrganizationRegister
{
    export class HierarchicalOrganizationMapper
    {
        public static map(data: any): Array<Hierarchical>
        {
            var result: Array<Hierarchical> = new Array<Hierarchical>();
            data.forEach((item: any) =>
            {
                var names: Array<LocalizedText> = LocalizedTextMapper.map(item.names);
                result.push(new Hierarchical(item.id, names[0].localizedValue, this.map(item.subOrganizations)));
            });
            return result;
        }
    }
} 