using System;
using Affecto.Authentication.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Common.User;

namespace ServiceRegister.Common.Tests.User
{
    [TestClass]
    public class AuthenticationExtensionMethodsTests
    {
        private IAuthenticatedUserContext userContext;

        [TestInitialize]
        public void Setup()
        {
            userContext = Substitute.For<IAuthenticatedUserContext>();
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredCustomPropertyMissingException))]
        public void ExceptionIsThrownIfOrganizationIdIsNotPresent()
        {
            userContext.HasCustomProperty(CustomPropertyName.OrganizationId.ToString()).Returns(false);
            userContext.GetUserOrganizationId();
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredCustomPropertyMissingException))]
        public void ExceptionIsThrownIfOrganizationIdIsNotGuid()
        {
            userContext.HasCustomProperty(CustomPropertyName.OrganizationId.ToString()).Returns(true);
            userContext.GetCustomPropertyValue(CustomPropertyName.OrganizationId.ToString()).Returns("FooBar");

            userContext.GetUserOrganizationId();
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredCustomPropertyMissingException))]
        public void ExceptionIsThrownIfOrganizationIdIsOfInvalidFormat()
        {
            Guid expected = Guid.NewGuid();
            userContext.HasCustomProperty(CustomPropertyName.OrganizationId.ToString()).Returns(true);
            userContext.GetCustomPropertyValue(CustomPropertyName.OrganizationId.ToString()).Returns(expected.ToString("N"));

            userContext.GetUserOrganizationId();
        }

        [TestMethod]
        public void OrganizationIdIsReturned()
        {
            Guid expected = Guid.NewGuid();
            userContext.HasCustomProperty(CustomPropertyName.OrganizationId.ToString()).Returns(true);
            userContext.GetCustomPropertyValue(CustomPropertyName.OrganizationId.ToString()).Returns(expected.ToString("D"));

            Guid result = userContext.GetUserOrganizationId();

            Assert.AreEqual(expected, result);
        }
    }
}