"use strict";

module OrganizationRegister
{
    export class UserRoleMapper
    {
        public static map(data: any): Array<UserRole>
        {
            var result: Array<UserRole> = new Array<UserRole>();
            data.forEach((item: any) =>
            {
                result.push(this.mapSingle(item));
            });
            return result;
        }

        public static mapSingle(data: any): UserRole
        {
            return new UserRole(data.id, data.name);
        }
    }
}