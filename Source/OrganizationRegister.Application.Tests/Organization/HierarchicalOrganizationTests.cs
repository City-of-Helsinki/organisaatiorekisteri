using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Tests.Organization
{
    [TestClass]
    public class HierarchicalOrganizationTests
    {
        private HierarchicalOrganization sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new HierarchicalOrganization(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "org")}, null);
        }

        [TestMethod]
        public void NullIsNotMyChild()
        {
            Assert.IsFalse(sut.IsMyChild(null));
        }

        [TestMethod]
        public void ObjectOfDifferentTypeIsNotMyChild()
        {
            Assert.IsFalse(sut.IsMyChild(CreateHierarchicalClass()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullChildren()
        {
            sut.AddChildren(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddChildrenOfDifferentType()
        {
            sut.AddChildren(new List<IHierarchical> { CreateHierarchicalClass() });
        }

        private static HierarchicalClass CreateHierarchicalClass()
        {
            return new HierarchicalClass(Guid.NewGuid(), "class", "sourceid", null, null);
        }
    }
}
