using System;

namespace OrganizationRegister.Application.User
{
    public interface IUserListItem
    {
        Guid Id { get; }
        Guid OrganizationId { get; }
        IRole Role { get; }
        string EmailAddress { get; }
        string LastName { get; }
        string FirstName { get; }
        bool IsDisabled { get; }
    }
}