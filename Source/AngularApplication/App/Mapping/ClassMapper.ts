"use strict";

module OrganizationRegister
{
    export class ClassMapper
    {
        public static map(data: any): Array<Class>
        {
            var result: Array<Class> = new Array<Class>();
            if (data != null)
            {
                data.forEach((item: any) =>
                {
                    result.push(new Class(item.id, item.name));
                });
            }
            return result;
        }
    }
}  