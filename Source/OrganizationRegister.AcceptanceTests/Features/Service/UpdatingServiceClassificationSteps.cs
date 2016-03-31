using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Testing.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Commanding.Service.Commands;
using OrganizationRegister.Common;
using OrganizationRegister.Tests.Infrastructure;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features.Service
{
    [Binding]
    [Scope(Feature = "UpdatingServiceClassification")]
    internal sealed class UpdatingServiceClassificationSteps : StepDefinition
    {
        [Given(@"there are following service classes:")]
        public void GivenThereAreFollowingServiceClasses(Table serviceClasses)
        {
            foreach (TableRow serviceClass in serviceClasses.Rows)
            {
                Repository.AddServiceClass(serviceClass["Name"], serviceClass["Parent"], null);
            }
        }

        [Given(@"there are following ontology terms:")]
        public void GivenThereAreFollowingOntologyTerms(Table ontologyTerms)
        {
            foreach (TableRow ontologyTerm in ontologyTerms.Rows)
            {
                Repository.AddOntologyTerm(ontologyTerm["Name"], ontologyTerm["Parent"], null);
            }
        }

        [Given(@"there are following target groups:")]
        public void GivenThereAreFollowingTargetGroups(Table targetGroups)
        {
            foreach (TableRow targetGroup in targetGroups.Rows)
            {
                Repository.AddTargetGroup(targetGroup["Name"], targetGroup["Parent"], null);
            }
        }

        [Given(@"there are following life events:")]
        public void GivenThereAreFollowingLifeEvents(Table lifeEvents)
        {
            foreach (TableRow lifeEvent in lifeEvents.Rows)
            {
                Repository.AddLifeEvent(lifeEvent["Name"], lifeEvent["Parent"], null);
            }
        }

        [Given(@"the following classification is set to the service:")]
        [When(@"the following classification is set to the service:")]
        public void WhenTheFollowingClassificationIsSetToTheService(Table classificationData)
        {
            TableRow data = classificationData.Rows.Single();
            IEnumerable<Guid> serviceClasses = Repository.GetServiceClassIds(data.SplitCommaSeparatedText("Service classes"));
            IEnumerable<Guid> ontologyTerms = Repository.GetOntologyTermIds(data.SplitCommaSeparatedText("Ontology terms"));
            IEnumerable<Guid> lifeEvents = Repository.GetLifeEventIds(data.SplitCommaSeparatedText("Life events"));
            IEnumerable<Guid> targetGroups = Repository.GetTargetGroupIds(data.SplitCommaSeparatedText("Target groups"));
            IEnumerable<LocalizedText> keywords = LocalizedTextHelper.CreateKeywordsCollection(data);
            var command = new SetServiceClassification(CurrentScenarioContext.OrganizationId, CurrentScenarioContext.ServiceId, serviceClasses, ontologyTerms, targetGroups, lifeEvents,
                keywords);
            SendCommand(command);
        }

        [Then(@"the service has the following classification:")]
        public void ThenTheServiceHasTheFollowingClassification(Table classificationData)
        {
            TableRow data = classificationData.Rows.Single();
            IEnumerable<IHierarchicalClass> serviceClasses =
                ClassificationRepository.GetServiceClassHierarchy().Where(@class => data.SplitCommaSeparatedText("Service classes").Contains(@class.Name));
            IEnumerable<IHierarchicalClass> lifeEvents =
                ClassificationRepository.GetLifeEventHierarchy().Where(@event => data.SplitCommaSeparatedText("Life events").Contains(@event.Name));
            IEnumerable<IHierarchicalClass> targetGroups =
                ClassificationRepository.GetTargetGroupHierarchy().Where(@group => data.SplitCommaSeparatedText("Target groups").Contains(@group.Name));

            IService service = ServiceService.GetService(CurrentScenarioContext.OrganizationId, CurrentScenarioContext.ServiceId);
            AssertHierarchicalClasses(serviceClasses.ToList(), service.ServiceClasses.ToList());
            AssertHierarchicalClasses(lifeEvents.ToList(), service.LifeEvents.ToList());
            AssertHierarchicalClasses(targetGroups.ToList(), service.TargetGroups.ToList());
            AssertOntologyTerms(data, service);
            AssertKeywords(data, service);

            IServiceListItem serviceListItem = ServiceService.GetServices(CurrentScenarioContext.OrganizationId).Single();
            Assert.AreEqual(data["Service classes"], serviceListItem.ServiceClasses);
            Assert.AreEqual(data["Ontology terms"], serviceListItem.OntologyTerms);
        }

        private static void AssertOntologyTerms(TableRow data, IService service)
        {
            string[] expectedOntologyTerms = data.SplitCommaSeparatedText("Ontology terms").ToArray();
            IClass[] actualOntologyTerms = service.OntologyTerms.ToArray();
            Assert.AreEqual(expectedOntologyTerms.Length, actualOntologyTerms.Length);
            for (int i = 0; i < expectedOntologyTerms.Length; i++)
            {
                Assert.AreEqual(expectedOntologyTerms[i], actualOntologyTerms[i].Name);
            }
        }

        private static void AssertKeywords(TableRow data, IClassification service)
        {
            string finnishKeywords = data["Finnish keywords"];
            string swedishKeywords = data["Swedish keywords"];
            if (string.IsNullOrWhiteSpace(finnishKeywords))
            {
                Assert.IsFalse(service.Keywords.Any(keyword => keyword.LanguageCode.Equals("fi")));
            }
            else
            {
                Assert.AreEqual(finnishKeywords, service.Keywords.Single(keyword => keyword.LanguageCode.Equals("fi")).LocalizedValue);
            }
            if (string.IsNullOrWhiteSpace(swedishKeywords))
            {
                Assert.IsFalse(service.Keywords.Any(keyword => keyword.LanguageCode.Equals("sv")));
            }
            else
            {
                Assert.AreEqual(swedishKeywords, service.Keywords.Single(keyword => keyword.LanguageCode.Equals("sv")).LocalizedValue);
            }
        }

        private static void AssertHierarchicalClasses(IReadOnlyCollection<IHierarchicalClass> expected, IReadOnlyCollection<IHierarchicalClass> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (IHierarchicalClass expectedClass in expected)
            {
                IHierarchicalClass actualClass = expected.Single(@class => @class.Name.Equals(expectedClass.Name) && @class.Id.Equals(expectedClass.Id));
                AssertHierarchicalClasses(expectedClass.SubClasses.ToList(), actualClass.SubClasses.ToList());
            }
        }
    }
}
