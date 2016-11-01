using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class OrganizationLanguageSpecification
    {
        public Guid LanguageId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HomepageUrl { get; set; }
        public string NameAbbreviation { get; set; }
        public virtual AvailableDataLanguage Language { get; set; }
    }
}