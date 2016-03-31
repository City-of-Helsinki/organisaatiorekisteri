using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class ServiceClassConfiguration : EntityTypeConfiguration<ServiceClass>
    {
        public ServiceClassConfiguration()
        {
            HasKey(serviceClass => serviceClass.Id);
            Property(serviceClass => serviceClass.Name);
            Property(serviceClass => serviceClass.SourceId);
            Property(serviceClass => serviceClass.SourceParentId);
        }
    }
}
