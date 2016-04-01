using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class ServiceKeywordConfiguration : EntityTypeConfiguration<ServiceKeyword>
    {
        public ServiceKeywordConfiguration()
        {
            HasKey(keyword => keyword.Id);

            Property(keyword => keyword.Keyword).IsRequired();

            HasRequired(keyword => keyword.Service);
            HasRequired(keyword => keyword.Language);
        }
    }
}
