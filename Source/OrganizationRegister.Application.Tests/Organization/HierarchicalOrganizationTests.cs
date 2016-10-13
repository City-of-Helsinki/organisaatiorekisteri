using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            sut = new HierarchicalOrganization(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "org")}, null, DateTime.Now, DateTime.Now.AddDays(1));
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

        private static HierarchicalOrganization CreateHierarchicalOrganization()
        {
            return new HierarchicalOrganization(Guid.NewGuid(), new List<LocalizedText> { new LocalizedText("fi", "org2") }, null, DateTime.Now.AddDays(30), DateTime.Now.AddDays(60));
        }
    }
}
