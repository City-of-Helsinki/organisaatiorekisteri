// ReSharper disable ObjectCreationAsStatement

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Affecto.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Api.User;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.Api.Tests.User
{
    [TestClass]
    public class AuthorizedUserControllerTests
    {
        private AuthorizedUserController sut;
        private IUserService userService;
        private IOrganizationService organizationService;
        private MapperFactory mapperFactory;
        private IMapper<IUserListItem, UserListItem> userListItemMapper;
        private IMapper<IOrganizationName, OrganizationName> organizationNameMapper;

        private static readonly Api.User.User User = new Api.User.User
        {
            OrganizationId = Guid.NewGuid(),
            RoleId = Guid.NewGuid(),
            EmailAddress = "e@mail.com",
            Password = "pass",
            LastName = "Last",
            FirstName = "First",
            PhoneNumber = "33-91823121"
        };

        [TestInitialize]
        public void Setup()
        {
            SetupMappers();
            userService = Substitute.For<IUserService>();
            organizationService = Substitute.For<IOrganizationService>();
            sut = new AuthorizedUserController(userService, new Lazy<IOrganizationService>(() => organizationService), mapperFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserServiceCannotBeNull()
        {
            new AuthorizedUserController(null, new Lazy<IOrganizationService>(() => organizationService), mapperFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OrganizationServiceCannotBeNull()
        {
            new AuthorizedUserController(userService, null, mapperFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MapperFactoryCannotBeNull()
        {
            new AuthorizedUserController(userService, new Lazy<IOrganizationService>(() => organizationService), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserCannotBeNullWhenAddingUser()
        {
            sut.AddUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmailAddressCannotBeNullWhenCheckingExistingUser()
        {
            sut.IsExistingUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmailAddressCannotBeEmptyWhenCheckingExistingUser()
        {
            sut.IsExistingUser(string.Empty);
        }

        [TestMethod]
        public void ResultIsReturnedWhenCheckingExistingUser()
        {
            const string email = "foo@bar.com";
            userService.IsExistingUser(email).Returns(true);

            var result = sut.IsExistingUser(email) as OkNegotiatedContentResult<bool>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content);
        }

        [TestMethod]
        public void UserServiceIsCalledWhenAddingUser()
        {
            sut.AddUser(User);

            userService.Received(1).AddUser(User.RoleId, User.OrganizationId, User.EmailAddress, User.Password, User.LastName, User.FirstName, User.PhoneNumber);
        }

        [TestMethod]
        public void UserIdIsReturnedWhenAddingUser()
        {
            Guid expectedUserId = Guid.NewGuid();
            userService
                .AddUser(User.RoleId, User.OrganizationId, User.EmailAddress, User.Password, User.LastName, User.FirstName, User.PhoneNumber)
                .Returns(expectedUserId);

            var result = sut.AddUser(User) as OkNegotiatedContentResult<Guid>;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUserId, result.Content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrganizationIdCannotBeEmptyWhenGettingUsers()
        {
            sut.GetInternalUsers(Guid.Empty);
        }

        [TestMethod]
        public void UsersAreReturnedWhenGettingUsers()
        {
            Guid organizationId = Guid.NewGuid();
            IOrganizationName appOrganization = Substitute.For<IOrganizationName>();
            OrganizationName apiOrganization = new OrganizationName();
            organizationNameMapper.Map(appOrganization).Returns(apiOrganization);

            IUserListItem appUser1 = Substitute.For<IUserListItem>();
            UserListItem apiUser1 = new UserListItem();
            userListItemMapper.Map(appUser1).Returns(apiUser1);
            IUserListItem appUser2 = Substitute.For<IUserListItem>();
            UserListItem apiUser2 = new UserListItem();
            userListItemMapper.Map(appUser2).Returns(apiUser2);
            var appUsers = new List<IUserListItem> { appUser1, appUser2 };

            organizationService.GetOrganizationName(organizationId).Returns(appOrganization);
            userService.GetInternalUsers(organizationId).Returns(appUsers);

            var result = sut.GetInternalUsers(organizationId) as OkNegotiatedContentResult<IEnumerable<UserListItem>>;

            Assert.IsNotNull(result);
            IEnumerable<UserListItem> users = result.Content;
            Assert.IsNotNull(users);

            Assert.AreEqual(2, users.Count());
            Assert.IsTrue(users.Contains(apiUser1));
            Assert.IsTrue(users.Contains(apiUser2));

            Assert.AreSame(apiOrganization, users.ElementAt(0).Organization);
            Assert.AreSame(apiOrganization, users.ElementAt(1).Organization);
        }

        [TestMethod]
        public void ValidatePasswordStrength()
        {
            const string password = "secret";
            const bool isValid = true;
            userService.ValidatePasswordStrength(password).Returns(isValid);

            OkNegotiatedContentResult<bool> result = sut.ValidatePasswordStrength(password) as OkNegotiatedContentResult<bool>;

            Assert.IsNotNull(result);
            Assert.AreEqual(isValid, result.Content);
        }

        private void SetupMappers()
        {
            mapperFactory = Substitute.For<MapperFactory>();
            userListItemMapper = Substitute.For<IMapper<IUserListItem, UserListItem>>();
            organizationNameMapper = Substitute.For<IMapper<IOrganizationName, OrganizationName>>();
            mapperFactory.CreateUserListItemMapper().Returns(userListItemMapper);
            mapperFactory.CreateOrganizationNameMapper().Returns(organizationNameMapper);
        }
    }
}