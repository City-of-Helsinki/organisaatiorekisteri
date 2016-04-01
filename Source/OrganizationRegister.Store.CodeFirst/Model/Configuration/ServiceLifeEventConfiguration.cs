using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class ServiceLifeEventConfiguration : EntityTypeConfiguration<ServiceLifeEvent>
    {
        public ServiceLifeEventConfiguration()
        {
            HasKey(serviceLifeEvent => new { serviceLifeEvent.ServiceId, serviceLifeEvent.LifeEventId});
  
            HasRequired(serviceLifeEvent => serviceLifeEvent.Service);
            HasRequired(serviceLifeEvent => serviceLifeEvent.LifeEvent);
        }
    }
}
