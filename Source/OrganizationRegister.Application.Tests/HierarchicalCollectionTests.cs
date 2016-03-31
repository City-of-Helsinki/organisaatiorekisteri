using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Application.Tests
{
    [TestClass]
    public class HierarchicalCollectionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrganizationWithUnexistingParent()
        {
            IHierarchicalOrganization rootOrganization = Substitute.For<IHierarchicalOrganization>();
            rootOrganization.HasParent.Returns(false);
            IHierarchicalOrganization subOrganization = Substitute.For<IHierarchicalOrganization>();
            subOrganization.HasParent.Returns(true);
            rootOrganization.IsMyChild(subOrganization).Returns(false);
            HierarchicalCollection<IHierarchicalOrganization>.CreateHierarchy(new List<IHierarchicalOrganization> { rootOrganization, subOrganization });
        }
    }
}