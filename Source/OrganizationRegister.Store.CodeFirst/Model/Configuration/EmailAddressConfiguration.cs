using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class EmailAddressConfiguration : EntityTypeConfiguration<EmailAddress>
    {
        public EmailAddressConfiguration()
        {
            HasKey(address => address.Id);

            Property(address => address.Email).IsRequired();
        }
    }
}