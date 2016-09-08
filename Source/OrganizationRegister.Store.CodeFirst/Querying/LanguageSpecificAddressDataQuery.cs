using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class LanguageSpecificPhoneNumberDataQuery
    {
        private readonly ICollection<PhoneNumberLanguageSpecification> languageSpecifications;

        public LanguageSpecificPhoneNumberDataQuery(ICollection<PhoneNumberLanguageSpecification> languageSpecifications)
        {
            this.languageSpecifications = languageSpecifications;
        }

        public IEnumerable<PhoneNumberLanguageSpecification> Execute(Guid phoneNumberId)
        {
            return languageSpecifications == null ? Enumerable.Empty<PhoneNumberLanguageSpecification>() :
                languageSpecifications.Where(number => number.PhoneNumberId.Equals(phoneNumberId));
        }
    }
}