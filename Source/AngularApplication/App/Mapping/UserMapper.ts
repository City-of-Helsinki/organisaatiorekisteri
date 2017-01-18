"use strict";

module OrganizationRegister
{
    export class UserMapper
    {
        public static map(data: any): User
        {
            
            return new User(data.id,
                data.emailAddress,
                data.password,
                null,
                data.lastName,
                data.firstName,
                data.phoneNumber,
                data.organizationId,
                data.roleId);

        }
    }
}