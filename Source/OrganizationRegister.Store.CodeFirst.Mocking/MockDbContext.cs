using System;
using System.Collections.Generic;
using System.Linq;
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

            if (initializeClassificationsAndOrganizations)
            {
                AddServiceClasses();
                AddOntologyTerms();
                AddLifeEvents();
                AddTargetGroups();
                AddOrganization(Guid.Parse("7B45E3BC-EDA9-4F6B-97BB-E9354DB660B5"), "Valtio", "Väestörekisterikeskus", "0245437-2");
            }
        }

        private void AddTargetGroups()
        {
            TargetGroups.Add(CreateTargetGroup("Viranomaiset", null));
            SaveChanges();
            TargetGroups.Add(CreateTargetGroup("Kansalaiset", null));
            SaveChanges();
            TargetGroups.Add(CreateTargetGroup("Nuoret", "Kansalaiset"));
            SaveChanges();
            TargetGroups.Add(CreateTargetGroup("Vammaiset", "Kansalaiset"));
            SaveChanges();
        }

        private TargetGroup CreateTargetGroup(string name, string parentName)
        {
            Guid id = Guid.NewGuid();
            return new TargetGroup
            {
                Id = id,
                Name = name,
                SourceId = id.ToString(),
                SourceParentId = string.IsNullOrWhiteSpace(parentName) ? null : TargetGroups.Single(le => le.Name.Equals(parentName)).SourceId
            };
        }

        private void AddLifeEvents()
        {
            LifeEvents.Add(CreateLifeEvent("Lapsen saaminen", null));
            SaveChanges();
            LifeEvents.Add(CreateLifeEvent("Omaisen kuolema", null));
            SaveChanges();
            LifeEvents.Add(CreateLifeEvent("Adoption", "Lapsen saaminen"));
            SaveChanges();
        }

        private LifeEvent CreateLifeEvent(string name, string parentName)
        {
            Guid id = Guid.NewGuid();
            return new LifeEvent
            {
                Id = id,
                Name = name,
                SourceId = id.ToString(),
                SourceParentId = string.IsNullOrWhiteSpace(parentName) ? null : LifeEvents.Single(le => le.Name.Equals(parentName)).SourceId
            };
        }

        private void AddOntologyTerms()
        {
            OntologyTerms.Add(CreateOntologyTerm("Opetus", null));
            SaveChanges();
            OntologyTerms.Add(CreateOntologyTerm("Päivähoito", null));
            SaveChanges();
            OntologyTerms.Add(CreateOntologyTerm("Työväenopisto", "Opetus"));
            SaveChanges();
            OntologyTerms.Add(CreateOntologyTerm("kirjasto(työväenopisto)", "Työväenopisto"));
            SaveChanges();
        }

        private OntologyTerm CreateOntologyTerm(string name, string parentName)
        {
            Guid id = Guid.NewGuid();
            return new OntologyTerm
            {
                Id = id,
                Name = name,
                LowerCaseName = name.ToLower(),
                SourceId = id.ToString(),
                SourceParentId = string.IsNullOrWhiteSpace(parentName) ? null : OntologyTerms.Single(term => term.Name.Equals(parentName)).SourceId
            };
        }

        private void AddServiceClasses()
        {
            ServiceClasses.Add(CreateServiceClass("Elinkeinot", null));
            SaveChanges();
            ServiceClasses.Add(CreateServiceClass("Kulttuuri", null));
            SaveChanges();
            ServiceClasses.Add(CreateServiceClass("Tuote- ja palvelukehitys", "Elinkeinot"));
            SaveChanges();
        }

        private ServiceClass CreateServiceClass(string name, string parentName)
        {
            Guid id = Guid.NewGuid();
            return new ServiceClass
            {
                Id = id,
                Name = name,
                SourceId = id.ToString(),
                SourceParentId = string.IsNullOrWhiteSpace(parentName) ? null : ServiceClasses.Single(sc => sc.Name.Equals(parentName)).SourceId
            };
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

        private WebPageType CreateWebPageType(string type)
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
            SetAllLanguagesAsServiceLanguages();
        }

        private void SetAllLanguagesAsServiceLanguages()
        {
            foreach (Language language in Languages)
            {
                ServiceLanguages.Add(new AvailableServiceLanguage { Language = language });
            }
            SaveChanges();
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