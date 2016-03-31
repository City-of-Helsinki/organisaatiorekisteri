using System.Collections.Generic;
using System.Linq;
using Affecto.Testing.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.AcceptanceTests.Infrastructure;
using OrganizationRegister.Application.Classification;
using TechTalk.SpecFlow;

namespace OrganizationRegister.AcceptanceTests.Features
{
    [Binding]
    [Scope(Feature = "Classification")]
    internal class ClassificationSteps : StepDefinition
    {
        private List<IHierarchicalClass> results;
        private IClass[] ontologyTermsResult;
            
        [Given(@"there are following life events:")]
        public void GivenThereAreFollowingLifeEvents(Table lifeEventData)
        {
            foreach (TableRow lifeEvent in lifeEventData.Rows)
            {
                string orderNumber = lifeEvent.GetOptionalValue("Order");
                Repository.AddLifeEvent(lifeEvent["Name"], lifeEvent["Parent"], string.IsNullOrWhiteSpace(orderNumber) ? (int?)null : int.Parse(orderNumber));
            }
        }
        
        [When(@"the life events are retrievd")]
        public void WhenTheLifeEventsAreRetrievd()
        {
            results = ClassificationRepository.GetLifeEventHierarchy().ToList();
        }
        
        [Then(@"the following main service classes are received:")]
        [Then(@"the following main life events are received:")]
        [Then(@"the following main ontology terms are received:")]
        [Then(@"the following main target groups are received:")]
        public void ThenTheFollowingMainLifeEventsAreReceived(Table classData)
        {
            Assert.AreEqual(classData.RowCount, results.Count);
            for (int i = 0; i < classData.RowCount; i++)
            {
                Assert.AreEqual(classData.Rows[i]["Name"], results[i].Name);
            }
        }
        
        [Then(@"service class '(.+)' has the following child service classes:")]
        [Then(@"life event '(.+)' has the following child life events:")]
        [Then(@"ontology term '(.+)' has the following child ontology terms:")]
        [Then(@"target group '(.+)' has the following child target groups:")]
        public void ThenLifeEventHasTheFollowingChildLifeEvents(string parentClassName, Table childClassData)
        {
            List<IHierarchicalClass> childClasses = ClassHelper.GetClass(results, parentClassName).SubClasses.ToList();
            for (int i = 0; i < childClassData.RowCount; i++)
            {
                Assert.AreEqual(childClassData.Rows[i]["Name"], childClasses[i].Name);
            }
        }

        [Given(@"there are following service classes:")]
        public void GivenThereAreFollowingServiceClasses(Table serviceClassData)
        {
            foreach (TableRow serviceClass in serviceClassData.Rows)
            {
                string orderNumber = serviceClass.GetOptionalValue("Order");
                Repository.AddServiceClass(serviceClass["Name"], serviceClass["Parent"], string.IsNullOrWhiteSpace(orderNumber) ? (int?)null : int.Parse(orderNumber));
            }
        }

        [When(@"the service classes are retrievd")]
        public void WhenTheServiceClassesAreRetrievd()
        {
            results = ClassificationRepository.GetServiceClassHierarchy().ToList();
        }

        [Given(@"there are following ontology terms:")]
        public void GivenThereAreFollowingOntologyTerms(Table ontologyTermData)
        {
            foreach (TableRow ontologyTerm in ontologyTermData.Rows)
            {
                string orderNumber = ontologyTerm.GetOptionalValue("Order");
                Repository.AddOntologyTerm(ontologyTerm["Name"], ontologyTerm["Parent"], string.IsNullOrWhiteSpace(orderNumber) ? (int?)null : int.Parse(orderNumber));
            }
        }

        [When(@"the ontology terms are retrievd")]
        public void WhenTheOntologyTermsAreRetrievd()
        {
            results = ClassificationRepository.GetOntologyTermHierarchy().ToList();
        }

        [Then(@"ontology term '(.+)' has no child ontology terms")]
        public void ThenOntologyTermHasNoChildOntologyTerms(string ontologyTermName)
        {
            Assert.IsFalse(ClassHelper.GetClass(results, ontologyTermName).SubClasses.Any());
        }

        [Given(@"there are following target groups:")]
        public void GivenThereAreFollowingTargetGroups(Table targetGroupData)
        {
            foreach (TableRow targetGroup in targetGroupData.Rows)
            {
                string orderNumber = targetGroup.GetOptionalValue("Order");
                Repository.AddTargetGroup(targetGroup["Name"], targetGroup["Parent"], string.IsNullOrWhiteSpace(orderNumber) ? (int?)null : int.Parse(orderNumber));
            }
        }

        [When(@"the target groups are retrievd")]
        public void WhenTheTargetGroupsAreRetrievd()
        {
            results = ClassificationRepository.GetTargetGroupHierarchy().ToList();
        }

        [When(@"ontology terms are searched with text '(\w+)'")]
        public void WhenOntologyTermsAreSearchedWithText(string searchText)
        {
            ontologyTermsResult = ClassificationRepository.GetFlatOntologyTerms(searchText).ToArray();
        }

        [When(@"ontology terms are searched with text '(\w+)' and a maximum result amount of '(\d+)'")]
        public void WhenOntologyTermsAreSearchedWithTextAndAMaximumResultAmountOf(string searchText, int maxResults)
        {
            ontologyTermsResult = ClassificationRepository.GetFlatOntologyTerms(searchText, maxResults).ToArray();
        }

        [Then(@"the following ontology terms are received:")]
        public void ThenTheFollowingOntologyTermsAreReceived(Table expectedOntologyTable)
        {
            Assert.AreEqual(expectedOntologyTable.RowCount, ontologyTermsResult.Length);
            TableRows expectedOntologyTerms = expectedOntologyTable.Rows;
            for (int i = 0; i < expectedOntologyTable.RowCount; i++)
            {
                Assert.AreEqual(expectedOntologyTerms[i]["Name"], ontologyTermsResult[i].Name);
            }
        }

        [Then(@"no ontology terms are received")]
        public void ThenNoOntologyTermsAreReceived()
        {
            Assert.IsFalse(ontologyTermsResult.Any());
        }
    }
}
