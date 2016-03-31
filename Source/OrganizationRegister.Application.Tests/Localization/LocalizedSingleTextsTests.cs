using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Tests.Localization
{
    [TestClass]
    public class LocalizedSingleTextsTests
    {
        private const string LanguageCode = "fi";

        private LocalizedSingleTexts sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TwoOrMoreTextsCannotHaveTheSameLanguage()
        {
            sut = new LocalizedSingleTexts(new List<LocalizedText> { new LocalizedText(LanguageCode, "value1"), new LocalizedText(LanguageCode, "value2")} );
        }
    }
}
