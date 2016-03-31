using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class LifeEventConfiguration : EntityTypeConfiguration<LifeEvent>
    {
        public LifeEventConfiguration()
        {
            HasKey(lifeEvent => lifeEvent.Id);
            Property(lifeEvent => lifeEvent.Name);
            Property(lifeEvent => lifeEvent.SourceId);
            Property(lifeEvent => lifeEvent.SourceParentId);
        }
    }
}
