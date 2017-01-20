using System;

namespace OrganizationRegister.Api.User
{
    public class User
    {
        public string EmailAddress { get; set; }
       public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid RoleId { get; set; }
        public Guid Id { get; set; }
        public bool IsDisabled { get; set; }
    }
}


