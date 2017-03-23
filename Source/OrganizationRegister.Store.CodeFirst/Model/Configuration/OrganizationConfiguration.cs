using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class OrganizationConfiguration : EntityTypeConfiguration<Organization>
    {
        public OrganizationConfiguration()
        {
            HasKey(organization => organization.Id);

            Property(organization => organization.NumericId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(organization => organization.BusinessId);
            Property(organization => organization.Oid);
            Property(organization => organization.Active);
            Property(organization => organization.CanBeTransferredToFsc);

            HasRequired(organization => organization.Type);
          

            HasOptional(organization => organization.ParentOrganization);
            HasOptional(organization => organization.VisitingAddress);
            HasOptional(organization => organization.EmailAddress);
            HasOptional(organization => organization.PhoneNumber);

            HasMany(organization => organization.LanguageSpecifications);
            
            HasMany(organization => organization.PostalAddresses)
                .WithMany()
                .Map(conf =>
                {
                    conf.MapLeftKey("organizationid");
                    conf.MapRightKey("addressid");
                    conf.ToTable("organization_postaladdress");
                });

            HasMany(organization => organization.WebPages)
                .WithMany()
                .Map(conf =>
                {
                    conf.MapLeftKey("organizationid");
                    conf.MapRightKey("webaddressid");
                    conf.ToTable("organization_webaddress");
                });
        }
    }
}