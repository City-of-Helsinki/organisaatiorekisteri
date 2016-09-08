using System;
using System.ComponentModel.DataAnnotations;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class PhoneNumberLanguageSpecification
    {
        
        public Guid LanguageId { get; set; }
       
        public Guid PhoneNumberId { get; set; }
        public string CallChargeInfo { get; set; }
        public virtual AvailableDataLanguage Language { get; set; }
    }
}