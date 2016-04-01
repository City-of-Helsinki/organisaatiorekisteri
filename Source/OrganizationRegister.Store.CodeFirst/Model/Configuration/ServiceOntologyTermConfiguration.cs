using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class ServiceOntologyTermConfiguration : EntityTypeConfiguration<ServiceOntologyTerm>
    {
        public ServiceOntologyTermConfiguration()
        {
            HasKey(serviceOntologyTerm => new { serviceOntologyTerm.ServiceId, serviceOntologyTerm.OntologyTermId});

            HasRequired(serviceOntologyTerm => serviceOntologyTerm.Service);
            HasRequired(serviceOntologyTerm => serviceOntologyTerm.OntologyTerm);
        }
    }
}
