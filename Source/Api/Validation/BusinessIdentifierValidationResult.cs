namespace OrganizationRegister.Api.Validation
{
    public class BusinessIdentifierValidationResult
    {
        public bool IsValid { get; set; }
        public string ReasonForInvalidity { get; set; }
    }
}