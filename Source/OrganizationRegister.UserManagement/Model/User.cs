using System;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.UserManagement.Model
{
    internal class User :  IUser
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid RoleId { get; set; }

        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDisabled { get; set; }

    }
}