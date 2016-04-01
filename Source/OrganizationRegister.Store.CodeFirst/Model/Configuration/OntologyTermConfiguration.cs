using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class OntologyTermConfiguration : EntityTypeConfiguration<OntologyTerm>
    {
        public OntologyTermConfiguration()
        {
            HasKey(serviceClass => serviceClass.Id);
            Property(serviceClass => serviceClass.Name);
            Property(serviceClass => serviceClass.LowerCaseName);
            Property(serviceClass => serviceClass.SourceId);
            Property(serviceClass => serviceClass.SourceParentId);
        }
    }
}
