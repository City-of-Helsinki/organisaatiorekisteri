using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            HasKey(address => address.Id);

            Property(address => address.PostOfficeBox);
            Property(address => address.PostalCode);

            HasMany(address => address.LanguageSpecifications).WithRequired().HasForeignKey(a => a.AddressId);
        }
    }
}