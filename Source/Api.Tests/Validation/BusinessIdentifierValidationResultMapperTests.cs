using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Validation;
using OrganizationRegister.Application.Validation;

namespace OrganizationRegister.Api.Tests.Validation
{
    [TestClass]
    public class BusinessIdentifierValidationResultMapperTests
    {
        private BusinessIdentifierValidationResultMapper sut;
        private IBusinessIdentifierValidationResult source;
        private BusinessIdentifierValidationResult destination;

        [TestInitialize]
        public void Setup()
        {
            source = Substitute.For<IBusinessIdentifierValidationResult>();
            sut = new BusinessIdentifierValidationResultMapper();
        }

        [TestMethod]
        public void IsValidIsMapped()
        {
            source.IsValid.Returns(true);

            destination = sut.Map(source);

            Assert.IsTrue(destination.IsValid);
        }

        [TestMethod]
        public void ReasonForInvalidityIsMapped()
        {
            const string reason = "reason";
            source.ReasonForInvalidity.Returns(reason);

            destination = sut.Map(source);

            Assert.AreEqual(reason, destination.ReasonForInvalidity);
        }
    }
}
