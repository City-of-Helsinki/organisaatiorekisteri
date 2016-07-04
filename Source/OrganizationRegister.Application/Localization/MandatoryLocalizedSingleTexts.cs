using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Localization
{
    internal class MandatoryLocalizedSingleTexts : LocalizedSingleTexts
    {
        // TODO: Get mandatory languages from DB
        public MandatoryLocalizedSingleTexts(IEnumerable<LocalizedText> texts)
            : base(texts)
        {
            if (this.texts.All(name => name.LanguageCode != "fi"))
            {
                throw new ArgumentException("At least one localized finnish name must be given.");
            }
            if (HasUndefinedLocalizedValues())
            {
                throw new ArgumentException("One or more localized finnish names had undefined localized value.");
            }
        }

        private bool HasUndefinedLocalizedValues()
        {
            return this.Any(name => string.IsNullOrWhiteSpace(name.LocalizedValue) && name.LanguageCode == "fi");
        }
    }
}
