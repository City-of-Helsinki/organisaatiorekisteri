using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class AddressLanguageSpecification
    {
        public Guid AddressId { get; set; }
        public Guid LanguageId { get; set; }
        public string StreetAddress { get; set; }
        public string PostalDistrict { get; set; }
        public string Qualifier { get; set; }
        public virtual AvailableDataLanguage Language { get; set; }
    }
}