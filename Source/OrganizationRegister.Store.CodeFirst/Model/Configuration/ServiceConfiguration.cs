using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class ServiceConfiguration : EntityTypeConfiguration<Service>
    {
        public ServiceConfiguration()
        {
            HasKey(service => service.Id);
            Property(service => service.NumericId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(service => service.LanguageSpecifications);
            HasMany(service => service.Languages);

            HasMany(service => service.LifeEvents);
            HasMany(service => service.OntologyTerms);
            HasMany(service => service.ServiceClasses);
            HasMany(service => service.TargetGroups);

            HasMany(service => service.Keywords);
        }
    }
}
