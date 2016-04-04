"use strict";

describe("Available postal address types",() =>
{
    var sut: OrganizationRegister.AvailablePostalAddressTypes;

    describe("Setting available",() =>
    {
        it("setting an address type available adds it to the type collection",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            sut.setAvailable(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress);
            expect(sut.types.length).toBe(1);
            expect(sut.types[0]).toEqual(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.PostOfficeBoxAddress]);
        });
        it("setting an address type available again does nothing",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            sut.setAvailable(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress);
            sut.setAvailable(OrganizationRegister.PostalAddressType.SeparateStreetAddress);
            sut.setAvailable(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress);
            expect(sut.types.length).toBe(2);
        });
        it("available address types are custom ordered",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            sut.setAvailable(OrganizationRegister.PostalAddressType.SameAsVisitingAddress);
            sut.setAvailable(OrganizationRegister.PostalAddressType.SeparateStreetAddress);
            sut.setAvailable(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress);
            expect(sut.types.length).toBe(3);
            expect(sut.types[0]).toEqual(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SameAsVisitingAddress]);
            expect(sut.types[1]).toEqual(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SeparateStreetAddress]);
            expect(sut.types[2]).toEqual(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.PostOfficeBoxAddress]);
        });
        it("available address type ordering when some address types are missing",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            sut.setAvailable(OrganizationRegister.PostalAddressType.SeparateStreetAddress);
            sut.setAvailable(OrganizationRegister.PostalAddressType.SameAsVisitingAddress);
            expect(sut.types.length).toBe(2);
            expect(sut.types[0]).toEqual(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SameAsVisitingAddress]);
            expect(sut.types[1]).toEqual(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SeparateStreetAddress]);
        });
    });

    describe("Setting unavailable",() =>
    {
        it("setting an address type unavailable removes it from the type collection",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            sut.setAvailable(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress);
            sut.setUnavailable(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress);
            expect(sut.types.length).toBe(0);
        });
        it("setting an address type that is not available unavailable does nothing",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            sut.setUnavailable(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress);
            expect(sut.types.length).toBe(0);
        });
    });

    describe("Checking available types",() =>
    {
        it("no any available type when nothing is set available",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            expect(sut.any()).toBeFalsy();
        });
        it("available types exist when something is set available",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            sut.setAvailable(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress);
            expect(sut.any()).toBeTruthy();
        });
    });

    describe("Getting first available type",() =>
    {
        it("no types available",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            expect(sut.firstOrDefault()).toBeNull();
        });
        it("types are available",() =>
        {
            sut = new OrganizationRegister.AvailablePostalAddressTypes();
            sut.setAvailable(OrganizationRegister.PostalAddressType.SameAsVisitingAddress);
            sut.setAvailable(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress);
            expect(sut.firstOrDefault()).toEqual(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SameAsVisitingAddress]);
        });
    });
});
 