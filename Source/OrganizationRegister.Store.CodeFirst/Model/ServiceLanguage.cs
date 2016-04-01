using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class ServiceLanguage
    {
        public Guid LanguageId { get; set; }
        public Guid ServiceId { get; set; }
        public virtual AvailableServiceLanguage Language { get; set; }
        public virtual Service Service { get; set; }
    }
}
