using System;
using System.Collections.Generic;
using System.Linq;

namespace OrganizationRegister.Application.Localization
{
    internal class LocalizedTextsContainer
    {
        private readonly IReadOnlyCollection<string> languageCodes;
        private readonly IDictionary<LocalizedProperty, LocalizedTexts> textsCollection;

        public LocalizedTextsContainer(IEnumerable<string> languageCodes)
        {
            this.languageCodes = languageCodes.ToList();
            textsCollection = new Dictionary<LocalizedProperty, LocalizedTexts>();
        }

        public void Set(LocalizedProperty property, LocalizedTexts texts)
        {
            if (texts == null)
            {
                textsCollection[property] = new LocalizedSingleTexts();
            }
            else
            {
                textsCollection[property] = texts;
                ValidateTextLanguages(texts);
            }
        }

        public LocalizedTexts GetTexts(LocalizedProperty property)
        {
            return textsCollection.ContainsKey(property) ? textsCollection[property] : new LocalizedTexts();
        }

        private void ValidateTextLanguages(LocalizedTexts texts)
        {
            if (texts.Any(text => !languageCodes.Contains(text.LanguageCode)))
            {
                throw new ArgumentException("Localized text found with unsupported languages.");
            }
        }
    }
}
