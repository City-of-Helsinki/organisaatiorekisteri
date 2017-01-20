using System;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.UserManagement.Model
{
    internal class UserListItem : IUserListItem
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public IRole Role { get; set; }
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsDisabled { get; set; }
    }
}