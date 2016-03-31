using System;

namespace OrganizationRegister.Application.Validation
{
    public interface IValidationService
    {
        IBusinessIdentifierValidationResult ValidateUniqueBusinessIdentifier(string businessId, Guid? organizationId);
        IBusinessIdentifierValidationResult ValidateBusinessIdentifier(string businessId);
        bool ValidatePhoneNumber(string phoneNumber);
        bool ValidateEmailAddress(string emailAddress);
        bool ValidateWebAddress(string webAddress);
        bool ValidatePostalCode(string postalCode);
        bool ValidatePostOfficeBoxPostalCode(string postalCode);
    }
}
