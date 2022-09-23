using System;
using System.Web.Http;
using Affecto.WebApi.Toolkit.CustomRoutes;
using OrganizationRegister.Application.Validation;

namespace OrganizationRegister.Api.Validation
{
    [RoutePrefix("v1/organizationregister")]
    public class AuthorizedValidationController : ApiController
    {
        private readonly IValidationService validationService;
        private readonly MapperFactory mapperFactory;

        public AuthorizedValidationController(IValidationService validationService, MapperFactory mapperFactory)
        {
            if (validationService == null)
            {
                throw new ArgumentNullException("validationService");
            }
            if (mapperFactory == null)
            {
                throw new ArgumentNullException("mapperFactory");
            }
            this.validationService = validationService;
            this.mapperFactory = mapperFactory;
        }

        [HttpPost]
        [PostRoute("ptvid")]
        public IHttpActionResult ValidatePtvIdIdentifier(BusinessIdentifierValidationRequest request)
        {
            IBusinessIdentifierValidationResult result;

            result = validationService.ValidateUniquePtvId(request.BusinessId, request.OrganizationId);

            var mapper = mapperFactory.CreateBusinessIdentifierValidationResultMapper();
            BusinessIdentifierValidationResult mappedResult = mapper.Map(result);
            return Ok(mappedResult);
        }


        [HttpPost]
        [PostRoute("businessid")]
        public IHttpActionResult ValidateBusinessIdentifier(BusinessIdentifierValidationRequest request)
        {
            IBusinessIdentifierValidationResult result;
            if (request.AllowDuplicates)
            {
                result = validationService.ValidateBusinessIdentifier(request.BusinessId);
            }
            else
            {
                result = validationService.ValidateUniqueBusinessIdentifier(request.BusinessId, request.OrganizationId);                
            }
            var mapper = mapperFactory.CreateBusinessIdentifierValidationResultMapper();
            BusinessIdentifierValidationResult mappedResult = mapper.Map(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [PostRoute("phonenumber")]
        public IHttpActionResult ValidatePhoneNumber([FromBody] string phoneNumber)
        {
            bool result = validationService.ValidatePhoneNumber(phoneNumber);
            return Ok(result);
        }

        [HttpPost]
        [PostRoute("emailaddress")]
        public IHttpActionResult ValidateEmailAddress([FromBody] string emailAddress)
        {
            bool result = validationService.ValidateEmailAddress(emailAddress);
            return Ok(result);
        }

        [HttpPost]
        [PostRoute("webaddress")]
        public IHttpActionResult ValidateWebAddress([FromBody] string webAddress)
        {
            bool result = validationService.ValidateWebAddress(webAddress);
            return Ok(result);
        }

        [HttpPost]
        [PostRoute("postalcode")]
        public IHttpActionResult ValidatePostalCode([FromBody] string postalCode)
        {
            bool result = validationService.ValidatePostalCode(postalCode);
            return Ok(result);
        }

        [HttpPost]
        [PostRoute("postofficeboxpostalcode")]
        public IHttpActionResult ValidatePostOfficeBoxPostalCode([FromBody] string postalCode)
        {
            bool result = validationService.ValidatePostOfficeBoxPostalCode(postalCode);
            return Ok(result);
        }
    }
}