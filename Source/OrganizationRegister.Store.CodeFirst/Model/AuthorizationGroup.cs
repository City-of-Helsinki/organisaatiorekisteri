using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class AuthorizationGroup
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public Guid RoleId { get; set; }

        public Organization Organization { get; set; }
    }
}