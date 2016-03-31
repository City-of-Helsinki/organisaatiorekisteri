using System.Web.Http.Results;
using Affecto.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Validation;
using OrganizationRegister.Application.Validation;

namespace OrganizationRegister.Api.Tests.Validation
{
    [TestClass]
    public class AuthorizedValidationControllerTests
    {
        private AuthorizedValidationController sut;
        private IValidationService validationService;
        private MapperFactory mapperFactory;
        private IMapper<IBusinessIdentifierValidationResult, BusinessIdentifierValidationResult> businessIdentifierValidationResultMapper;

        [TestInitialize]
        public void Setup()
        {
            SetupMappers();
            validationService = Substitute.For<IValidationService>();
            sut = new AuthorizedValidationController(validationService, mapperFactory);
        }

        private void SetupMappers()
        {
            mapperFactory = Substitute.For<MapperFactory>();
            businessIdentifierValidationResultMapper = Substitute.For<IMapper<IBusinessIdentifierValidationResult, BusinessIdentifierValidationResult>>();
            mapperFactory.CreateBusinessIdentifierValidationResultMapper().Returns(businessIdentifierValidationResultMapper);
        }

        [TestMethod]
        public void ValidateUniqueBusinessIdentifier()
        {
            const string businessId = "1234567-8";

            BusinessIdentifierValidationResult returnValue = new BusinessIdentifierValidationResult();
            IBusinessIdentifierValidationResult appReturnValue = Substitute.For<IBusinessIdentifierValidationResult>();
            businessIdentifierValidationResultMapper.Map(appReturnValue).Returns(returnValue);
            validationService.ValidateUniqueBusinessIdentifier(businessId, null).Returns(appReturnValue);
            var request = new BusinessIdentifierValidationRequest { BusinessId = businessId, AllowDuplicates = false };

            var result = sut.ValidateBusinessIdentifier(request) as OkNegotiatedContentResult<BusinessIdentifierValidationResult>;

            Assert.IsNotNull(result);
            Assert.AreSame(returnValue, result.Content);
        }

        [TestMethod]
        public void ValidateNonUniqueBusinessIdentifier()
        {
            const string businessId = "1234567-8";

            BusinessIdentifierValidationResult returnValue = new BusinessIdentifierValidationResult();
            IBusinessIdentifierValidationResult appReturnValue = Substitute.For<IBusinessIdentifierValidationResult>();
            businessIdentifierValidationResultMapper.Map(appReturnValue).Returns(returnValue);
            validationService.ValidateBusinessIdentifier(businessId).Returns(appReturnValue);
            var request = new BusinessIdentifierValidationRequest { BusinessId = businessId, AllowDuplicates = true };

            var result = sut.ValidateBusinessIdentifier(request) as OkNegotiatedContentResult<BusinessIdentifierValidationResult>;

            Assert.IsNotNull(result);
            Assert.AreSame(returnValue, result.Content);
        }

        [TestMethod]
        public void ValidatePhoneNumber()
        {
            const string phoneNumber = "0100100";
            validationService.ValidatePhoneNumber(phoneNumber).Returns(true);

            var result = sut.ValidatePhoneNumber(phoneNumber) as OkNegotiatedContentResult<bool>;

            AssertTrueContent(result);
        }

        [TestMethod]
        public void ValidateEmailAddress()
        {
            const string emailAddress = "me@gmail.com";
            validationService.ValidateEmailAddress(emailAddress).Returns(true);

            var result = sut.ValidateEmailAddress(emailAddress) as OkNegotiatedContentResult<bool>;

            AssertTrueContent(result);
        }

        [TestMethod]
        public void ValidateWebAddress()
        {
            const string webAddress = "www.gmail.com";
            validationService.ValidateWebAddress(webAddress).Returns(true);

            var result = sut.ValidateWebAddress(webAddress) as OkNegotiatedContentResult<bool>;

            AssertTrueContent(result);
        }

        [TestMethod]
        public void ValidatePostalCode()
        {
            const string postalCode = "20780";
            validationService.ValidatePostalCode(postalCode).Returns(true);

            var result = sut.ValidatePostalCode(postalCode) as OkNegotiatedContentResult<bool>;

            AssertTrueContent(result);
        }

        [TestMethod]
        public void ValidatePostOfficeBoxPostalCode()
        {
            const string postalCode = "20781";
            validationService.ValidatePostOfficeBoxPostalCode(postalCode).Returns(true);

            var result = sut.ValidatePostOfficeBoxPostalCode(postalCode) as OkNegotiatedContentResult<bool>;

            AssertTrueContent(result);
        }

        private static void AssertTrueContent(OkNegotiatedContentResult<bool> result)
        {
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content);
        }
    }
}
