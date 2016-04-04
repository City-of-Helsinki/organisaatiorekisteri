using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class PostalStreetAddressQuery
    {
        private readonly ICollection<Address> addresses;

        public PostalStreetAddressQuery(ICollection<Address> addresses)
        {
            this.addresses = addresses;
        }

        public Address Execute()
        {
            return addresses == null ? null : 
                addresses.SingleOrDefault(address => address.LanguageSpecifications.Any(langData => !string.IsNullOrWhiteSpace(langData.StreetAddress)));
        }
    }
}