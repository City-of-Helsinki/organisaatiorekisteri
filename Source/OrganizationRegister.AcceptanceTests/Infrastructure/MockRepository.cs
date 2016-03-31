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

        public void RemoveAllServiceLanguages()
        {
            foreach (AvailableServiceLanguage serviceLanguage in context.ServiceLanguages.ToList())
            {
                context.ServiceLanguages.Remove(serviceLanguage);
            }
            context.SaveChanges();
        }

        public void AddServiceLanguage(string languageCode, int? orderNumber)
        {
            Language serviceLanguage = context.Languages.Single(l => l.Code.Equals(languageCode));
            context.ServiceLanguages.Add(new AvailableServiceLanguage { Language = serviceLanguage, OrderNumber = orderNumber });
            context.SaveChanges();
        }

        public void AddLifeEvent(string name, string parentName, int? orderNumber)
        {
            string sourceParentId = null;
            if (!string.IsNullOrWhiteSpace(parentName))
            {
                sourceParentId = context.LifeEvents.Single(@event => @event.Name.Equals(parentName)).SourceId;
            }

            context.LifeEvents.Add(new LifeEvent
            {
                Id = Guid.NewGuid(),
                Name = name,
                SourceId = name,
                SourceParentId = sourceParentId,
                OrderNumber = orderNumber
            });
            context.SaveChanges();
        }

        public void AddServiceClass(string name, string parentName, int? orderNumber)
        {
            string sourceParentId = null;
            if (!string.IsNullOrWhiteSpace(parentName))
            {
                sourceParentId = context.ServiceClasses.Single(@class => @class.Name.Equals(parentName)).SourceId;
            }

            context.ServiceClasses.Add(new ServiceClass
            {
                Id = Guid.NewGuid(),
                Name = name,
                SourceId = name,
                SourceParentId = sourceParentId,
                OrderNumber = orderNumber
            });
            context.SaveChanges();
        }

        public void AddOntologyTerm(string name, string parentName, int? orderNumber)
        {
            string sourceParentId = null;
            if (!string.IsNullOrWhiteSpace(parentName))
            {
                sourceParentId = context.OntologyTerms.Single(term => term.Name.Equals(parentName)).SourceId;
            }

            context.OntologyTerms.Add(new OntologyTerm
            {
                Id = Guid.NewGuid(),
                Name = name,
                LowerCaseName = name.ToLower(),
                SourceId = name,
                SourceParentId = sourceParentId,
                OrderNumber = orderNumber
            });
            context.SaveChanges();
        }

        public void AddTargetGroup(string name, string parentName, int? orderNumber)
        {
            string sourceParentId = null;
            if (!string.IsNullOrWhiteSpace(parentName))
            {
                sourceParentId = context.TargetGroups.Single(group => group.Name.Equals(parentName)).SourceId;
            }

            context.TargetGroups.Add(new TargetGroup
            {
                Id = Guid.NewGuid(),
                Name = name,
                SourceId = name,
                SourceParentId = sourceParentId,
                OrderNumber = orderNumber
            });
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

        public IEnumerable<Guid> GetServiceClassIds(IReadOnlyCollection<string> serviceClassNames)
        {
            return context.ServiceClasses.Where(@class => serviceClassNames.Contains(@class.Name)).Select(@class => @class.Id);
        }

        public IEnumerable<Guid> GetOntologyTermIds(IReadOnlyCollection<string> ontologyTermNames)
        {
            return context.OntologyTerms.Where(term => ontologyTermNames.Contains(term.Name)).Select(term => term.Id);
        }

        public IEnumerable<Guid> GetLifeEventIds(IReadOnlyCollection<string> lifeEventNames)
        {
            return context.LifeEvents.Where(@event => lifeEventNames.Contains(@event.Name)).Select(@event => @event.Id);
        }

        public IEnumerable<Guid> GetTargetGroupIds(IReadOnlyCollection<string> targetGroupNames)
        {
            return context.TargetGroups.Where(@group => targetGroupNames.Contains(@group.Name)).Select(@group => @group.Id);
        }

        private static WebPageType CreateWebPageType(string type)
        {
            return new WebPageType { Id = Guid.NewGuid(), Type = type };
        }
    }
}