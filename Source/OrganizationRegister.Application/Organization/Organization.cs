using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Affecto.Identifiers;
using Affecto.Identifiers.Finnish;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Application.Location;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Organization
{
    internal class Organization : IOrganization
    {
        private readonly IReadOnlyCollection<string> languageCodes;
        private readonly PostalAddresses postalAddresses;
        private WebPages webPages;
        private PhoneNumber phoneNumber;
        private EmailAddress emailAddress;
        private MunicipalityCode municipalityCode;
        private readonly LocalizedTextsContainer localizedTextsContainer;

        protected BusinessIdentifier businessId;
        
        public Organization(Guid id, long numericId, string businessId, string oid, string type, int? municipalityCode, IEnumerable<LocalizedText> names,
            IEnumerable<string> languageCodes, bool canBeTransferredToFsc)
            : this(id, businessId, oid, type, municipalityCode.HasValue ? municipalityCode.Value.ToString(CultureInfo.InvariantCulture) : null, names, languageCodes, canBeTransferredToFsc)
        {
            NumericId = numericId;
        }

        public Organization(Guid id, string businessId, string oid, string type, string municipalityCode, IEnumerable<LocalizedText> names, IEnumerable<string> languageCodes, bool canBeTransferredToFsc)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Organization must have an identifier.", "id");
            }

            this.languageCodes = languageCodes.ToList();
            Id = id;
            BusinessId = businessId;
          
            Oid = oid;
            webPages = new WebPages();

            CanBeTransferredToFsc = canBeTransferredToFsc;
            
            localizedTextsContainer = new LocalizedTextsContainer(this.languageCodes);
            Names = names;
          
            Type = type;
            SetMunicipalityCode(municipalityCode);
            ValidateType();

            postalAddresses = new PostalAddresses(this.languageCodes);
        }

        public Guid Id { get; protected set; }
        public long NumericId { get; protected set; }

        public bool IsSubOrganization
        {
            get { return this is SubOrganization; }
        }

        public virtual string BusinessId
        {
            get { return businessId.ToString(); }
            set { businessId = BusinessIdentifier.Create(value); }
        }

        public string Oid { get; set; }


        public bool CanBeTransferredToFsc { get; set; }

        public IEnumerable<WebPage> WebPages
        {
            get { return webPages; }
            set
            {
                webPages.Clear();
                if (value != null)
                {
                    webPages = new WebPages(value);
                }
            }
        }


        public IEnumerable<LocalizedText> HomepageUrls
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.HomepageUrl); }

            set
            {
                WebAddressSpecification specification = new WebAddressSpecification();

                if (value != null)
                {
                    foreach (LocalizedText txt in value)
                    {
                        if (!string.IsNullOrEmpty(txt.LocalizedValue) && !specification.IsSatisfiedBy(txt.LocalizedValue))
                        {
                            throw new ArgumentException(string.Format("Invalid url ({0}).", txt.LocalizedValue), nameof(value));
                        }
                    }
                }
                localizedTextsContainer.Set(LocalizedProperty.HomepageUrl, new LocalizedSingleTexts(value));
            }
        }

        public IEnumerable<LocalizedText> Names
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.Name); }
            set { localizedTextsContainer.Set(LocalizedProperty.Name, new MandatoryLocalizedSingleTexts(value)); }
            
        }

        public IEnumerable<LocalizedText> NameAbbreviations
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.NameAbbreviation); }
            set { localizedTextsContainer.Set(LocalizedProperty.NameAbbreviation, new LocalizedSingleTexts(value)); }

        }

        public IEnumerable<LocalizedText> Descriptions
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.Description); }
            set { localizedTextsContainer.Set(LocalizedProperty.Description, new LocalizedSingleTexts(value)); }
        }

        public string Type { get; private set; }

        public string MunicipalityCode
        {
            get { return municipalityCode == null ? null : municipalityCode.ToString(); }
        }

        public string PhoneNumber 
        {
            get { return phoneNumber == null ? null : phoneNumber.ToString(); }
        }

        public string PhoneCallChargeType { get; private set; }

        public IEnumerable<LocalizedText> PhoneCallChargeInfos
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.CallChargeInfo); }
            set { localizedTextsContainer.Set(LocalizedProperty.CallChargeInfo, new LocalizedSingleTexts(value)); }
        }

        public string GetPhoneCallChargeInfo(string languageCode)
        {
            LocalizedSingleTexts infos = (LocalizedSingleTexts)localizedTextsContainer.GetTexts(LocalizedProperty.CallChargeInfo);
            return infos.GetValue(languageCode);
        }

        public string EmailAddress
        {
            get { return emailAddress == null ? null : emailAddress.ToString(); }
            set
            {
                emailAddress = null;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    emailAddress = Affecto.Identifiers.EmailAddress.Create(value);
                }                
            }
        }

        //public string PhoneCallFee { get; private set; }


        public StreetAddress VisitingAddress { get; private set; }

        public StreetAddress PostalStreetAddress
        {
            get { return postalAddresses.StreetAddress; }
        }

        public PostOfficeBoxAddress PostalPostOfficeBoxAddress
        {
            get { return postalAddresses.PostOfficeBoxAddress; }
        }

        public bool UseVisitingAddressAsPostalAddress
        {
            get { return postalAddresses.UseVisitingAddress; }
        }

        public IEnumerable<LocalizedText> VisitingAddressQualifiers
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.VisitingAddressQualifier); }
            set { localizedTextsContainer.Set(LocalizedProperty.VisitingAddressQualifier, new LocalizedSingleTexts(value)); }
        }

        public string GetVisitingAddressQualifier(string languageCode)
        {
            LocalizedSingleTexts qualifiers = (LocalizedSingleTexts) localizedTextsContainer.GetTexts(LocalizedProperty.VisitingAddressQualifier);
            return qualifiers.GetValue(languageCode);
        }

        
        public string GetName(string languageCode)
        {
            LocalizedSingleTexts names = (LocalizedSingleTexts)localizedTextsContainer.GetTexts(LocalizedProperty.Name);
            return names.GetValue(languageCode);
        }

        public string GetDescription(string languageCode)
        {
            LocalizedSingleTexts descriptions = (LocalizedSingleTexts) localizedTextsContainer.GetTexts(LocalizedProperty.Description);
            return descriptions.GetValue(languageCode);
        }

        public string GetHomepageUrl(string languageCode)
        {
            LocalizedSingleTexts homepageUrls = (LocalizedSingleTexts)localizedTextsContainer.GetTexts(LocalizedProperty.HomepageUrl);
            return homepageUrls.GetValue(languageCode);
        }

        public string GetNameAbbreviation(string languageCode)
        {
            LocalizedSingleTexts nameAbbreviations = (LocalizedSingleTexts)localizedTextsContainer.GetTexts(LocalizedProperty.NameAbbreviation);
            return nameAbbreviations.GetValue(languageCode);
        }

        public DateTime? ValidFrom { get; private set; }
        public DateTime? ValidTo { get; private set; }

        public void SetType(string newType, string newMunicipalityCode)
        {
            Type = newType;
            SetMunicipalityCode(newMunicipalityCode);
            ValidateType();
        }

        public void SetCallInformation(string newPhoneNumber, string newCallChargeType, IEnumerable<LocalizedText> newCallChargeinfos)
        {
            phoneNumber = null;
            //PhoneCallFee = null;

            PhoneCallChargeType = null;

            if (!string.IsNullOrWhiteSpace(newPhoneNumber))
            {

               
                phoneNumber = Affecto.Identifiers.Finnish.PhoneNumber.Create(newPhoneNumber);
                PhoneCallChargeType = newCallChargeType;
                PhoneCallChargeInfos = newCallChargeinfos;

                //PhoneCallFee = newPhoneCallFee;
            }
        }

        public void SetVisitingAddress(IEnumerable<LocalizedText> streetAddresses, string postalCode, IEnumerable<LocalizedText> localities)
        {
            VisitingAddress = StreetAddress.Create(languageCodes, streetAddresses == null ? null : streetAddresses.ToList(), postalCode,
                localities == null ? null : localities.ToList());
        }

        public void SetPostalAddress(bool useVisitingAddress, IEnumerable<LocalizedText> streetAddresses, string streetAddressPostalCode,
            IEnumerable<LocalizedText> streetAddressPostalDistricts, string postOfficeBox, string postOfficeBoxAddressPostalCode, 
            IEnumerable<LocalizedText> postOfficeBoxAddressPostalDistricts)
        {
            if (useVisitingAddress && (VisitingAddress == null || !VisitingAddress.IsDefined))
            {
                throw new ArgumentException("Cannot use undefined visiting address as the postal address.");
            }
            postalAddresses.Set(useVisitingAddress, streetAddresses == null ? null : streetAddresses.ToList(), streetAddressPostalCode, 
                streetAddressPostalDistricts == null ? null : streetAddressPostalDistricts.ToList(), postOfficeBox, postOfficeBoxAddressPostalCode, 
                postOfficeBoxAddressPostalDistricts == null ? null : postOfficeBoxAddressPostalDistricts.ToList());
        }

        public void SetValidity(DateTime? validFrom, DateTime? validTo)
        {
            ValidFrom = validFrom.HasValue ? validFrom.Value.Date : (DateTime?)null;
            ValidTo = validTo.HasValue ? validTo.Value.Date : (DateTime?)null;
            if (!IsValidValidityDateRange())
            {
                throw new ArgumentException(string.Format("Invalid date range. Valid from date '{0}' must be before valid to date '{1}'.", ValidFrom, ValidTo));
            }
        }

        private bool IsValidValidityDateRange()
        {
            return !ValidFrom.HasValue || !ValidTo.HasValue || ValidFrom.Value <= ValidTo.Value;
        }

        private void SetMunicipalityCode(string newMunicipalityCode)
        {
            municipalityCode = string.IsNullOrWhiteSpace(newMunicipalityCode) ? null : Affecto.Identifiers.Finnish.MunicipalityCode.Create(newMunicipalityCode);
        }

        private void ValidateType()
        {
            if (string.IsNullOrWhiteSpace(Type))
            {
                throw new ArgumentException("Organization type value must be given.");
            }
            if (Type != OrganizationType.Municipality && municipalityCode != null)
            {
                throw new ArgumentException("Other than municipality organization cannot have municipality code.");
            }
            if (Type == OrganizationType.Municipality && municipalityCode == null)
            {
                throw new ArgumentException("Municipality organization must have municipality code.");
            }
        }
    }
}
