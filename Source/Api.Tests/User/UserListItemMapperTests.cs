using System;
using Affecto.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Settings;
using OrganizationRegister.Api.User;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.Api.Tests.User
{
    [TestClass]
    public class UserListItemMapperTests
    {
        private UserListItemMapper sut;
        private IMapper<IRole, Role> roleMapper; 
        private IUserListItem source;
        private UserListItem destination;

        [TestInitialize]
        public void Setup()
        {
            roleMapper = Substitute.For<IMapper<IRole, Role>>();
            sut = new UserListItemMapper(roleMapper);
            source = Substitute.For<IUserListItem>();
        }

        [TestMethod]
        public void IdIsMapped()
        {
            Guid expected = Guid.NewGuid();
            source.Id.Returns(expected);

            destination = sut.Map(source);

            Assert.AreEqual(expected, destination.Id);
        }

        [TestMethod]
        public void RoleIsMapped()
        {
            Role expected = new Role { Id = Guid.NewGuid(), Name = "Test" };
            roleMapper.Map(source.Role).Returns(expected);

            destination = sut.Map(source);

            Assert.AreSame(expected, destination.Role);
        }

        [TestMethod]
        public void EmailAddressIsMapped()
        {
            const string expected = "ExpectedValue";
            source.EmailAddress.Returns(expected);

            destination = sut.Map(source);

            Assert.AreEqual(expected, destination.EmailAddress);
        }

        [TestMethod]
        public void LastNameIsMapped()
        {
            const string expected = "ExpectedValue";
            source.LastName.Returns(expected);

            destination = sut.Map(source);

            Assert.AreEqual(expected, destination.LastName);
        }

        [TestMethod]
        public void FirstNameIsMapped()
        {
            const string expected = "ExpectedValue";
            source.FirstName.Returns(expected);

            destination = sut.Map(source);

            Assert.AreEqual(expected, destination.FirstName);
        }
    }
}