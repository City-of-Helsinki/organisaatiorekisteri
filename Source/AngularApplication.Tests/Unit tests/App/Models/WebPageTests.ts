"use strict";

describe("web page", () =>
{
    var sut: OrganizationRegister.WebPage;

    describe("Address", () =>
    {
        var inputAddress: string;

        it("is the same as given address when address already contains http protocol", () =>
        {
            inputAddress = "http://www.google.fi";
            sut = new OrganizationRegister.WebPage("Google", inputAddress, "type");
            expect(sut.address).toEqual(inputAddress);
        });
        it("is the same as given address when address already contains secured http protocol", () =>
        {
            inputAddress = "https://www.gmail.com";
            sut = new OrganizationRegister.WebPage("Gmail", inputAddress, "type");
            expect(sut.address).toEqual(inputAddress);
        });
        it("protocol is prefixed when it is not included in the given address", () =>
        {
            sut = new OrganizationRegister.WebPage("Google", "www.google.com", "type");
            expect(sut.address).toEqual("http://www.google.com");
        });
    });
});
 