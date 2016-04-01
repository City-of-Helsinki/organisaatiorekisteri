using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Affecto.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Settings;
using OrganizationRegister.Application.Settings;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.Api.Tests.Settings
{
    [TestClass]
    public class OpenSettingsControllerTests
    {
        private OpenSettingsController sut;
        private Lazy<ISettingsService> settingsService;
        private Lazy<IUserService> userService;
        private MapperFactory mapperFactory;

        [TestInitialize]
        public void Setup()
        {
            settingsService = new Lazy<ISettingsService>(() => Substitute.For<ISettingsService>());
            userService = new Lazy<IUserService>(() => Substitute.For<IUserService>());
            mapperFactory = Substitute.For<MapperFactory>();
            sut = new OpenSettingsController(settingsService, userService, mapperFactory);
        }

        [TestMethod]
        public void GetOrganizationTypes()
        {
            const string organizationType = "city";
            settingsService.Value.GetOrganizationTypes().Returns(new List<string> { organizationType });

            var result = sut.GetOrganizationTypes() as OkNegotiatedContentResult<IEnumerable<string>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(organizationType, result.Content.Single());
        }

        [TestMethod]
        public void GetWebPageTypes()
        {
            const string webPageType = "kotisivu";
            settingsService.Value.GetWebPageTypes().Returns(new List<string> { webPageType });

            var result = sut.GetWebPageTypes() as OkNegotiatedContentResult<IEnumerable<string>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(webPageType, result.Content.Single());
        }

        [TestMethod]
        public void GetRoles()
        {
            IRole role1 = Substitute.For<IRole>();
            IRole role2 = Substitute.For<IRole>();
            userService.Value.GetRoles().Returns(new List<IRole> { role1, role2 });

            var mapper = Substitute.For<IMapper<IRole, Role>>();
            var expectedRole1 = new Role();
            var expectedRole2 = new Role();
            mapper.Map(role1).Returns(expectedRole1);
            mapper.Map(role2).Returns(expectedRole2);
            mapperFactory.CreateRoleMapper().Returns(mapper);

            var result = sut.GetRoles() as OkNegotiatedContentResult<ICollection<Role>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(2, result.Content.Count);
            Assert.AreSame(expectedRole1, result.Content.First());
            Assert.AreSame(expectedRole2, result.Content.Last());
        }
    }
}
