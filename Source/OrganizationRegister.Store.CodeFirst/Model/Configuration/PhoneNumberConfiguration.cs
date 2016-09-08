using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class PhoneNumberConfiguration : EntityTypeConfiguration<PhoneNumber>
    {
        public PhoneNumberConfiguration()
        {
            HasKey(phoneNumber => phoneNumber.Id);

            Property(phoneNumber => phoneNumber.Number).IsRequired();
            //Property(phoneNumber => phoneNumber.PhoneCallFee);

            HasMany(phoneNumber => phoneNumber.LanguageSpecifications);
        }
    }
}