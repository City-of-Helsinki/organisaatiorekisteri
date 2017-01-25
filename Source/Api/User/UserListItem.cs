using System;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Api.Settings;

namespace OrganizationRegister.Api.User
{
    public class UserListItem
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
        public OrganizationName Organization { get; set; }
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsDisabled { get; set; }
    }
}