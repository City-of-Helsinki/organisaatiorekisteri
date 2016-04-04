using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class AddressLanguageSpecificationConfiguration : EntityTypeConfiguration<AddressLanguageSpecification>
    {
        public AddressLanguageSpecificationConfiguration()
        {
            HasKey(specification => new { specification.AddressId, specification.LanguageId });

            Property(specification => specification.StreetAddress);
            Property(specification => specification.PostalDistrict);
            Property(specification => specification.Qualifier);

            HasRequired(specification => specification.Language);
        }
    }
}