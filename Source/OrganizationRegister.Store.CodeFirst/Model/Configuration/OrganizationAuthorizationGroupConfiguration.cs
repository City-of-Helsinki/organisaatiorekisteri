using System;
using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class OrganizationAuthorizationGroupConfiguration : EntityTypeConfiguration<AuthorizationGroup>
    {
        public OrganizationAuthorizationGroupConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.OrganizationId).IsRequired();
            Property(t => t.GroupId).IsRequired();
            Property(t => t.GroupName).IsRequired();
            Property(t => t.RoleId).IsRequired();

            HasRequired<Organization>(s => s.Organization)
            .WithMany(g => g.AuthorizationGroups)
            .HasForeignKey<Guid>(s => s.OrganizationId);

        }
    }
}

