using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class OrganizationLanguageSpecificationConfiguration : EntityTypeConfiguration<OrganizationLanguageSpecification>
    {
        public OrganizationLanguageSpecificationConfiguration()
        {
            HasKey(specification => new { specification.OrganizationId, specification.LanguageId });
            
            Property(specification => specification.Name);
            Property(specification => specification.Description);
            Property(specification => specification.HomepageUrl);

            HasRequired(specification => specification.Language);
        }
    }
}