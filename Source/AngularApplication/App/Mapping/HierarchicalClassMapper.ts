"use strict";

module OrganizationRegister
{
    export class HierarchicalClassMapper
    {
        public static map(data: any): Array<Hierarchical>
        {
            var result: Array<Hierarchical> = new Array<Hierarchical>();
            if (data != null)
            {
                data.forEach((item: any) =>
                {
                    result.push(new Hierarchical(item.id, item.name, this.map(item.subClasses)));
                });                
            }
            return result;
        }
    }
}