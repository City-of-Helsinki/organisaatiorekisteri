namespace OrganizationRegister.Application.Validation
{
    internal class BusinessIdentifierValidationResult : IBusinessIdentifierValidationResult
    {
        public bool IsValid { get; private set; }
        public string ReasonForInvalidity { get; private set; }

        public BusinessIdentifierValidationResult(bool isValid, string reasonForInvalidity)
        {
            IsValid = isValid;
            ReasonForInvalidity = reasonForInvalidity;
        }
    }
}
