using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class ServiceKeyword
    {
        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }
        public Guid ServiceId { get; set; }
        public string Keyword { get; set; }
        public virtual AvailableDataLanguage Language { get; set; }
        public virtual Service Service { get; set; }
    }
}
