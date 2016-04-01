using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class TargetGroupConfiguration : EntityTypeConfiguration<TargetGroup>
    {
        public TargetGroupConfiguration()
        {
            HasKey(lifeEvent => lifeEvent.Id);
            Property(lifeEvent => lifeEvent.Name);
            Property(lifeEvent => lifeEvent.SourceId);
            Property(lifeEvent => lifeEvent.SourceParentId);
        }
    }
}
