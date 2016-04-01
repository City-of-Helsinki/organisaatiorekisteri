using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class ServiceOntologyTerm
    {
        public Guid OntologyTermId { get; set; }
        public Guid ServiceId { get; set; }
        public virtual OntologyTerm OntologyTerm { get; set; }
        public virtual Service Service { get; set; }
    }
}
