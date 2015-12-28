using System.Data.Entity.ModelConfiguration;

namespace ServiceRegister.Store.CodeFirst.Model.Configuration
{
    internal class WebPageConfiguration : EntityTypeConfiguration<WebPage>
    {
        public WebPageConfiguration()
        {
            HasKey(address => address.Id);

            Property(address => address.Url).IsRequired();
            Property(address => address.Name).IsRequired();
        }
    }
}