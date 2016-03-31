using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class AvailableServiceLanguage : ILanguageReference
    {
        public Guid LanguageId { get; set; }
        public int? OrderNumber { get; set; }
        public virtual Language Language { get; set; }
    }
}
