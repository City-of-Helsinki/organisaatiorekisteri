using System;

namespace OrganizationRegister.Application.User
{
    public interface IRole
    {
        Guid Id { get; }
        string Name { get; }
    }
}