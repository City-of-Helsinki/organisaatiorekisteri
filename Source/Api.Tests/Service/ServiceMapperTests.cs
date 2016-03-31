using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Classification;
using OrganizationRegister.Api.Service;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Tests.Service
{
    [TestClass]
    public class ServiceMapperTests
    {
        private ServiceMapper sut;
        private IService source;
        private Api.Service.Service destination;

        [TestInitialize]
        public void Setup()
        {
            source = Substitute.For<IService>();
            sut = new ServiceMapper();
        }

        [TestMethod]
        public void IdIsMapped()
        {
            Guid id = Guid.NewGuid();
            source.Id.Returns(id);

            destination = sut.Map(source);

            Assert.AreEqual(id, destination.Id);
        }

        [TestMethod]
        public void NumericIdIsMapped()
        {
            const long id = 1;
            source.NumericId.Returns(id);

            destination = sut.Map(source);

            Assert.AreEqual(id, destination.NumericId);
        }

        [TestMethod]
        public void NamesAreMapped()
        {
            var names = new List<LocalizedText> { new LocalizedText() };
            source.Names.Returns(names);

            destination = sut.Map(source);

            Assert.AreSame(names.Single(), destination.Names.Single());
        }

        [TestMethod]
        public void AlternateNamesAreMapped()
        {
            var names = new List<LocalizedText> { new LocalizedText() };
            source.AlternateNames.Returns(names);

            destination = sut.Map(source);

            Assert.AreSame(names.Single(), destination.AlternateNames.Single());
        }

        [TestMethod]
        public void DescriptionsAreMapped()
        {
            var descriptions = new List<LocalizedText> { new LocalizedText() };
            source.Descriptions.Returns(descriptions);

            destination = sut.Map(source);

            Assert.AreSame(descriptions.Single(), destination.Descriptions.Single());
        }

        [TestMethod]
        public void ShortDescriptionsAreMapped()
        {
            var descriptions = new List<LocalizedText> { new LocalizedText() };
            source.ShortDescriptions.Returns(descriptions);

            destination = sut.Map(source);

            Assert.AreSame(descriptions.Single(), destination.ShortDescriptions.Single());
        }

        [TestMethod]
        public void UserInstructionsAreMapped()
        {
            var instructions = new List<LocalizedText> { new LocalizedText() };
            source.UserInstructions.Returns(instructions);

            destination = sut.Map(source);

            Assert.AreEqual(instructions.Count, destination.UserInstructions.Count());
            Assert.AreSame(instructions.Single(), destination.UserInstructions.Single());
        }

        [TestMethod]
        public void RequirementsAreMapped()
        {
            var requirements = new List<LocalizedText> { new LocalizedText() };
            source.Requirements.Returns(requirements);

            destination = sut.Map(source);

            Assert.AreSame(requirements.Single(), destination.Requirements.Single());
        }

        [TestMethod]
        public void LanguagesAreMapped()
        {
            var languages = new List<Language> { new Language("FI", "suomi") };
            source.Languages.Returns(languages);

            destination = sut.Map(source);

            Assert.AreSame(languages.Single(), destination.Languages.Single());
        }

        [TestMethod]
        public void ServiceClassesAreMapped()
        {
            const string name = "name";
            Guid id = Guid.NewGuid();

            IHierarchicalClass sourceClass = Substitute.For<IHierarchicalClass>();
            sourceClass.Name.Returns(name);
            sourceClass.Id.Returns(id);
            var serviceClasses = new List<IHierarchicalClass> { sourceClass };
            source.ServiceClasses.Returns(serviceClasses);

            destination = sut.Map(source);

            HierarchicalClass destinationClass = destination.ServiceClasses.Single();
            Assert.AreEqual(name, destinationClass.Name);
            Assert.AreEqual(id, destinationClass.Id);
        }

        [TestMethod]
        public void TargetGroupsAreMapped()
        {
            const string name = "name";
            Guid id = Guid.NewGuid();

            IHierarchicalClass sourceGroup = Substitute.For<IHierarchicalClass>();
            sourceGroup.Name.Returns(name);
            sourceGroup.Id.Returns(id);
            var targetGroups = new List<IHierarchicalClass> { sourceGroup };
            source.TargetGroups.Returns(targetGroups);

            destination = sut.Map(source);

            HierarchicalClass destinationGroup = destination.TargetGroups.Single();
            Assert.AreEqual(name, destinationGroup.Name);
            Assert.AreEqual(id, destinationGroup.Id);
        }

        [TestMethod]
        public void LifeEventsAreMapped()
        {
            const string name = "name";
            Guid id = Guid.NewGuid();

            IHierarchicalClass sourceEvent = Substitute.For<IHierarchicalClass>();
            sourceEvent.Name.Returns(name);
            sourceEvent.Id.Returns(id);
            var lifeEvents = new List<IHierarchicalClass> { sourceEvent };
            source.LifeEvents.Returns(lifeEvents);

            destination = sut.Map(source);

            HierarchicalClass destinationEvent = destination.LifeEvents.Single();
            Assert.AreEqual(name, destinationEvent.Name);
            Assert.AreEqual(id, destinationEvent.Id);
        }

        [TestMethod]
        public void OntologyTermsAreMapped()
        {
            const string name = "name";
            Guid id = Guid.NewGuid();

            IClass sourceTerm = Substitute.For<IClass>();
            sourceTerm.Name.Returns(name);
            sourceTerm.Id.Returns(id);
            var ontologyTerms = new List<IClass> { sourceTerm };
            source.OntologyTerms.Returns(ontologyTerms);

            destination = sut.Map(source);

            Class destinationTerm = destination.OntologyTerms.Single();
            Assert.AreEqual(name, destinationTerm.Name);
            Assert.AreEqual(id, destinationTerm.Id);
        }

        [TestMethod]
        public void KeywordsAreMapped()
        {
            var keywords = new List<LocalizedText> { new LocalizedText() };
            source.Keywords.Returns(keywords);

            destination = sut.Map(source);

            Assert.AreSame(keywords.Single(), destination.Keywords.Single());
        }
    }
}
