using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Tests.Service
{
    [TestClass]
    public class ServiceTests
    {
        private Application.Service.Service sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyIdIsNotAllowed()
        {
            sut = new Application.Service.Service(Guid.Empty, new List<LocalizedText> { new LocalizedText("fi", "palvelu")}, 
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu")}, new List<LocalizedText> { new LocalizedText("fi", "Hyvä")}, 
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithEmptyServiceLanguageCollection()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu") }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language>(), new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithNullServiceLanguageCollection()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu") }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                null, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithEmptyNamesCollection()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText>(),
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu") }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithNullNamesCollection()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), null,
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu") }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithNullNameValue()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", null) },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu") }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithEmptyNameValue()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", string.Empty) },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu") }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithNameHavingUnsupportedLanguage()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("en", "service") },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä palvelu") }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithEmptyDescriptionsCollection()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu")},
                new List<LocalizedText>(), new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithNullDescriptionsCollection()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                null, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithNullDescriptionValue()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", null) }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithEmptyDescriptionValue()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", string.Empty) }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithDescriptionHavingUnsupportedLanguage()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                new List<LocalizedText> { new LocalizedText("en", "really good") }, new List<LocalizedText> { new LocalizedText("fi", "Hyvä") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithEmptyShortDescriptionsCollection()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä") }, new List<LocalizedText>(),
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithNullShortDescriptionsCollection()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä") }, null,
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithNullShortDescriptionValue()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä") }, new List<LocalizedText> { new LocalizedText("fi", null) },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithEmptyShortDescriptionValue()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä") }, new List<LocalizedText> { new LocalizedText("fi", string.Empty) },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithShortDescriptionHavingUnsupportedLanguage()
        {
            sut = new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä") }, new List<LocalizedText> { new LocalizedText("en", "good") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithAlternateNameHavingUnsupportedLanguage()
        {
            sut = CreateService();
            sut.AlternateNames = new List<LocalizedText> { new LocalizedText("en", "name")};
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithRequirementHavingUnsupportedLanguage()
        {
            sut = CreateService();
            sut.Requirements = new List<LocalizedText> { new LocalizedText("en", "req") };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializingWithUserInstructionHavingUnsupportedLanguage()
        {
            sut = CreateService();
            sut.UserInstructions = new List<LocalizedText> { new LocalizedText("en", "do this") };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyServiceLanguageCollection()
        {
            sut = CreateService();
            sut.Languages = new List<Language>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNullServiceLanguageCollection()
        {
            sut = CreateService();
            sut.Languages = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyNamesCollection()
        {
            sut = CreateService();
            sut.Names = new List<LocalizedText>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNullNamesCollection()
        {
            sut = CreateService();
            sut.Names = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNullNameValue()
        {
            sut = CreateService();
            sut.Names = new List<LocalizedText> { new LocalizedText("fi", null) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyNameValue()
        {
            sut = CreateService();
            sut.Names = new List<LocalizedText> { new LocalizedText("fi", string.Empty) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNameHavingUnsupportedLanguage()
        {
            sut = CreateService();
            sut.Names = new List<LocalizedText> { new LocalizedText("en", "service") };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyDescriptionsCollection()
        {
            sut = CreateService();
            sut.Descriptions = new List<LocalizedText>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNullDescriptionsCollection()
        {
            sut = CreateService();
            sut.Descriptions = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNullDescriptionValue()
        {
            sut = CreateService();
            sut.Descriptions = new List<LocalizedText> { new LocalizedText("fi", null) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyDescriptionValue()
        {
            sut = CreateService();
            sut.Descriptions = new List<LocalizedText> { new LocalizedText("fi", string.Empty) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingDescriptionHavingUnsupportedLanguage()
        {
            sut = CreateService();
            sut.Descriptions = new List<LocalizedText> { new LocalizedText("en", "really good") };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyShortDescriptionsCollection()
        {
            sut = CreateService();
            sut.ShortDescriptions = new List<LocalizedText>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNullShortDescriptionsCollection()
        {
            sut = CreateService();
            sut.ShortDescriptions = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingNullShortDescriptionValue()
        {
            sut = CreateService();
            sut.ShortDescriptions = new List<LocalizedText> { new LocalizedText("fi", null) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingEmptyShortDescriptionValue()
        {
            sut = CreateService();
            sut.ShortDescriptions = new List<LocalizedText> { new LocalizedText("fi", string.Empty) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SettingShortDescriptionHavingUnsupportedLanguage()
        {
            sut = CreateService();
            sut.ShortDescriptions = new List<LocalizedText> { new LocalizedText("en", "good") };
        }

        private static Application.Service.Service CreateService()
        {
            return new Application.Service.Service(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "palvelu") },
                new List<LocalizedText> { new LocalizedText("fi", "Hyvä") }, new List<LocalizedText> { new LocalizedText("fi", "Jee!") },
                new List<Language> { new Language("fi", "suomi") }, new List<string> { "fi" });
        }
    }
}
