using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OrganizationRegister.Application;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Common;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class Service
    {
        public Service()
        {
            Languages = new Collection<ServiceLanguage>();
            LanguageSpecifications = new Collection<ServiceLanguageSpecification>();
            ServiceClasses = new Collection<ServiceServiceClass>();
            LifeEvents = new Collection<ServiceLifeEvent>();
            TargetGroups = new Collection<ServiceTargetGroup>();
            OntologyTerms = new Collection<ServiceOntologyTerm>();
            Keywords = new Collection<ServiceKeyword>();
        }

        public Guid Id { get; set; }
        public long NumericId { get; set; }
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<ServiceLanguage> Languages { get; set; }
        public virtual ICollection<ServiceLanguageSpecification> LanguageSpecifications { get; set; }
        public virtual ICollection<ServiceServiceClass> ServiceClasses { get; set; }
        public virtual ICollection<ServiceLifeEvent> LifeEvents { get; set; }
        public virtual ICollection<ServiceTargetGroup> TargetGroups { get; set; }
        public virtual ICollection<ServiceOntologyTerm> OntologyTerms { get; set; }
        public virtual ICollection<ServiceKeyword> Keywords { get; set; }

        internal IEnumerable<LocalizedText> GetNames()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.Name));
        }

        internal IEnumerable<AvailableServiceLanguage> GetLanguages()
        {
            return Languages.Select(lang => lang.Language);
        }

        internal IEnumerable<string> GetServiceClassNames()
        {
            return ServiceClasses.Select(sc => sc.ServiceClass.Name);
        }

        internal IEnumerable<string> GetOntologyTermNames()
        {
            return OntologyTerms.Select(term => term.OntologyTerm.Name);
        }

        internal void SetBasicInformation(IBasicInformation information, IStoreContext context)
        {
            RemoveAllLanguages();
            foreach (string languageCode in information.LanguagesCodes)
            {
                Languages.Add(new ServiceLanguage
                {
                    Language = context.GetServiceLanguage(languageCode)
                });
            }

            RemoveAllLanguageSpecificData();
            foreach (LocalizedText name in information.Names)
            {
                LanguageSpecifications.Add(new ServiceLanguageSpecification
                {
                    Language = context.GetDataLanguage(name.LanguageCode),
                    Name = name.LocalizedValue,
                    AlternateName = information.GetAlternateName(name.LanguageCode),
                    Description = information.GetDescription(name.LanguageCode),
                    ShortDescription = information.GetShortDescription(name.LanguageCode),
                    UserInstructions = information.GetUserInstructions(name.LanguageCode),
                    Requirements = information.GetRequirements(name.LanguageCode)
                });
            }
        }

        internal void RemoveAllLanguages()
        {
            foreach (ServiceLanguage language in Languages.ToList())
            {
                Languages.Remove(language);
            }
        }

        internal void RemoveAllLanguageSpecificData()
        {
            foreach (ServiceLanguageSpecification specification in LanguageSpecifications.ToList())
            {
                LanguageSpecifications.Remove(specification);
            }
        }

        internal void RemoveAllClassificationData(IStoreContext context)
        {
            RemoveServiceClasses(context);
            RemoveOntologyTerms(context);
            RemoveTargetGroups(context);
            RemoveLifeEvents(context);
            RemoveKeywords(context);
        }

        public void SetServiceClasses(IEnumerable<Guid> serviceClassIds, IStoreContext context)
        {
            RemoveServiceClasses(context);

            if (serviceClassIds == null)
            {
                return;
            }
            foreach (var classId in serviceClassIds)
            {
                ServiceClasses.Add(new ServiceServiceClass
                {
                    ServiceClass = context.ServiceClasses.Single(c => c.Id.Equals(classId))
                });
            }
        }

        public void SetLifeEvents(IEnumerable<Guid> lifeEventIds, IStoreContext context)
        {
            RemoveLifeEvents(context);

            if (lifeEventIds == null)
            {
                return;
            }
            foreach (var lifeEventId in lifeEventIds)
            {
                LifeEvents.Add(new ServiceLifeEvent
                {
                    LifeEvent = context.LifeEvents.Single(l => l.Id.Equals(lifeEventId))
                });
            }
        }

        public void SetOntologyTerms(IEnumerable<Guid> ontologyTermIds, IStoreContext context)
        {
            RemoveOntologyTerms(context);

            if (ontologyTermIds == null)
            {
                return;
            }
            foreach (var ontologyTermsId in ontologyTermIds)
            {
                OntologyTerms.Add(new ServiceOntologyTerm
                {
                    OntologyTerm = context.OntologyTerms.Single(o => o.Id.Equals(ontologyTermsId))
                });
            }
        }

        public void SetTargetGroups(IEnumerable<Guid> targetGroupIds, IStoreContext context)
        {
            RemoveTargetGroups(context);

            if (targetGroupIds == null)
            {
                return;
            }
            foreach (var targetGroupId in targetGroupIds)
            {
                TargetGroups.Add(new ServiceTargetGroup
                {
                    TargetGroup = context.TargetGroups.Single(t => t.Id.Equals(targetGroupId))
                });
            }
        }

        public void SetKeywords(IEnumerable<LocalizedText> keywords, IStoreContext context)
        {
            RemoveKeywords(context);

            if (keywords == null)
            {
                return;
            }
            foreach (var keyword in keywords)
            {
                Keywords.Add(new ServiceKeyword
                {
                    Id = Guid.NewGuid(),
                    Language = context.GetDataLanguage(keyword.LanguageCode),
                    Keyword = keyword.LocalizedValue
                });
            }
            
        }

        internal OrganizationRegister.Application.Service.Dto.Service ToDto()
        {
            return new OrganizationRegister.Application.Service.Dto.Service
            {
                Id = Id,
                NumericId = NumericId,
                Names = GetNames(),
                AlternateNames = GetAlternateNames(),
                Descriptions = GetDescriptions(),
                ShortDescriptions = GetShortDescriptions(),
                UserInstructions = GetUserInstructions(),
                Requirements = GetRequirements(),
                Languages = GetOrderedServiceLanguages(),
                ServiceClasses = GetOrderedServiceClassHierarchy(),
                TargetGroups = GetOrderedTargetGroupHierarchy(),
                LifeEvents = GetOrderedLifeEventHierarchy(),
                OntologyTerms = GetOntologyTerms(),
                Keywords = GetKeywords()
            };
        }

        private IEnumerable<OrganizationRegister.Application.Classification.IClass> GetOntologyTerms()
        {
            return OntologyTerms.Select(term => term.OntologyTerm).Select(term => ClassFactory.CreateClass(term.Id, term.Name));
        }

        private IEnumerable<LocalizedText> GetKeywords()
        {
            return Keywords.Select(word => new LocalizedText(word.Language.Language.Code, word.Keyword));
        }

        private IEnumerable<IHierarchicalClass> GetOrderedLifeEventHierarchy()
        {
            IEnumerable<LifeEvent> lifeEvents = LifeEvents.Select(@event => @event.LifeEvent);
            return ClassHierarchyBuilder.CreateOrderedClassHierarchy(lifeEvents);
        }

        private IEnumerable<IHierarchicalClass> GetOrderedTargetGroupHierarchy()
        {
            IEnumerable<TargetGroup> targetGroups = TargetGroups.Select(@group => @group.TargetGroup);
            return ClassHierarchyBuilder.CreateOrderedClassHierarchy(targetGroups);
        }

        private IEnumerable<IHierarchicalClass> GetOrderedServiceClassHierarchy()
        {
            IEnumerable<ServiceClass> serviceClasses = ServiceClasses.Select(@class => @class.ServiceClass);
            return ClassHierarchyBuilder.CreateOrderedClassHierarchy(serviceClasses);
        }

        private IEnumerable<OrganizationRegister.Common.Language> GetOrderedServiceLanguages()
        {
            IEnumerable<AvailableServiceLanguage> languages = GetLanguages();
            OrderableCollection<ILanguage> serviceLanguages = new OrderableCollection<ILanguage>(languages
                .Select(l => LanguageFactory.CreateLanguage(l.Language.Code, l.Language.Name, l.OrderNumber)));
            return serviceLanguages.Order().Select(l => new OrganizationRegister.Common.Language(l.Code, l.Name));
        }

        private IEnumerable<LocalizedText> GetAlternateNames()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.AlternateName));
        }

        private IEnumerable<LocalizedText> GetDescriptions()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.Description));
        }

        private IEnumerable<LocalizedText> GetShortDescriptions()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.ShortDescription));
        }

        private IEnumerable<LocalizedText> GetUserInstructions()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.UserInstructions));
        }

        private IEnumerable<LocalizedText> GetRequirements()
        {
            return LanguageSpecifications.Select(data => new LocalizedText(data.Language.Language.Code, data.Requirements));
        }

        private void RemoveServiceClasses(IStoreContext context)
        {
            foreach (ServiceServiceClass serviceClass in ServiceClasses.ToList())
            {
                context.ServiceServiceClasses.Remove(serviceClass);
                ServiceClasses.Remove(serviceClass);
            }
        }

        private void RemoveTargetGroups(IStoreContext context)
        {
            foreach (ServiceTargetGroup targetGroup in TargetGroups.ToList())
            {
                context.ServiceTargetGroups.Remove(targetGroup);
                TargetGroups.Remove(targetGroup);
            }
        }

        private void RemoveOntologyTerms(IStoreContext context)
        {
            foreach (ServiceOntologyTerm ontologyTerm in OntologyTerms.ToList())
            {

                context.ServiceOntologyTerms.Remove(ontologyTerm);
                OntologyTerms.Remove(ontologyTerm);
            }
        }

        private void RemoveLifeEvents(IStoreContext context)
        {
            foreach (ServiceLifeEvent lifeEvent in LifeEvents.ToList())
            {
                context.ServiceLifeEvents.Remove(lifeEvent);
                LifeEvents.Remove(lifeEvent);
            }
        }

        private void RemoveKeywords(IStoreContext context)
        {
            foreach (ServiceKeyword keyword in Keywords.ToList())
            {
                context.ServiceKeywords.Remove(keyword);
                Keywords.Remove(keyword);
            }
        }
    }
}
