using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class ServiceLanguageConfiguration : EntityTypeConfiguration<ServiceLanguage>
    {
        public ServiceLanguageConfiguration()
        {
            HasKey(serviceLanguage => new { serviceLanguage.ServiceId, serviceLanguage.LanguageId });
            HasRequired(serviceLanguage => serviceLanguage.Service);
            HasRequired(serviceLanguage => serviceLanguage.Language);
        }
    }
}
