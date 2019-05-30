using System;
using Affecto.Identifiers;
using Affecto.Identifiers.Finnish;
using OrganizationRegister.Application.Organization;

namespace OrganizationRegister.Application.Validation
{
    internal class ValidationService : IValidationService
    {
        private readonly IOrganizationRepository organizationRepository;

        public ValidationService(IOrganizationRepository organizationRepository)
        {
            if (organizationRepository == null)
            {
                throw new ArgumentNullException("organizationRepository");
            }
            this.organizationRepository = organizationRepository;
        }

        public IBusinessIdentifierValidationResult ValidateUniqueBusinessIdentifier(string businessId, Guid? organizationId)
        {
            IBusinessIdentifierValidationResult result = ValidateBusinessIdentifier(businessId);
            if (result.IsValid && organizationRepository.HasActiveOrganization(businessId, organizationId))
            {
                return new BusinessIdentifierValidationResult(false, InvalidBusinessIdentifierReason.AlreadyExists);
            }
            return result;
        }

        public IBusinessIdentifierValidationResult ValidateBusinessIdentifier(string businessId)
        {
            var specification = new BusinessIdentifierSpecification();
            bool isValid = specification.IsSatisfiedBy(businessId);
            string reasonForInvalidity = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return new BusinessIdentifierValidationResult(isValid, reasonForInvalidity);
        }

        public bool ValidatePhoneNumber(string phoneNumber)
        {
            var specification = new PhoneNumberSpecification();
            return specification.IsSatisfiedBy(phoneNumber);
        }

        public bool ValidateEmailAddress(string emailAddress)
        {
            var specification = new EmailAddressSpecification();
            return specification.IsSatisfiedBy(emailAddress);
        }

        public bool ValidateWebAddress(string webAddress)
        {
            var specification = new WebAddressSpecification();
            return specification.IsSatisfiedBy(webAddress);
        }

        public bool ValidatePostalCode(string postalCode)
        {
            var specification = new PostalCodeSpecification();
            return specification.IsSatisfiedBy(postalCode);
        }

        public bool ValidatePostOfficeBoxPostalCode(string postalCode)
        {
            //var specification = new PostOfficeBoxPostalCodeSpecification();

            // use postal code validation specification with post office box postal code validation
            var specification = new PostalCodeSpecification();
            return specification.IsSatisfiedBy(postalCode);
        }
    }
}
