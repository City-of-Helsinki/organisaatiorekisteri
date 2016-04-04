using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Mocking;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.AcceptanceTests.Infrastructure
{
    internal class MockRepository
    {
        private readonly MockDbContext context;

        public MockRepository(MockDbContext context)
        {
            this.context = context;
        }
        public void SetWebPageTypes(IEnumerable<string> webPageTypes)
        {
            RemoveAllWebPageTypes();
            AddWebPageTypes(webPageTypes);
        }

        private void AddWebPageTypes(IEnumerable<string> webPageTypes)
        {
            foreach (string type in webPageTypes)
            {
                context.WebPageTypes.Add(CreateWebPageType(type));
            }
            context.SaveChanges();
        }

        private void RemoveAllWebPageTypes()
        {
            foreach (WebPageType type in context.WebPageTypes.ToList())
            {
                context.WebPageTypes.Remove(type);
            }
            context.SaveChanges();
        }

        public void SetProviderTypes(IEnumerable<string> providerTypeNames)
        {
            RemoveAllProviderTypes();
            AddProviderTypes(providerTypeNames);
        }

        public void AddLanguage(string languageCode, string languageName)
        {
            context.AddLanguage(languageCode, languageName);
        }

        public void RemoveAllLanguages()
        {
            foreach (Language language in context.Languages.ToList())
            {
                context.Languages.Remove(language);
            }
            context.SaveChanges();
        }

        private void AddProviderTypes(IEnumerable<string> providerTypeNames)
        {
            foreach (string name in providerTypeNames)
            {
                context.OrganizationTypes.Add(CreateProviderType(name));
            }
            context.SaveChanges();
        }

        private void RemoveAllProviderTypes()
        {
            foreach (OrganizationType type in context.OrganizationTypes.ToList())
            {
                context.OrganizationTypes.Remove(type);
            }
            context.SaveChanges();
        }

        private static OrganizationType CreateProviderType(string name)
        {
            return new OrganizationType { Id = Guid.NewGuid(), Name = name };
        }

        private static WebPageType CreateWebPageType(string type)
        {
            return new WebPageType { Id = Guid.NewGuid(), Type = type };
        }
    }
}