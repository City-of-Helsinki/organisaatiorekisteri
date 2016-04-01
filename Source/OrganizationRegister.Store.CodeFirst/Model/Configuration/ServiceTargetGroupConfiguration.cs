using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class ServiceTargetGroupConfiguration : EntityTypeConfiguration<ServiceTargetGroup>
    {
        public ServiceTargetGroupConfiguration()
        {
            HasKey(serviceTargetGroup=> new { serviceTargetGroup.ServiceId, serviceTargetGroup.TargetGroupId});

            HasRequired(serviceTargetGroup => serviceTargetGroup.Service);
            HasRequired(serviceTargetGroup => serviceTargetGroup.TargetGroup);
        }
    }
}
