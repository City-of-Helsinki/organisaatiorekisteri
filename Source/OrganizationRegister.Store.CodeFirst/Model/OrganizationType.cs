using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class OrganizationType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SourceId { get; set; }
        public int? OrderNumber { get; set; }
    }
}