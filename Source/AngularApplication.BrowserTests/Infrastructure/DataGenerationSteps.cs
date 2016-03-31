using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Patterns.Cqrs;
using Affecto.Testing.SpecFlow;
using Autofac;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Commanding.Service.Commands;
using OrganizationRegister.Common;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AngularApplication.BrowserTests.Infrastructure
{
    [Binding]
    internal class DataGenerationSteps
    {
        [Given(@"an organization exists with the name '(.+)'")]
        public void GivenAnOrganizationExistsWithTheName(string name)
        {
            IOrganizationService organizationService = ScenarioContext.Current.Get<IOrganizationService>();
            CurrentScenarioContext.OrganizationId = organizationService.AddOrganization("1029764-5", null, "Yritykset", null, new List<LocalizedText> { new LocalizedText("fi", name)}, 
                null); 
        }

        [Given(@"a service exists for the organization with following details")]
        public void GivenAServiceExistsForTheOrganizationWithFollowingDetails(Table table)
        {
            TableRow serviceData = table.Rows.First();
            AddServiceWithBasicInformation(serviceData);
            SetServiceClassificationInformation(serviceData);
        }

        private static void SetServiceClassificationInformation(TableRow serviceData)
        {
            var container = ScenarioContext.Current.Get<IContainer>();
            IClassificationRepository classificationRepository = container.Resolve<IClassificationRepository>();
            ICommandBus commandBus = container.Resolve<ICommandBus>();

            IReadOnlyCollection<string> serviceServiceClasses = serviceData.SplitCommaSeparatedText("Service classes");
            IReadOnlyCollection<string> serviceOntologyTerms = serviceData.SplitCommaSeparatedText("Ontology terms");
            IReadOnlyCollection<string> serviceTargetGroups = serviceData.SplitCommaSeparatedText("Target groups");
            IReadOnlyCollection<string> serviceLifeEvents = serviceData.SplitCommaSeparatedText("Life events");
            string serviceKeywords = serviceData["Keywords"];

            IReadOnlyCollection<IHierarchicalClass> serviceClasses = classificationRepository.GetServiceClassHierarchy();
            IReadOnlyCollection<IHierarchicalClass> ontologyTerms = classificationRepository.GetOntologyTermHierarchy();
            IReadOnlyCollection<IHierarchicalClass> targetGroups = classificationRepository.GetTargetGroupHierarchy();
            IReadOnlyCollection<IHierarchicalClass> lifeEvents = classificationRepository.GetLifeEventHierarchy();

            IEnumerable<Guid> serviceServiceClassIds = GetHierarchicalClassIds(serviceServiceClasses, serviceClasses);
            IEnumerable<Guid> serviceOntologyTermIds = GetHierarchicalClassIds(serviceOntologyTerms, ontologyTerms);
            IEnumerable<Guid> serviceTargetGroupIds = GetHierarchicalClassIds(serviceTargetGroups, targetGroups);
            IEnumerable<Guid> serviceLifeEventIds = GetHierarchicalClassIds(serviceLifeEvents, lifeEvents);

            SetServiceClassification command = new SetServiceClassification(CurrentScenarioContext.OrganizationId, CurrentScenarioContext.ServiceId, serviceServiceClassIds, 
                serviceOntologyTermIds, serviceTargetGroupIds, serviceLifeEventIds, CreateLocalizedTextList("fi", serviceKeywords));
            commandBus.Send(Envelope.Create(command));
        }

        private static IEnumerable<Guid> GetHierarchicalClassIds(IReadOnlyCollection<string> classNames, IReadOnlyCollection<IHierarchicalClass> allClasses)
        {
            var classIds = new List<Guid>();
            foreach (string className in classNames)
            {
                classIds.Add(GetClass(className, allClasses).Id);
            }
            return classIds;
        }

        private static IHierarchicalClass GetClass(string className, IReadOnlyCollection<IHierarchicalClass> classes)
        {
            if (classes.Any(c => c.Name.Equals(className)))
            {
                return classes.Single(c => c.Name.Equals(className));
            }
            foreach (IHierarchicalClass @class in classes.Where(c => c.SubClasses != null && c.SubClasses.Any()))
            {
                return GetClass(className, @class.SubClasses.ToList());
            }
            throw new ArgumentException(string.Format("Class '{0}' not found.", className));
        }

        private static void AddServiceWithBasicInformation(TableRow serviceData)
        {
            IServiceService serviceService = ScenarioContext.Current.Get<IServiceService>();
            CurrentScenarioContext.ServiceId = serviceService.AddService(
                CurrentScenarioContext.OrganizationId,
                CreateLocalizedTextList("fi", serviceData["Service name"]),
                CreateLocalizedTextList("fi", serviceData["Alternate name"]),
                CreateLocalizedTextList("fi", serviceData["Description"]),
                CreateLocalizedTextList("fi", serviceData["Short description"]),
                CreateLocalizedTextList("fi", serviceData["User instructions"]),
                LanguageHelper.GetLanguageCodes(serviceData.SplitCommaSeparatedText("Languages")),
                CreateLocalizedTextList("fi", serviceData["Requirements"]));
        }

        private static List<LocalizedText> CreateLocalizedTextList(string languageCode, string textValue)
        {
            return new List<LocalizedText> { new LocalizedText(languageCode, textValue) };
        }

    }
}