using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OrganizationRegister.Application.Location;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class Address
    {
        public Address()
        {
            LanguageSpecifications = new Collection<AddressLanguageSpecification>();
        }

        public Guid Id { get; set; }
        public string PostalCode { get; set; }
        public string PostOfficeBox { get; set; }
        public virtual ICollection<AddressLanguageSpecification> LanguageSpecifications { get; set; }

        public void AddAddress(IVisitingAddress address, IStoreContext context)
        {
            StreetAddress visitingAddress = address.VisitingAddress;
            AddAddress(visitingAddress, context, address);
        }

        public void AddAddress(StreetAddress address, IStoreContext context, IVisitingAddressQualifiers qualifiers = null)
        {
            PostalCode = address.PostalCode;

            foreach (string languageCode in address.LanguageCodes)
            {
                LanguageSpecifications.Add(new AddressLanguageSpecification
                {
                    Language = context.GetDataLanguage(languageCode),
                    StreetAddress = address.GetStreetAddress(languageCode),
                    PostalDistrict = address.GetPostalDistrict(languageCode),
                    Qualifier = qualifiers == null ? null : qualifiers.GetVisitingAddressQualifier(languageCode)
                });
            }
        }

        public void AddAddress(PostOfficeBoxAddress address, IStoreContext context)
        {
            PostOfficeBox = address.PostOfficeBox;
            PostalCode = address.PostalCode;

            foreach (string languageCode in address.LanguageCodes)
            {
                LanguageSpecifications.Add(new AddressLanguageSpecification
                {
                    Language = context.GetDataLanguage(languageCode),
                    PostalDistrict = address.GetPostalDistrict(languageCode),
                });
            }
        }
    }
}