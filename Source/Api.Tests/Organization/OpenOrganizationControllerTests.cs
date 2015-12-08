﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ServiceRegister.Api.Organization;
using ServiceRegister.Application.Organization;

namespace ServiceRegister.Api.Tests.Organization
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
            organizationService.GetActiveOrganizationHierarchy().Returns(new List<IHierarchicalOrganization> { appReturnValue, appReturnValue });

            var result = sut.GetOrganizationHierarchy() as OkNegotiatedContentResult<IEnumerable<HierarchicalOrganization>>;

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
            organizationService.GetActiveMainOrganizations().Returns(new List<IOrganizationName> { appReturnValue });

            var result = sut.GetMainOrganizations() as OkNegotiatedContentResult<IEnumerable<OrganizationName>>;

            Assert.AreSame(returnValue, result.Content.Single());
        }

        [TestMethod]
        public void GetOrganizations()
        {
            OrganizationName returnValue = new OrganizationName();
            IOrganizationName appReturnValue = Substitute.For<IOrganizationName>();
            organizationNameMapper.Map(appReturnValue).Returns(returnValue);
            organizationService.GetActiveOrganizations().Returns(new List<IOrganizationName> { appReturnValue });

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