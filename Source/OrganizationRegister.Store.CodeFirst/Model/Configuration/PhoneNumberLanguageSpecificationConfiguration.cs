using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class PhoneNumberLanguageSpecificationConfiguration : EntityTypeConfiguration<PhoneNumberLanguageSpecification>
    {
        public PhoneNumberLanguageSpecificationConfiguration()
        {
            HasKey(specification => new { specification.PhoneNumberId, specification.LanguageId });

            Property(specification => specification.CallChargeInfo);
            HasRequired(specification => specification.Language);
        }
    }
}