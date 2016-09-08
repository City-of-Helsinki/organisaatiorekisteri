using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class LanguageSpecificAddressDataQuery
    {
        private readonly ICollection<AddressLanguageSpecification> languageSpecifications;

        public LanguageSpecificAddressDataQuery(ICollection<AddressLanguageSpecification> languageSpecifications)
        {
            this.languageSpecifications = languageSpecifications;
        }

        public IEnumerable<AddressLanguageSpecification> Execute(Guid addressId)
        {
            return languageSpecifications == null ? Enumerable.Empty<AddressLanguageSpecification>() :
                languageSpecifications.Where(address => address.AddressId.Equals(addressId));
        }
    }
}