using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Identifiers.Finnish;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Location
{
    public abstract class Address
    {
        protected readonly IReadOnlyCollection<string> languageCodes;
        protected readonly LocalizedSingleTexts postalDistricts;
        protected PostalCode postalCode;

        protected Address(IReadOnlyCollection<string> languageCodes)
        {
            if (languageCodes == null)
            {
                throw new ArgumentNullException("languageCodes");
            }
            if (!languageCodes.Any())
            {
                throw new ArgumentException("At least one language code must be given.", "languageCodes");
            }
            
            this.languageCodes = languageCodes.ToList();
            postalDistricts = new LocalizedSingleTexts();            
        }

        public bool IsDefined
        {
            get { return postalDistricts.Any(); }
        }

        public string PostalCode
        {
            get { return postalCode == null ? null : postalCode.ToString(); }
        }

        public IEnumerable<LocalizedText> PostalDistricts
        {
            get { return postalDistricts; }
        }

        public IEnumerable<string> LanguageCodes
        {
            get { return postalDistricts.LanguageCodes; }
        }

        public string GetPostalDistrict(string languageCode)
        {
            return postalDistricts.GetValue(languageCode);
        }

        protected void SetLocalizedTexts(LocalizedSingleTexts target, IReadOnlyCollection<LocalizedText> newTexts)
        {
            if (newTexts != null && newTexts.Any(text => !string.IsNullOrWhiteSpace(text.LocalizedValue)))
            {
                foreach (LocalizedText newStreetAddress in newTexts)
                {
                    SetLocalizedText(target, newStreetAddress);
                }
            }
        }

        protected static bool HasItems(IEnumerable<LocalizedText> texts)
        {
            return texts != null && texts.Any(text => !string.IsNullOrWhiteSpace(text.LocalizedValue));
        }

        private void SetLocalizedText(LocalizedSingleTexts target, LocalizedText newText)
        {
            if (!languageCodes.Any(code => code.Equals(newText.LanguageCode)))
            {
                throw new ArgumentException(string.Format("Language '{0}' not supported.", newText.LanguageCode), "newText");
            }
            target.SetValue(newText);
        }
    }
}
