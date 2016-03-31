using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Classification;
using OrganizationRegister.Application.Classification;

namespace OrganizationRegister.Api.Tests.Classification
{
    [TestClass]
    public class HierarchicalClassMapperTests
    {
        private HierarchicalClassMapper sut;
        private IHierarchicalClass source;
        private HierarchicalClass destination;

        [TestInitialize]
        public void Setup()
        {
            sut = new HierarchicalClassMapper();
            source = Substitute.For<IHierarchicalClass>();
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
        public void NameIsMapped()
        {
            const string name = "event";
            source.Name.Returns(name);

            destination = sut.Map(source);

            Assert.AreEqual(name, destination.Name);
        }

        [TestMethod]
        public void SubLifeEventsAreMapped()
        {
            IEnumerable<IHierarchicalClass> subEvents = new List<IHierarchicalClass> { Substitute.For<IHierarchicalClass>() };
            source.SubClasses.Returns(subEvents);

            destination = sut.Map(source);

            Assert.AreEqual(1, destination.SubClasses.Count());
        }
    }
}
