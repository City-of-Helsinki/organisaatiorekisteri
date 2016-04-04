using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class AvailableDataLanguageConfiguration : EntityTypeConfiguration<AvailableDataLanguage>
    {
        public AvailableDataLanguageConfiguration()
        {
            HasKey(dataLanguage => dataLanguage.LanguageId);
            HasRequired(dataLanguage => dataLanguage.Language).WithOptional();
        }
    }
}
