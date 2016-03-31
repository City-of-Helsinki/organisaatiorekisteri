using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Localization
{
    public class LocalizedSingleTexts : LocalizedTexts
    {
        public LocalizedSingleTexts()
        {
        }

        public LocalizedSingleTexts(IEnumerable<LocalizedText> texts)
            : base(texts)
        {
            if (this.texts.Count != this.texts.Select(t => t.LanguageCode).Distinct().Count())
            {
                throw new ArgumentException("Two or more localized texts had the same language.", "texts");
            }
        }

        public void SetValue(LocalizedText text)
        {
            SetValue(text.LanguageCode, text.LocalizedValue);
        }

        public void SetValue(string languageCode, string localizedValue)
        {
            if (texts.Any(d => d.LanguageCode.Equals(languageCode)))
            {
                texts.Single(d => d.LanguageCode.Equals(languageCode)).LocalizedValue = localizedValue;
            }
            else
            {
                texts.Add(new LocalizedText(languageCode, localizedValue));
            }
        }

        public string GetValue(string languageCode)
        {
            return IsLocalizedFor(languageCode) ? texts.Single(text => text.LanguageCode.Equals(languageCode)).LocalizedValue : null;
        }
    }
}
