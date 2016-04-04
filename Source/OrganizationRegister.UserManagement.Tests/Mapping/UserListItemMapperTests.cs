// ReSharper disable ObjectCreationAsStatement

using System;
using System.Collections.Generic;
using Affecto.IdentityManagement.Interfaces.Model;
using Affecto.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Common.User;
using OrganizationRegister.UserManagement.Mapping;
using OrganizationRegister.UserManagement.Model;

namespace OrganizationRegister.UserManagement.Tests.Mapping
{
    [TestClass]
    public class UserListItemMapperTests
    {
        private static readonly Guid ExpectedId = Guid.NewGuid();
        private static readonly Guid ExpectedOrganizationId = Guid.NewGuid();
        private static readonly string ExpectedEmailAddress = "e@mail.net";
        private static readonly string ExpectedLastName = "Johnson";
        private static readonly string ExpectedFirstName = "Magic";

        private UserListItemMapper sut;
        private IMapper<IRole, Role> roleMapper;
        private IRole sourceRole;
        private Role destinationRole;
        private IUser source;
        private UserListItem destination;

        [TestInitialize]
        public void Setup()
        {
            roleMapper = Substitute.For<IMapper<IRole, Role>>();
            sourceRole = Substitute.For<IRole>();
            destinationRole = new Role();
            roleMapper.Map(sourceRole).Returns(destinationRole);

            sut = new UserListItemMapper(roleMapper);

            source = Substitute.For<IUser>();
            source.Id.Returns(ExpectedId);
            source.Roles.Returns(new List<IRole> { sourceRole });

            ICustomProperty organizationId = Substitute.For<ICustomProperty>();
            organizationId.Name.Returns(CustomPropertyName.OrganizationId.ToString());
            organizationId.Value.Returns(ExpectedOrganizationId.ToString("D"));

            ICustomProperty email = Substitute.For<ICustomProperty>();
            email.Name.Returns(CustomPropertyName.EmailAddress.ToString());
            email.Value.Returns(ExpectedEmailAddress);

            ICustomProperty lastName = Substitute.For<ICustomProperty>();
            lastName.Name.Returns(CustomPropertyName.LastName.ToString());
            lastName.Value.Returns(ExpectedLastName);

            ICustomProperty firstName = Substitute.For<ICustomProperty>();
            firstName.Name.Returns(CustomPropertyName.FirstName.ToString());
            firstName.Value.Returns(ExpectedFirstName);

            var customProperties = new List<ICustomProperty> { organizationId, email, lastName, firstName };
            source.CustomProperties.Returns(customProperties);

            destination = sut.Map(source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RoleMapperCannotBeNull()
        {
            new UserListItemMapper(null);
        }

        [TestMethod]
        public void NullIsMappedToNull()
        {
            Assert.IsNull(sut.Map(null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CustomPropertiesCannotBeNull()
        {
            source.CustomProperties.Returns((IReadOnlyCollection<ICustomProperty>) null);
            sut.Map(source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RolesCannotBeNull()
        {
            source.Roles.Returns((IReadOnlyCollection<IRole>) null);
            sut.Map(source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RolesCannotBeEmpty()
        {
            source.Roles.Returns(new List<IRole>());
            sut.Map(source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UserCannotHaveMultipleRoles()
        {
            source.Roles.Returns(new List<IRole> { Substitute.For<IRole>(), Substitute.For<IRole>() });
            sut.Map(source);
        }

        [TestMethod]
        public void IdIsMapped()
        {
            Assert.AreEqual(ExpectedId, destination.Id);
        }

        [TestMethod]
        public void RoleIsMapped()
        {
            Assert.AreSame(destinationRole, destination.Role);
        }

        [TestMethod]
        public void OrganizationIdIsMapped()
        {
            Assert.AreEqual(ExpectedOrganizationId, destination.OrganizationId);
        }

        [TestMethod]
        public void EmailAddressIsMapped()
        {
            Assert.AreEqual(ExpectedEmailAddress, destination.EmailAddress);
        }

        [TestMethod]
        public void LastNameIsMapped()
        {
            Assert.AreEqual(ExpectedLastName, destination.LastName);
        }

        [TestMethod]
        public void FirstNameIsMapped()
        {
            Assert.AreEqual(ExpectedFirstName, destination.FirstName);
        }
    }
}