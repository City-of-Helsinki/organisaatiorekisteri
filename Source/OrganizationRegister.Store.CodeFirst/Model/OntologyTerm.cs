using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class OntologyTerm : IClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LowerCaseName { get; set; }
        public string SourceId { get; set; }
        public string SourceParentId { get; set; }
        public int? OrderNumber { get; set; }
    }
}
