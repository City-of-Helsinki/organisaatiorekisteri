namespace OrganizationRegister.Application.Location
{
    public interface IVisitingAddress : IVisitingAddressQualifiers
    {
        StreetAddress VisitingAddress { get; }
    }
}
