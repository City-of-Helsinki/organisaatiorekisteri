using System;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.UserManagement.Model
{
    internal class Role : IRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}