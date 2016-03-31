"use strict";

module OrganizationRegister
{
    export class User implements Affecto.Base.IModel
    {
        constructor(public emailAddress?: string, public password?: string, public passwordConfirm?: string, public lastName?: string, public firstName?: string,
            public phoneNumber?: string, public organizationId?: string, public roleId?: string)
        {
        }

        public get roleIdProperty(): string
        {
            return this.roleId;
        }

        public set roleIdProperty(value: string)
        {
            this.roleId = value;
        }

        public get organizationIdProperty(): string
        {
            return this.organizationId;
        }

        public set organizationIdProperty(value: string)
        {
            this.organizationId = value;
        }

        public hasPhoneNumber(): boolean
        {
            return this.phoneNumber != null && this.phoneNumber !== "";
        }

        public hasEmailAddress(): boolean
        {
            return this.emailAddress != null && this.emailAddress !== "";
        }

        public hasPassword(): boolean
        {
            return this.password != null && this.password !== "";
        }

        public hasBothPasswords(): boolean
        {
            return this.hasPassword() && this.passwordConfirm != null && this.passwordConfirm !== "";
        }
    }
}