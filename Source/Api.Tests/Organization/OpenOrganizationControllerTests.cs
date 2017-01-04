using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Api.Tests.Organization
{
    [TestClass]
    public class OpenOrganizationControllerTests : OrganizationControllerTests
    {
        private IOrganizationService organizationService;
        private OpenOrganizationController sut;

        [TestInitialize]
        public void Setup()
        {
            SetupMappers();
            organizationService = Substitute.For<IOrganizationService>();
            sut = new OpenOrganizationController(organizationService, mapperFactory);
        }

        [TestMethod]
        public void GetOrganizationHierarchy()
        {
            HierarchicalOrganization returnValue = new HierarchicalOrganization();
            IHierarchicalOrganization appReturnValue = Substitute.For<IHierarchicalOrganization>();
            hierarchicalOrganizationMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetOrganizationHierarchy().Returns(new List<IHierarchicalOrganization> { appReturnValue, appReturnValue });

            var result = sut.GetOrganizationHierarchy() as OkNegotiatedContentResult<IEnumerable<HierarchicalOrganization>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count());
            Assert.IsTrue(result.Content.All(org => org.Equals(returnValue)));
        }

        [TestMethod]
        public void GetCurrentOrganizationHierarchy()
        {
            HierarchicalOrganization returnValue = new HierarchicalOrganization();
            IHierarchicalOrganization appReturnValue = Substitute.For<IHierarchicalOrganization>();
            hierarchicalOrganizationMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetOrganizationHierarchy(false).Returns(new List<IHierarchicalOrganization> { appReturnValue, appReturnValue });

            var result = sut.GetCurrentOrganizationHierarchy() as OkNegotiatedContentResult<IEnumerable<HierarchicalOrganization>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count());
            Assert.IsTrue(result.Content.All(org => org.Equals(returnValue)));
        }

        [TestMethod]
        public void GetCurrentAndFutureOrganizationHierarchy()
        {
            HierarchicalOrganization returnValue = new HierarchicalOrganization();
            IHierarchicalOrganization appReturnValue = Substitute.For<IHierarchicalOrganization>();
            hierarchicalOrganizationMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetOrganizationHierarchy(true).Returns(new List<IHierarchicalOrganization> { appReturnValue, appReturnValue });

            var result = sut.GetCurrentAndFutureOrganizationHierarchy() as OkNegotiatedContentResult<IEnumerable<HierarchicalOrganization>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count());
            Assert.IsTrue(result.Content.All(org => org.Equals(returnValue)));
        }


        [TestMethod]
        public void GetOrganizationHierarchyForOrganization()
        {
            Guid organizationId = Guid.NewGuid();
            HierarchicalOrganization returnValue = new HierarchicalOrganization();
            IHierarchicalOrganization appReturnValue = Substitute.For<IHierarchicalOrganization>();
            hierarchicalOrganizationMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetCompleteOrganizationHierarchyForOrganization(organizationId).Returns(new List<IHierarchicalOrganization> { appReturnValue, appReturnValue });

            var result = sut.GetOrganizationHierarchyForOrganization(organizationId) as OkNegotiatedContentResult<IEnumerable<HierarchicalOrganization>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count());
            Assert.IsTrue(result.Content.All(org => org.Equals(returnValue)));
        }

        [TestMethod]
        public void GetCurrentOrganizationHierarchyForOrganization()
        {
            Guid organizationId = Guid.NewGuid();
            HierarchicalOrganization returnValue = new HierarchicalOrganization();
            IHierarchicalOrganization appReturnValue = Substitute.For<IHierarchicalOrganization>();
            hierarchicalOrganizationMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetOrganizationHierarchyForOrganization(organizationId, false).Returns(new List<IHierarchicalOrganization> { appReturnValue, appReturnValue });

            var result = sut.GetCurrentOrganizationHierarchyForOrganization(organizationId) as OkNegotiatedContentResult<IEnumerable<HierarchicalOrganization>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count());
            Assert.IsTrue(result.Content.All(org => org.Equals(returnValue)));
        }

        [TestMethod]
        public void GetCurrentAndFutureOrganizationHierarchyForOrganization()
        {
            Guid organizationId = Guid.NewGuid();
            HierarchicalOrganization returnValue = new HierarchicalOrganization();
            IHierarchicalOrganization appReturnValue = Substitute.For<IHierarchicalOrganization>();
            hierarchicalOrganizationMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetOrganizationHierarchyForOrganization(organizationId, true).Returns(new List<IHierarchicalOrganization> { appReturnValue, appReturnValue });

            var result = sut.GetCurrentAndFutureOrganizationHierarchyForOrganization(organizationId) as OkNegotiatedContentResult<IEnumerable<HierarchicalOrganization>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count());
            Assert.IsTrue(result.Content.All(org => org.Equals(returnValue)));
        }

        [TestMethod]
        public void GetMainOrganizations()
        {
            OrganizationName returnValue = new OrganizationName();
            IOrganizationName appReturnValue = Substitute.For<IOrganizationName>();
            organizationNameMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetMainOrganizations().Returns(new List<IOrganizationName> { appReturnValue });

            var result = sut.GetMainOrganizations() as OkNegotiatedContentResult<IEnumerable<OrganizationName>>;

            Assert.AreSame(returnValue, result.Content.Single());
        }

        [TestMethod]
        public void GetOrganizations()
        {
            OrganizationName returnValue = new OrganizationName();
            IOrganizationName appReturnValue = Substitute.For<IOrganizationName>();
            organizationNameMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetOrganizations().Returns(new List<IOrganizationName> { appReturnValue });

            var result = sut.GetOrganizations() as OkNegotiatedContentResult<IEnumerable<OrganizationName>>;

            Assert.AreSame(returnValue, result.Content.Single());
        }

        [TestMethod]
        public void GetOrganization()
        {
            Guid organizationId = Guid.NewGuid();
            Api.Organization.Organization returnValue = new Api.Organization.Organization();
            IOrganization appReturnValue = Substitute.For<IOrganization>();
            organizationMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetOrganization(organizationId).Returns(appReturnValue);

            var result = sut.GetOrganization(organizationId) as OkNegotiatedContentResult<Api.Organization.Organization>;

            Assert.IsNotNull(result);
            Assert.AreEqual(returnValue, result.Content);
        }
    }
}