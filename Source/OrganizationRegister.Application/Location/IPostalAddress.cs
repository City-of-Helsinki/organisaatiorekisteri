namespace OrganizationRegister.Application.Location
{
    public interface IPostalAddress
    {
        StreetAddress PostalStreetAddress { get; }
        PostOfficeBoxAddress PostalPostOfficeBoxAddress { get; }
        bool UseVisitingAddressAsPostalAddress { get; }
    }
}
