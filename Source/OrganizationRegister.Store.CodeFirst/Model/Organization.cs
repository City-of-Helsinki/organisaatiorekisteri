using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;
using OrganizationRegister.Store.CodeFirst.Querying;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class Organization
    {
        public Organization()
        {
            LanguageSpecifications = new Collection<OrganizationLanguageSpecification>();
            WebPages = new Collection<WebPage>();
            PostalAddresses = new Collection<Address>();
            Active = true;
        }

        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public Guid? ParentOrganizationId { get; set; }
        public Guid? EmailAddressId { get; set; }
        public Guid? PhoneNumberId { get; set; }
        public Guid? VisitingAddressId { get; set; }
        public long NumericId { get; set; }
        public string BusinessId { get; set; }
        public int? MunicipalityCode { get; set; }
        public string Oid { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? ValidFrom { get; set; }
        public bool Active { get; set; }
        public bool? StreetAddressAsPostalAddress { get; set; }
        public bool CanBeTransferredToFsc { get; set; }
        public bool CanBeResponsibleDeptForService { get; set; }

        public virtual ICollection<OrganizationLanguageSpecification> LanguageSpecifications { get; set; }
      
        public virtual Organization ParentOrganization { get; set; }
        public virtual OrganizationType Type { get; set; }
        public virtual EmailAddress EmailAddress { get; set; }
        public virtual PhoneNumber PhoneNumber { get; set; }
        public virtual ICollection<WebPage> WebPages { get; set; }
        public virtual Address VisitingAddress { get; set; }
        public virtual ICollection<Address> PostalAddresses { get; set; }
        public virtual ICollection<AuthorizationGroup> AuthorizationGroups { get; set; }

        internal bool UseStreetAddressAsPostalAddress
        {
            get { return StreetAddressAsPostalAddress.HasValue && StreetAddressAsPostalAddress.Value; }
        }

        internal void SetCallInformation(string phoneNumber, string callChargeType, IEnumerable<LocalizedText> callChargeInfos, IStoreContext context)
        {
            if (PhoneNumber != null)
            {
                context.PhoneNumbers.Remove(PhoneNumber);
                PhoneNumber = null;
            }

            if (phoneNumber != null)
            {
                var phoneNumberLanguageSpecifications = new  Collection<PhoneNumberLanguageSpecification>();
                
                foreach (LocalizedText info in callChargeInfos)
                {
                    phoneNumberLanguageSpecifications.Add(new PhoneNumberLanguageSpecification()
                    {
                        Language = context.GetDataLanguage(info.LanguageCode),
                        CallChargeInfo = info.LocalizedValue
                    });
                }

                PhoneNumber = new PhoneNumber
                {
                    Id = Guid.NewGuid(),
                    //PhoneCallFee = phoneCallFee,
                    Number = phoneNumber,
                    ChargeType = context.GetCallChargeType(callChargeType),
                    LanguageSpecifications = phoneNumberLanguageSpecifications
                };


                
            }
        }

        internal void SetEmailAddress(string emailAddress, IStoreContext context)
        {
            if (EmailAddress != null)
            {
                context.EmailAddresses.Remove(EmailAddress);
            }

            if (emailAddress != null)
            {
                EmailAddress = new EmailAddress { Id = Guid.NewGuid(), Email = emailAddress };
            }
        }

        internal void SetWebPages(IEnumerable<OrganizationRegister.Common.WebPage> webPages, IStoreContext context)
        {
            RemoveAllWebPages(context);

            if (webPages != null)
            {
                foreach (OrganizationRegister.Common.WebPage page in webPages)
                {
                    WebPages.Add(new WebPage { Id = Guid.NewGuid(), Url = page.Address, Name = page.Name, TypeId = context.GetWebPageType(page.Type).Id });
                }
            }
        }

        internal void RemoveAllWebPages(IStoreContext context)
        {
            foreach (WebPage webPage in WebPages.ToList())
            {
                context.WebPages.Remove(webPage);
                WebPages.Remove(webPage);
            }
        }

        internal void SetMunicipalityCode(string municipalityCode)
        {
            if (string.IsNullOrWhiteSpace(municipalityCode))
            {
                MunicipalityCode = null;
            }
            else
            {
                MunicipalityCode = int.Parse(municipalityCode);
            }
        }

        internal void RemoveAllLanguageSpecificData()
        {
            foreach (OrganizationLanguageSpecification specification in LanguageSpecifications.ToList())
            {
                LanguageSpecifications.Remove(specification);
            }
        }

        internal void SetVisitingAddress(IVisitingAddress address, IStoreContext context)
        {
            RemoveVisitingAddress(context);
            if (address != null && address.VisitingAddress.IsDefined)
            {
                VisitingAddress = new Address { Id = Guid.NewGuid() };
                VisitingAddress.AddAddress(address, context);
            }
        }

        internal void RemoveVisitingAddress(IStoreContext context)
        {
            if (VisitingAddress != null)
            {
                context.Addresses.Remove(VisitingAddress);
                StreetAddressAsPostalAddress = false;
            }
        }

        internal void SetPostalAddresses(IPostalAddress addresses, IStoreContext context)
        {
            RemovePostalAddresses(context);

            if (addresses != null)
            {
                if (addresses.PostalStreetAddress != null && addresses.PostalStreetAddress.IsDefined)
                {
                    var address = new Address { Id = Guid.NewGuid() };
                    address.AddAddress(addresses.PostalStreetAddress, context);
                    PostalAddresses.Add(address);
                }
                if (addresses.PostalPostOfficeBoxAddress != null && addresses.PostalPostOfficeBoxAddress.IsDefined)
                {
                    var address = new Address { Id = Guid.NewGuid() };
                    address.AddAddress(addresses.PostalPostOfficeBoxAddress, context);
                    PostalAddresses.Add(address);
                }
                StreetAddressAsPostalAddress = addresses.UseVisitingAddressAsPostalAddress;
            }
        }

        internal ICollection<AddressLanguageSpecification> GetLanguageSpecificStreetAddressData()
        {
            return VisitingAddress == null ? null : VisitingAddress.LanguageSpecifications;
        }

        internal string GetStreetAddressPostalCode()
        {
            return VisitingAddress == null ? null : VisitingAddress.PostalCode;
        }

        internal string GetPostalStreetAddressPostalCode()
        {
            var query = new PostalStreetAddressQuery(PostalAddresses);
            Address address = query.Execute();
            return address == null ? null : address.PostalCode;
        }

        internal string GetPostalPostOfficeBoxAddressPostOfficeBox()
        {
            var query = new PostalPostOfficeBoxAddressQuery(PostalAddresses);
            Address address = query.Execute();
            return address == null ? null : address.PostOfficeBox;
        }

        internal string GetPostalPostOfficeBoxAddressPostalCode()
        {
            var query = new PostalPostOfficeBoxAddressQuery(PostalAddresses);
            Address address = query.Execute();
            return address == null ? null : address.PostalCode;
        }

        internal void RemovePostalAddresses(IStoreContext context)
        {
            foreach (Address address in PostalAddresses.ToList())
            {
                context.Addresses.Remove(address);
                PostalAddresses.Remove(address);
            }
        }

        internal void SetContactInformation(IContactInformation information, IStoreContext context)
        {
            SetCallInformation(information.PhoneNumber, information.PhoneCallChargeType, information.PhoneCallChargeInfos, context);
            SetEmailAddress(information.EmailAddress, context);
            SetWebPages(information.WebPages, context);
            SetHomepageUrls(information, context);
        }

        internal void SetHomepageUrls(ILocalizedText information, IStoreContext context)
        {
            SetLocalizedTexts(information, context);
        }

        private void SetLocalizedTexts (ILocalizedText information, IStoreContext context)
        {

            RemoveAllLanguageSpecificData();

            foreach (var lang in context.DataLanguages.ToList())
            {
                var name = information.GetName(lang.Language.Code);
                var description = information.GetDescription(lang.Language.Code);
                var homepageUrl = information.GetHomepageUrl(lang.Language.Code);
                var nameAbbreviation = information.GetNameAbbreviation(lang.Language.Code);
 
                if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(description) || !string.IsNullOrEmpty(homepageUrl))
                {
                    LanguageSpecifications.Add(new OrganizationLanguageSpecification
                    {
                        Language = lang,
                        Name = name,
                        Description = description,
                        HomepageUrl = homepageUrl,
                        NameAbbreviation = nameAbbreviation
                    });
                }
              
            }
        }



        internal IReadOnlyCollection<LocalizedText> GetDescriptions()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.Description)).ToList();
        }

        internal IReadOnlyCollection<LocalizedText> GetNames()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.Name)).ToList();
        }

        internal IReadOnlyCollection<LocalizedText> GetHomepageUrls()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.HomepageUrl)).ToList();
        }


        internal IReadOnlyCollection<LocalizedText> GetNameAbbreviations()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.NameAbbreviation)).ToList();
        }


        internal void SetBasicInformation(IBasicInformation information, IStoreContext context)
        {
            BusinessId = information.BusinessId;
            Oid = information.Oid;
            Type = context.GetOrganizationType(information.Type);
            SetMunicipalityCode(information.MunicipalityCode);
            ValidFrom = information.ValidFrom;
            ValidTo = information.ValidTo;
            CanBeTransferredToFsc = information.CanBeTransferredToFsc;
            CanBeResponsibleDeptForService = information.CanBeResponsibleDeptForService;

            SetLocalizedTexts(information, context);

           

            //RemoveAllLanguageSpecificData();

            //foreach (LocalizedText localizedName in information.Names)
            //{
            //    LanguageSpecifications.Add(new OrganizationLanguageSpecification
            //    {
            //        Language = context.GetDataLanguage(localizedName.LanguageCode),
            //        Name = localizedName.LocalizedValue,
            //        Description = information.GetDescription(localizedName.LanguageCode),
            //        HomepageUrl = information.GetHomepageUrl(localizedName.LanguageCode)
            //    });
            //}
        }


        
    }
}