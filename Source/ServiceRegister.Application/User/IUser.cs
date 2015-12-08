﻿using System;

namespace ServiceRegister.Application.User
{
    public interface IUser
    {
        Guid Id { get; }
        Guid OrganizationId { get; }
        Guid RoleId { get; }
        string EmailAddress { get; }
        string LastName { get; }
        string FirstName { get; }
        string PhoneNumber { get; }
    }
}