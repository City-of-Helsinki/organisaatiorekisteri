using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Tests.Localization
{
    [TestClass]
    public class LocalizedTextsTests
    {
        private LocalizedTexts sut;

        [TestMethod]
        public void LanguageCodesReturnsAllDifferentLanguageCodes()
        {
            sut = new LocalizedTexts(new List<LocalizedText> { new LocalizedText("fi", "arvo1"), new LocalizedText("fi", "arvo2"), new LocalizedText("en", "value") });

            IReadOnlyCollection<string> result = sut.LanguageCodes;

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains("fi"));
            Assert.IsTrue(result.Contains("en"));
        }
    }
}
