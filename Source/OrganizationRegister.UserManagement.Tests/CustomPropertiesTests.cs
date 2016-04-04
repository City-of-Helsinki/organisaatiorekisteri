using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Interfaces.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Common.User;

namespace OrganizationRegister.UserManagement.Tests
{
    [TestClass]
    public class CustomPropertiesTests
    {
        private static readonly string ExpectedLastName = "Clarkson";
        private static readonly string ExpectedFirstName = "Jeremy";
        private static readonly string ExpectedEmailAddress = "clarkson@bbc.co.uk";
        private static readonly string ExpectedPhoneNumber = "419373";
        private static readonly Guid ExpectedOrganizationId = Guid.NewGuid();

        [TestMethod]
        public void ValuesAreDefaultedToNullWithDefaultConstructor()
        {
            var sut = new CustomProperties();

            Assert.IsNull(sut.LastName);
            Assert.IsNull(sut.FirstName);
            Assert.IsNull(sut.EmailAddress);
            Assert.IsNull(sut.PhoneNumber);
            Assert.IsNull(sut.OrganizationId);
        }

        [TestMethod]
        public void ValuesAreDefaultedToNullWithEmptyList()
        {
            var sut = new CustomProperties(new List<ICustomProperty>());

            Assert.IsNull(sut.LastName);
            Assert.IsNull(sut.FirstName);
            Assert.IsNull(sut.EmailAddress);
            Assert.IsNull(sut.PhoneNumber);
            Assert.IsNull(sut.OrganizationId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotInitializeWithNullCustomPropertiesArgument()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new CustomProperties((IEnumerable<ICustomProperty>) null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotInitializeWithNullKeyValuePairsArgument()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new CustomProperties((IEnumerable<KeyValuePair<string, string>>) null);
        }

        [TestMethod]
        public void ValuesAreInitialized()
        {
            var lastName = new CustomProperty(CustomPropertyName.LastName.ToString(), ExpectedLastName);
            var firstName = new CustomProperty(CustomPropertyName.FirstName.ToString(), ExpectedFirstName);
            var emailAddress = new CustomProperty(CustomPropertyName.EmailAddress.ToString(), ExpectedEmailAddress);
            var phoneNumber = new CustomProperty(CustomPropertyName.PhoneNumber.ToString(), ExpectedPhoneNumber);
            var organizationId = new CustomProperty(CustomPropertyName.OrganizationId.ToString(), ExpectedOrganizationId.ToString());

            var customProperties = new List<ICustomProperty> { lastName, firstName, emailAddress, phoneNumber, organizationId };
            var sut = new CustomProperties(customProperties);

            Assert.AreEqual<string>(ExpectedLastName, sut.LastName);
            Assert.AreEqual<string>(ExpectedFirstName, sut.FirstName);
            Assert.AreEqual<string>(ExpectedEmailAddress, sut.EmailAddress);
            Assert.AreEqual<string>(ExpectedPhoneNumber, sut.PhoneNumber);
            Assert.AreEqual<Guid?>(ExpectedOrganizationId, sut.OrganizationId);
            Assert.AreEqual<string>(ExpectedOrganizationId.ToString("D"), sut.OrganizationIdString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrganizationIdCannotBeInitializedWithInvalidValue()
        {
            var organizationId = new CustomProperty(CustomPropertyName.OrganizationId.ToString(), "FooBar");

            var customProperties = new List<ICustomProperty> { organizationId };
            // ReSharper disable once ObjectCreationAsStatement
            new CustomProperties(customProperties);
        }

        [TestMethod]
        public void InitializedValuesAreConvertedToKeyValuePairs()
        {
            var lastName = new CustomProperty(CustomPropertyName.LastName.ToString(), ExpectedLastName);
            var firstName = new CustomProperty(CustomPropertyName.FirstName.ToString(), ExpectedFirstName);
            var emailAddress = new CustomProperty(CustomPropertyName.EmailAddress.ToString(), ExpectedEmailAddress);
            var phoneNumber = new CustomProperty(CustomPropertyName.PhoneNumber.ToString(), ExpectedPhoneNumber);
            var organizationId = new CustomProperty(CustomPropertyName.OrganizationId.ToString(), ExpectedOrganizationId.ToString());

            var sut = new CustomProperties(new List<ICustomProperty> { lastName, firstName, emailAddress, phoneNumber, organizationId });
            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(5, keyValuePairs.Count);

            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.LastName.ToString() && p.Value == ExpectedLastName));
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.FirstName.ToString() && p.Value == ExpectedFirstName));
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.EmailAddress.ToString() && p.Value == ExpectedEmailAddress));
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.PhoneNumber.ToString() && p.Value == ExpectedPhoneNumber));
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.OrganizationId.ToString() && p.Value == ExpectedOrganizationId.ToString()));
        }

        [TestMethod]
        public void UnknownPropertiesAreInitializedAndSaved()
        {
            var property1 = new CustomProperty("Foo", "Bar");
            var property2 = new CustomProperty("Top", "Gear");

            var sut = new CustomProperties(new List<ICustomProperty> { property1, property2 });
            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(2, keyValuePairs.Count);

            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == "Foo" && p.Value == "Bar"));
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == "Top" && p.Value == "Gear"));
        }

        [TestMethod]
        public void LastNameCanBeSetAndRetrieved()
        {
            var sut = new CustomProperties
            {
                LastName = ExpectedLastName
            };

            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(1, keyValuePairs.Count);
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.LastName.ToString() && p.Value == ExpectedLastName));
            Assert.AreEqual<string>(ExpectedLastName, sut.LastName);
        }

        [TestMethod]
        public void FirstNameCanBeSetAndRetrieved()
        {
            var sut = new CustomProperties
            {
                FirstName = ExpectedFirstName
            };

            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(1, keyValuePairs.Count);
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.FirstName.ToString() && p.Value == ExpectedFirstName));
            Assert.AreEqual<string>(ExpectedFirstName, sut.FirstName);
        }

        [TestMethod]
        public void EmailAddressCanBeSetAndRetrieved()
        {
            var sut = new CustomProperties
            {
                EmailAddress = ExpectedEmailAddress
            };

            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(1, keyValuePairs.Count);
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.EmailAddress.ToString() && p.Value == ExpectedEmailAddress));
            Assert.AreEqual<string>(ExpectedEmailAddress, sut.EmailAddress);
        }

        [TestMethod]
        public void PhoneNumberCanBeSetAndRetrieved()
        {
            var sut = new CustomProperties
            {
                PhoneNumber = ExpectedPhoneNumber
            };

            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(1, keyValuePairs.Count);
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.PhoneNumber.ToString() && p.Value == ExpectedPhoneNumber));
            Assert.AreEqual<string>(ExpectedPhoneNumber, sut.PhoneNumber);
        }

        [TestMethod]
        public void OrganizationIdCanBeSetAndRetrieved()
        {
            var sut = new CustomProperties
            {
                OrganizationId = ExpectedOrganizationId
            };

            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(1, keyValuePairs.Count);
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.OrganizationId.ToString() && p.Value == ExpectedOrganizationId.ToString()));
            Assert.AreEqual<Guid?>(ExpectedOrganizationId, sut.OrganizationId);
            Assert.AreEqual<string>(ExpectedOrganizationId.ToString("D"), sut.OrganizationIdString);
        }

        [TestMethod]
        public void EmptyOrganizationIdIsConvertedToNull()
        {
            var sut = new CustomProperties
            {
                OrganizationId = Guid.Empty
            };

            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(1, keyValuePairs.Count);
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.OrganizationId.ToString() && p.Value == null));
            Assert.IsNull(sut.OrganizationId);
            Assert.IsNull(sut.OrganizationIdString);
        }

        [TestMethod]
        public void MultipleValuesCanBeSet()
        {
            var lastName1 = new CustomProperty(CustomPropertyName.LastName.ToString(), "Foo");
            var lastName2 = new CustomProperty(CustomPropertyName.LastName.ToString(), "Bar");

            var sut = new CustomProperties(new List<ICustomProperty> { lastName1, lastName2 });
            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(2, keyValuePairs.Count);

            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.LastName.ToString() && p.Value == "Foo"));
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.LastName.ToString() && p.Value == "Bar"));
        }

        [TestMethod]
        public void MultipleValuesAreErasedWhenNewValueIsSet()
        {
            var lastName1 = new CustomProperty(CustomPropertyName.LastName.ToString(), "Foo");
            var lastName2 = new CustomProperty(CustomPropertyName.LastName.ToString(), "Bar");

            var sut = new CustomProperties(new List<ICustomProperty> { lastName1, lastName2 });
            sut.LastName = ExpectedLastName;

            IReadOnlyCollection<KeyValuePair<string, string>> keyValuePairs = sut.ToKeyValuePairs();

            Assert.IsNotNull(keyValuePairs);
            Assert.AreEqual(1, keyValuePairs.Count);
            Assert.IsNotNull(keyValuePairs.SingleOrDefault(p => p.Key == CustomPropertyName.LastName.ToString() && p.Value == ExpectedLastName));
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredCustomPropertyMissingException))]
        public void OrganizationIdCannotBeNullWhenValidated()
        {
            var lastName = new CustomProperty(CustomPropertyName.LastName.ToString(), ExpectedLastName);
            var firstName = new CustomProperty(CustomPropertyName.FirstName.ToString(), ExpectedFirstName);
            var emailAddress = new CustomProperty(CustomPropertyName.EmailAddress.ToString(), ExpectedEmailAddress);

            var sut = new CustomProperties(new List<ICustomProperty> { lastName, firstName, emailAddress });
            sut.ValidateRequiredProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredCustomPropertyMissingException))]
        public void EmailAddressCannotBeNullWhenValidated()
        {
            var lastName = new CustomProperty(CustomPropertyName.LastName.ToString(), ExpectedLastName);
            var firstName = new CustomProperty(CustomPropertyName.FirstName.ToString(), ExpectedFirstName);
            var organizationId = new CustomProperty(CustomPropertyName.OrganizationId.ToString(), ExpectedOrganizationId.ToString());

            var sut = new CustomProperties(new List<ICustomProperty> { lastName, firstName, organizationId });
            sut.ValidateRequiredProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredCustomPropertyMissingException))]
        public void LastNameCannotBeNullWhenValidated()
        {
            var firstName = new CustomProperty(CustomPropertyName.FirstName.ToString(), ExpectedFirstName);
            var emailAddress = new CustomProperty(CustomPropertyName.EmailAddress.ToString(), ExpectedEmailAddress);
            var organizationId = new CustomProperty(CustomPropertyName.OrganizationId.ToString(), ExpectedOrganizationId.ToString());

            var sut = new CustomProperties(new List<ICustomProperty> { firstName, emailAddress, organizationId });
            sut.ValidateRequiredProperties();
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredCustomPropertyMissingException))]
        public void FirstNameCannotBeNullWhenValidated()
        {
            var lastName = new CustomProperty(CustomPropertyName.LastName.ToString(), ExpectedLastName);
            var emailAddress = new CustomProperty(CustomPropertyName.EmailAddress.ToString(), ExpectedEmailAddress);
            var organizationId = new CustomProperty(CustomPropertyName.OrganizationId.ToString(), ExpectedOrganizationId.ToString());

            var sut = new CustomProperties(new List<ICustomProperty> { lastName, emailAddress, organizationId });
            sut.ValidateRequiredProperties();
        }

        [TestMethod]
        public void RequiredPropertiesAreValidated()
        {
            var lastName = new CustomProperty(CustomPropertyName.LastName.ToString(), ExpectedLastName);
            var firstName = new CustomProperty(CustomPropertyName.FirstName.ToString(), ExpectedFirstName);
            var emailAddress = new CustomProperty(CustomPropertyName.EmailAddress.ToString(), ExpectedEmailAddress);
            var organizationId = new CustomProperty(CustomPropertyName.OrganizationId.ToString(), ExpectedOrganizationId.ToString());

            var sut = new CustomProperties(new List<ICustomProperty> { lastName, firstName, emailAddress, organizationId });
            sut.ValidateRequiredProperties();
        }

        internal class CustomProperty : ICustomProperty
        {
            public CustomProperty(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; private set; }
            public string Value { get; private set; }
        }
    }
}