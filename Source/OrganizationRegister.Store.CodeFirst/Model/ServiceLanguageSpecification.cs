using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class ServiceLanguageSpecification
    {
        public Guid LanguageId { get; set; }
        public Guid ServiceId { get; set; }
        public string Name { get; set; }
        public string AlternateName { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string UserInstructions { get; set; }
        public string Requirements { get; set; }
        public virtual AvailableDataLanguage Language { get; set; }
    }
}
