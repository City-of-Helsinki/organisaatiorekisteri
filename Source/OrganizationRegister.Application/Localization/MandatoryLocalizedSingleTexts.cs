using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Localization
{
    internal class MandatoryLocalizedSingleTexts : LocalizedSingleTexts
    {
        public MandatoryLocalizedSingleTexts(IEnumerable<LocalizedText> texts)
            : base(texts)
        {
            if (!this.texts.Any())
            {
                throw new ArgumentException("At least one localized name must be given.");
            }
            if (HasUndefinedLocalizedValues())
            {
                throw new ArgumentException("One or more localized names had undefined localized value.");
            }
        }

        private bool HasUndefinedLocalizedValues()
        {
            return this.Any(name => string.IsNullOrWhiteSpace(name.LocalizedValue));
        }
    }
}
