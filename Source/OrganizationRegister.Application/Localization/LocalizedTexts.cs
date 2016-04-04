using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Localization
{
    public class LocalizedTexts : IEnumerable<LocalizedText>
    {
        protected readonly List<LocalizedText> texts;

        public LocalizedTexts(IEnumerable<LocalizedText> texts)
        {
            this.texts = texts == null ? new List<LocalizedText>() : texts.ToList();
        }

        public LocalizedTexts()
        {
            texts = new List<LocalizedText>();
        }

        public IReadOnlyCollection<string> LanguageCodes
        {
            get { return texts.Select(text => text.LanguageCode).Distinct().ToList(); }
        }

        public void Clear()
        {
            texts.RemoveAll(t => true);
        }

        public bool IsLocalizedFor(string languageCode)
        {
            return texts.Any(text => text.LanguageCode.Equals(languageCode));
        }

        public IEnumerator<LocalizedText> GetEnumerator()
        {
            return texts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
