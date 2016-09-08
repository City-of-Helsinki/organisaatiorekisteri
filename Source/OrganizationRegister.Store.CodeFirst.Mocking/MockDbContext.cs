using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Migrations;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Mocking
{
    internal class MockDbContext : StoreContext
    {
        public MockDbContext(bool initializeClassificationsAndOrganizations = true)
            : base(Effort.DbConnectionFactory.CreateTransient())
        {
            AddInitialLanguages();
            AddOrganizationTypes(new List<string> { "Kunta", "Yritys", "Valtio" });
            AddWebPageTypes(new List<string> { "Kotisivu", "Sosiaalisen median palvelu" });
            AddCallChargeTypes(new List<string> { "Maksuton", "Paikallisverkko-, paikallispuhelu- tai matkapuhelinmaksu", "Muu" });

            if (initializeClassificationsAndOrganizations)
            {
                AddOrganization(Guid.Parse("7B45E3BC-EDA9-4F6B-97BB-E9354DB660B5"), "Valtio", "Väestörekisterikeskus", "0245437-2");
            }
        }

        private void AddCallChargeTypes(List<string> callChargeTypes)
        {
            foreach (string type in callChargeTypes)
            {
                CallChargeTypes.Add(CreateCallChargeTypee(type));
            }
            SaveChanges();
        }

        private static CallChargeType CreateCallChargeTypee(string type)
        {
            return new CallChargeType { Id = Guid.NewGuid(), Type = type };
        }

        private void AddOrganization(Guid id, string type, string finnishName, string businessId)
        {
            Organizations.Add(new Organization
            {
                Id = id,
                Type = OrganizationTypes.Single(t => t.Name.Equals(type)),
                BusinessId = businessId,
                LanguageSpecifications = new List<OrganizationLanguageSpecification>
                {
                    new OrganizationLanguageSpecification
                    {
                        Language = DataLanguages.Single(l => l.Language.Code.Equals("fi")),
                        Name = finnishName
                    }
                }
            });
            SaveChanges();
        }

        private void AddWebPageTypes(IEnumerable<string> webPageTypes)
        {
            foreach (string type in webPageTypes)
            {
                WebPageTypes.Add(CreateWebPageType(type));
            }
            SaveChanges();
        }

        private static WebPageType CreateWebPageType(string type)
        {
            return new WebPageType { Id = Guid.NewGuid(), Type = type };
        }

        public void AddLanguage(string languageCode, string languageName)
        {
            Languages.Add(CreateLanguage(languageCode, languageName));
            SaveChanges();
        }

        private void AddOrganizationTypes(IEnumerable<string> providerTypeNames)
        {
            foreach (string name in providerTypeNames)
            {
                OrganizationTypes.Add(CreateOrganizationType(name));
            }
            SaveChanges();
        }

        private void AddInitialLanguages()
        {
            Languages.Add(CreateLanguage("fi", "suomi"));
            Languages.Add(CreateLanguage("en", "englanti"));
            Languages.Add(CreateLanguage("sv", "ruotsi"));
            SaveChanges();
            SetAllLanguagesAsDataLanguages();
        }

        private void SetAllLanguagesAsDataLanguages()
        {
            foreach (Language language in Languages)
        {
                DataLanguages.Add(new AvailableDataLanguage { Language = language });
            }
            SaveChanges();
        }
        
        private static OrganizationType CreateOrganizationType(string name)
        {
            return new OrganizationType { Id = Guid.NewGuid(), Name = name };
        }

        private static Language CreateLanguage(string code, string description)
        {
            return new Language { Id = Guid.NewGuid(), Code = code, Name = description };
        }
    }
}