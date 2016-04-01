using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class ServiceLanguageSpecificationConfiguration : EntityTypeConfiguration<ServiceLanguageSpecification>
    {
        public ServiceLanguageSpecificationConfiguration()
        {
            HasKey(specification => new { specification.ServiceId, specification.LanguageId });

            Property(specification => specification.Name);
            Property(specification => specification.AlternateName);
            Property(specification => specification.Description);
            Property(specification => specification.ShortDescription);
            Property(specification => specification.Requirements);
            Property(specification => specification.UserInstructions);

            HasRequired(specification => specification.Language);
        }
    }
}
