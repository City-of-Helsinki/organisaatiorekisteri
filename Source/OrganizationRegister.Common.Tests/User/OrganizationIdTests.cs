using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Common.User;

namespace OrganizationRegister.Common.Tests.User
{
    [TestClass]
    public class OrganizationIdTests
    {
        [TestMethod]
        public void TryConvertValidStringValue()
        {
            Guid expected = Guid.NewGuid();
            Guid result;
            bool isValid = OrganizationId.TryConvert(expected.ToString("D"), out result);

            Assert.IsTrue(isValid);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryConvertValidStringValueInInvalidFormat()
        {
            Guid expected = Guid.NewGuid();
            Guid result;
            bool isValid = OrganizationId.TryConvert(expected.ToString("N"), out result);

            Assert.IsFalse(isValid);
            Assert.AreEqual(Guid.Empty, result);
        }

        [TestMethod]
        public void TryConvertNullString()
        {
            Guid result;
            bool isValid = OrganizationId.TryConvert(null, out result);

            Assert.IsFalse(isValid);
            Assert.AreEqual(Guid.Empty, result);
        }

        [TestMethod]
        public void TryConvertEmptyString()
        {
            Guid result;
            bool isValid = OrganizationId.TryConvert(string.Empty, out result);

            Assert.IsFalse(isValid);
            Assert.AreEqual(Guid.Empty, result);
        }

        [TestMethod]
        public void TryConvertWhitespaceString()
        {
            Guid result;
            bool isValid = OrganizationId.TryConvert(" ", out result);

            Assert.IsFalse(isValid);
            Assert.AreEqual(Guid.Empty, result);
        }

        [TestMethod]
        public void IsValidWithValidValue()
        {
            Guid expected = Guid.NewGuid();
            bool isValid = OrganizationId.IsValid(expected.ToString("D"));

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValidWithValidValueInInvalidFormat()
        {
            Guid expected = Guid.NewGuid();
            bool isValid = OrganizationId.IsValid(expected.ToString("N"));

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidWithNullString()
        {
            bool isValid = OrganizationId.IsValid(null);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidWithEmptyString()
        {
            bool isValid = OrganizationId.IsValid(string.Empty);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidWithWhitespace()
        {
            bool isValid = OrganizationId.IsValid(" ");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ConvertValidStringValue()
        {
            Guid expected = Guid.NewGuid();
            Guid result = OrganizationId.Convert(expected.ToString("D"));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConvertValidStringValueInInvalidFormat()
        {
            Guid expected = Guid.NewGuid();
            OrganizationId.Convert(expected.ToString("N"));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConvertNullString()
        {
            OrganizationId.Convert(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConvertEmptyString()
        {
            OrganizationId.Convert(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ConvertWhitespaceString()
        {
            OrganizationId.Convert(" ");
        }

        [TestMethod]
        public void TryConvertValidGuidValue()
        {
            Guid id = Guid.NewGuid();
            string expected = id.ToString("D");
            string result;
            bool isValid = OrganizationId.TryConvert(id, out result);

            Assert.IsTrue(isValid);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TryConvertEmptyGuidValue()
        {
            string result;
            bool isValid = OrganizationId.TryConvert(Guid.Empty, out result);

            Assert.IsFalse(isValid);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertValidGuidValue()
        {
            Guid id = Guid.NewGuid();
            string expected = id.ToString("D");
            string result = OrganizationId.Convert(id);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertEmptyGuidValue()
        {
            OrganizationId.Convert(Guid.Empty);
        }
    }
}