using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Tests.Classification
{
    [TestClass]
    public class HierarchicalClassTests
    {
        private HierarchicalClass sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new HierarchicalClass(Guid.NewGuid(), "class", "sourceClass", null, null);
        }

        [TestMethod]
        public void NullIsNotMyChild()
        {
            Assert.IsFalse(sut.IsMyChild(null));
        }

        [TestMethod]
        public void ObjectOfDifferentTypeIsNotMyChild()
        {
            Assert.IsFalse(sut.IsMyChild(CreateHierarchicalOrganization()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullChildren()
        {
            sut.AddChildren(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AdChildrenOfDifferentType()
        {
            sut.AddChildren(new List<IHierarchical> { CreateHierarchicalOrganization() });
        }

        private IHierarchical CreateHierarchicalOrganization()
        {
            return new HierarchicalOrganization(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "org") }, null);
        }
    }
}
