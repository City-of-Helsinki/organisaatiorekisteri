using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Service;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Tests.Service
{
    [TestClass]
    public class ServiceListItemMapperTests
    {
        private ServiceListItemMapper sut;
        private IServiceListItem source;
        private ServiceListItem destination;

        [TestInitialize]
        public void Setup()
        {
            source = Substitute.For<IServiceListItem>();
            sut = new ServiceListItemMapper();
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
        public void NamesAreMapped()
        {
            var names = new List<LocalizedText> { new LocalizedText() };
            source.Names.Returns(names);

            destination = sut.Map(source);

            Assert.AreSame(names.Single(), destination.Names.Single());
        }

        [TestMethod]
        public void ServiceClassesAreMapped()
        {
            const string serviceClasses = "class1, class2";
            source.ServiceClasses.Returns(serviceClasses);

            destination = sut.Map(source);

            Assert.AreEqual(serviceClasses, destination.ServiceClasses);
        }

        [TestMethod]
        public void OntologyTermsAreMapped()
        {
            const string ontologyTerms = "term";
            source.OntologyTerms.Returns(ontologyTerms);

            destination = sut.Map(source);

            Assert.AreEqual(ontologyTerms, destination.OntologyTerms);
        }
    }
}