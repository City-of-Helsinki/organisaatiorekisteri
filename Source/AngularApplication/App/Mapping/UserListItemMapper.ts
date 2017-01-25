"use strict";

module OrganizationRegister
{
    export class UserListItemMapper
    {
        public static map(data: any): Array<UserListItem>
        {
            var result: Array<UserListItem> = new Array<UserListItem>();
            data.forEach((item: any) =>
            {
                result.push(this.mapSingle(item));
            });
            return result;
        }

        public static mapSingle(data: any): UserListItem
        {
            var organization: OrganizationName = OrganizationNameMapper.mapSingle(data.organization);
            var role: UserRole = UserRoleMapper.mapSingle(data.role);
            return new UserListItem(data.id, role, organization, data.emailAddress, data.lastName, data.firstName, data.isDisabled);
        }
    }
}