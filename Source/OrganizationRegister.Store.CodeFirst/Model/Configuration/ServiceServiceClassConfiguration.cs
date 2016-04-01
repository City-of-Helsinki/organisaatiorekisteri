using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class ServiceServiceClassConfiguration : EntityTypeConfiguration<ServiceServiceClass>
    {
        public ServiceServiceClassConfiguration()
        {
            HasKey(serviceServiceClass => new { serviceServiceClass.ServiceId, serviceServiceClass.ServiceClassId});

            HasRequired(serviceServiceClass => serviceServiceClass.Service);
            HasRequired(serviceServiceClass => serviceServiceClass.ServiceClass);
        }
    }
}
