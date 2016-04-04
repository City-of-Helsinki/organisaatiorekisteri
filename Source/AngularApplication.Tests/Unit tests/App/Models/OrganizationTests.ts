"use strict";

describe("Organization", () =>
{
    var sut: OrganizationRegister.Organization;
    sut = new OrganizationRegister.Organization();

    describe("municipality", () =>
    {
        it("is municipality when type is municipality", () =>
        {
            sut.type = "Kunta";
            expect(sut.isMunicipality()).toBeTruthy();
        });
        it("is not municipality when type is not municipality", () =>
        {
            sut.type = "Yritys";
            expect(sut.isMunicipality()).toBeFalsy();
        });
    });

    describe("type", () =>
    {
        it("setting something else than a municipality type clears municipality code", () =>
        {
            sut.municipalityCode = 12;
            sut.typeProperty = "Yritys";
            expect(sut.municipalityCode).toBeNull();
        });
    });

    describe("municipality code", () =>
    {
        it("has no municipality code when municipality code is null", () =>
        {
            sut.municipalityCode = null;
            expect(sut.hasMunicipalityCode()).toBeFalsy();
        });
        it("has municipality code when municipality code is defined", () =>
        {
            sut.municipalityCode = 112;
            expect(sut.hasMunicipalityCode()).toBeTruthy();
        });
    });

    describe("business id", () =>
    {
        it("has no business id when business id is null", () =>
        {
            expect(sut.hasBusinessId()).toBeFalsy();
        });
        it("has no business id when business id is empty", () =>
        {
            sut.businessId = "";
            expect(sut.hasBusinessId()).toBeFalsy();
        });
        it("has business id when business id is defined", () =>
        {
            sut.businessId = "1324567-1";
            expect(sut.hasBusinessId()).toBeTruthy();
        });
    });

    describe("phone number", () =>
    {
        it("has no phone number when phone number is null", () =>
        {
            expect(sut.hasPhoneNumber()).toBeFalsy();
        });
        it("has no phone number when phone number is empty", () =>
        {
            sut.phoneNumber = "";
            expect(sut.hasPhoneNumber()).toBeFalsy();
        });
        it("has phone number when phone number is defined", () =>
        {
            sut.phoneNumber = "112";
            expect(sut.hasPhoneNumber()).toBeTruthy();
        });
    });

    describe("phone call fee", () =>
    {
        it("has no phone call fee when information is null", () =>
        {
            expect(sut.hasPhoneCallFee()).toBeFalsy();
        });
        it("has no phone call fee when information is empty", () =>
        {
            sut.phoneCallFee = "";
            expect(sut.hasPhoneCallFee()).toBeFalsy();
        });
        it("has phone call fee when information is defined", () =>
        {
            sut.phoneCallFee = "ppm";
            expect(sut.hasPhoneCallFee()).toBeTruthy();
        });
    });

    describe("contact information", () =>
    {
        it("has no contact information when no phone number, call fee, email address or web pages are set", () =>
        {
            sut.phoneNumber = null;
            sut.phoneCallFee = null;
            sut.emailAddress = null;
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeFalsy();
        });
        it("has no contact information when phone number, call fee, email address or web pages are empty", () =>
        {
            sut.phoneNumber = "";
            sut.phoneCallFee = "";
            sut.emailAddress = "";
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeFalsy();
        });
        it("has contact information when phone number is set", () =>
        {
            sut.phoneNumber = "112";
            sut.phoneCallFee = null;
            sut.emailAddress = null;
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeTruthy();
        });
        it("has contact information when phone call cost is set", () =>
        {
            sut.phoneCallFee = "ppm";
            sut.phoneNumber = null;
            sut.emailAddress = null;
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeTruthy();
        });
        it("has contact information when email address is set", () =>
        {
            sut.emailAddress = "me@here.fi";
            sut.phoneCallFee = null;
            sut.phoneNumber = null;
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeTruthy();
        });
        it("has contact information when there are web pages", () =>
        {
            sut.webPages.push(new OrganizationRegister.WebPage("home", "www.home.fi", "type"));
            sut.phoneCallFee = null;
            sut.emailAddress = null;
            sut.phoneNumber = null;
            expect(sut.hasContactInformation()).toBeTruthy();
        });
    });

    describe("visiting address postal code", () =>
    {
        it("has no visiting address postal code when postal code is null", () =>
        {
            expect(sut.hasVisitingAddressPostalCode()).toBeFalsy();
        });
        it("has no visiting address postal code when postal code is empty", () =>
        {
            sut.visitingAddressPostalCode = "";
            expect(sut.hasVisitingAddressPostalCode()).toBeFalsy();
        });
        it("has visiting address postal code when postal code is defined", () =>
        {
            sut.visitingAddressPostalCode = "20540";
            expect(sut.hasVisitingAddressPostalCode()).toBeTruthy();
        });
    });

    describe("effective postal street address street", () =>
    {
        it("visiting street address is effective postal street address when visiting address is used as the postal address", () =>
        {
            var street: string = "Street 1 A";
            sut.useVisitingAddressAsPostalAddress = true;
            sut.visitingStreetAddress = street;
            expect(sut.effectivePostalStreetAddressStreet).toEqual(street);
        });
        it("separate postal street address is effective postal street address when visiting address is not used as the postal address", () =>
        {
            var visitingStreetAddress: string = "Street 1 A";
            var postalStreetAddress: string = "Street 2 B";
            sut.useVisitingAddressAsPostalAddress = false;
            sut.visitingStreetAddress = visitingStreetAddress;
            sut.postalStreetAddressStreet = postalStreetAddress;
            expect(sut.effectivePostalStreetAddressStreet).toEqual(postalStreetAddress);
        });
    });

    describe("effective postal street address postal code", () =>
    {
        it("visiting street address postal code is effective postal street address postal code when visiting address is used as the postal address", () =>
        {
            var postalCode: string = "12345";
            sut.useVisitingAddressAsPostalAddress = true;
            sut.visitingAddressPostalCode = postalCode;
            expect(sut.effectivePostalStreetAddressPostalCode).toEqual(postalCode);
        });
        it("separate postal street address postal code is effective postal street address postal code when visiting address is not used as the postal address", () =>
        {
            var visitingStreetAddressPostalCode: string = "12345";
            var postalStreetAddressPostalCode: string = "54321";
            sut.useVisitingAddressAsPostalAddress = false;
            sut.visitingAddressPostalCode = visitingStreetAddressPostalCode;
            sut.postalStreetAddressPostalCode = postalStreetAddressPostalCode;
            expect(sut.effectivePostalStreetAddressPostalCode).toEqual(postalStreetAddressPostalCode);
        });
    });

    describe("effective postal street address postal district", () =>
    {
        it("visiting street address postal district is effective postal street address postal district when visiting address is used as the postal address", () =>
        {
            var postalDistrict: string = "Town";
            sut.useVisitingAddressAsPostalAddress = true;
            sut.visitingAddressPostalDistrict = postalDistrict;
            expect(sut.effectivePostalStreetAddressPostalDistrict).toEqual(postalDistrict);
        });
        it("separate postal street address postal district is effective postal street address postal district when visiting address is not used as the postal address", () =>
        {
            var visitingAddressPostalDistrict: string = "Town";
            var postalAddressPostalDistrict: string = "City";
            sut.useVisitingAddressAsPostalAddress = false;
            sut.visitingAddressPostalDistrict = visitingAddressPostalDistrict;
            sut.postalStreetAddressPostalDistrict = postalAddressPostalDistrict;
            expect(sut.effectivePostalStreetAddressPostalDistrict).toEqual(postalAddressPostalDistrict);
        });
    });

    describe("postal street address postal code", () =>
    {
        it("has no postal street address postal code when postal code is null", () =>
        {
            sut.postalStreetAddressPostalCode = null;
            expect(sut.hasPostalStreetAddressPostalCode()).toBeFalsy();
        });
        it("has no postal street address postal code when postal code is empty", () =>
        {
            sut.postalStreetAddressPostalCode = "";
            expect(sut.hasPostalStreetAddressPostalCode()).toBeFalsy();
        });
        it("has postal street address postal code when postal code is defined", () =>
        {
            sut.postalStreetAddressPostalCode = "20540";
            expect(sut.hasPostalStreetAddressPostalCode()).toBeTruthy();
        });
    });

    describe("postal post office box address postal code", () =>
    {
        it("has no postal post office box address postal code when postal code is null", () =>
        {
            expect(sut.hasPostalPostOfficeBoxAddressPostalCode()).toBeFalsy();
        });
        it("has no postal post office box address postal code when postal code is empty", () =>
        {
            sut.postalPostOfficeBoxAddressPostalCode = "";
            expect(sut.hasPostalPostOfficeBoxAddressPostalCode()).toBeFalsy();
        });
        it("has postal post office box address postal code when postal code is defined", () =>
        {
            sut.postalPostOfficeBoxAddressPostalCode = "20540";
            expect(sut.hasPostalPostOfficeBoxAddressPostalCode()).toBeTruthy();
        });
    });

    describe("postal address types", () =>
    {
        it("post office box address type is available when no addresses are given", () =>
        {
            expect(sut.postalAddressTypes.available.contains(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.PostOfficeBoxAddress])).toBeTruthy();
        });
        it("separate street address type is available when no addresses are given", () =>
        {
            expect(sut.postalAddressTypes.available.contains(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SeparateStreetAddress])).toBeTruthy();
        });
        it("same as visiting address type is not available when no addresses are given", () =>
        {
            expect(sut.postalAddressTypes.available.contains(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SameAsVisitingAddress])).toBeFalsy();
        });
        it("adding an address type adds it to the type collection", () =>
        {
            sut.postalAddressTypes.add(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.PostOfficeBoxAddress]);
            expect(sut.postalAddressTypes.contains(OrganizationRegister.PostalAddressType.PostOfficeBoxAddress)).toBeTruthy();
        });
        it("adding a post office box address removes it from available address types", () =>
        {
            sut.postalAddressTypes.add(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.PostOfficeBoxAddress]);
            expect(sut.postalAddressTypes.available.contains(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.PostOfficeBoxAddress])).toBeFalsy();
        });
        it("adding a separate postal address removes street addresses from available address types", () =>
        {
            sut.postalAddressTypes.add(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SeparateStreetAddress]);
            expect(sut.postalAddressTypes.available.contains(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SeparateStreetAddress])).toBeFalsy();
            expect(sut.postalAddressTypes.available.contains(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SameAsVisitingAddress])).toBeFalsy();
        });
        it("adding a same as visiting postal address removes street addresses from available address types", () =>
        {
            sut.postalAddressTypes.add(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SameAsVisitingAddress]);
            expect(sut.postalAddressTypes.available.contains(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SeparateStreetAddress])).toBeFalsy();
            expect(sut.postalAddressTypes.available.contains(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SameAsVisitingAddress])).toBeFalsy();
        });
    });

    describe("email address", () =>
    {
        it("has no email address when email address is null", () =>
        {
            expect(sut.hasEmailAddress()).toBeFalsy();
        });
        it("has no email address when email address is empty", () =>
        {
            sut.emailAddress = "";
            expect(sut.hasEmailAddress()).toBeFalsy();
        });
        it("has email address when email address is defined", () =>
        {
            sut.emailAddress = "me@server.com";
            expect(sut.hasEmailAddress()).toBeTruthy();
        });
    });

    describe("web page", () =>
    {
        it("has no web page when web page name is null", () =>
        {
            sut.webPageUrl = "http://www.google.fi";
            sut.webPageName = null;
            sut.webPageType = "Kotisivu";
            expect(sut.hasWebPage()).toBeFalsy();
        });
        it("has no web page when web page name is empty", () =>
        {
            sut.webPageUrl = "http://www.google.fi";
            sut.webPageName = "";
            sut.webPageType = "Kotisivu";
            expect(sut.hasWebPage()).toBeFalsy();
        });
        it("has no web page when web page url is null", () =>
        {
            sut.webPageName = "Google";
            sut.webPageUrl = null;
            sut.webPageType = "Kotisivu";
            expect(sut.hasWebPage()).toBeFalsy();
        });
        it("has no web page when web page url is empty", () =>
        {
            sut.webPageName = "Google";
            sut.webPageUrl = "";
            sut.webPageType = "Kotisivu";
            expect(sut.hasWebPage()).toBeFalsy();
        });
        it("has no web page when both web page name and url are empty", () =>
        {
            sut.webPageName = "";
            sut.webPageUrl = "";
            expect(sut.hasWebPage()).toBeFalsy();
        });
        it("has no web page when both web page name and url are null", () =>
        {
            sut.webPageName = null;
            sut.webPageUrl = null;
            expect(sut.hasWebPage()).toBeFalsy();
        });
        it("has web page when both web page name and url are defined", () =>
        {
            sut.webPageName = "Google";
            sut.webPageUrl = "http://www.google.fi";
            sut.webPageType = "Kotisivu";
            expect(sut.hasWebPage()).toBeTruthy();
        });

        it("has no web page url when web page url is null", () =>
        {
            sut.webPageName = "Google";
            sut.webPageUrl = null;
            sut.webPageType = "Kotisivu";
            expect(sut.hasWebPageUrl()).toBeFalsy();
        });
        it("has no web page url when web page url is empty", () =>
        {
            sut.webPageName = "Google";
            sut.webPageUrl = "";
            sut.webPageType = "Kotisivu";
            expect(sut.hasWebPageUrl()).toBeFalsy();
        });
        it("has no web page when web page type is null", () =>
        {
            sut.webPageName = "Google";
            sut.webPageUrl = "http://www.google.fi";
            sut.webPageType = null;
            expect(sut.hasWebPage()).toBeFalsy();
        });
        it("has no web page when web page type is empty", () =>
        {
            sut.webPageName = "Google";
            sut.webPageUrl = "http://www.google.fi";
            sut.webPageType = "";
            expect(sut.hasWebPage()).toBeFalsy();
        });
        it("has web page url when web page url, name and type are defined", () =>
        {
            sut.webPageName = "Google";
            sut.webPageUrl = "http://www.google.fi";
            sut.webPageType = "Kotisivu";
            expect(sut.hasWebPageUrl()).toBeTruthy();
        });
        it("has web page when web page url, name and type are defined", () =>
        {
            sut.webPageName = "Google";
            sut.webPageUrl = "http://www.google.fi";
            sut.webPageType = "Kotisivu";
            expect(sut.hasWebPage()).toBeTruthy();
        });

        it("has no edited web page when edited web page name is null", () =>
        {
            sut.editedWebPageUrl = "http://www.google.fi";
            sut.editedWebPageName = null;
            sut.editedWebPageType = "Kotisivu";
            expect(sut.hasEditedWebPage()).toBeFalsy();
        });
        it("has no edited web page when edited web page name is empty", () =>
        {
            sut.editedWebPageUrl = "http://www.google.fi";
            sut.editedWebPageName = "";
            sut.editedWebPageType = "Kotisivu";
            expect(sut.hasEditedWebPage()).toBeFalsy();
        });
        it("has no edited web page when edited web page url is null", () =>
        {
            sut.editedWebPageName = "Google";
            sut.editedWebPageUrl = null;
            sut.editedWebPageType = "Kotisivu";
            expect(sut.hasEditedWebPage()).toBeFalsy();
        });
        it("has no edited web page when edited web page url is empty", () =>
        {
            sut.editedWebPageName = "Google";
            sut.editedWebPageUrl = "";
            sut.editedWebPageType = "Kotisivu";
            expect(sut.hasEditedWebPage()).toBeFalsy();
        });
        it("has no edited web page when both edited web page name and url are empty", () =>
        {
            sut.editedWebPageName = "";
            sut.editedWebPageUrl = "";
            sut.editedWebPageType = "Kotisivu";
            expect(sut.hasEditedWebPage()).toBeFalsy();
        });
        it("has no edited web page when both edited web page name and url are null", () =>
        {
            sut.editedWebPageName = null;
            sut.editedWebPageUrl = null;
            sut.editedWebPageType = "Kotisivu";
            expect(sut.hasEditedWebPage()).toBeFalsy();
        });
        it("has edited web page when both edited web page name, url and type are defined", () =>
        {
            sut.editedWebPageName = "Google";
            sut.editedWebPageUrl = "http://www.google.fi";
            sut.editedWebPageType = "Kotisivu";
            expect(sut.hasEditedWebPage()).toBeTruthy();
        });
        it("has no edited web page when edited web page type is null", () =>
        {
            sut.editedWebPageName = "Google";
            sut.editedWebPageUrl = "http://www.google.fi";
            sut.editedWebPageType = null;
            expect(sut.hasEditedWebPage()).toBeFalsy();
        });
        it("has no edited web page when edited web page type is empty", () =>
        {
            sut.editedWebPageName = "Google";
            sut.editedWebPageUrl = "http://www.google.fi";
            sut.editedWebPageType = "";
            expect(sut.hasEditedWebPage()).toBeFalsy();
        });

        it("has no edited web page url when edited web page url is null", () =>
        {
            sut.editedWebPageName = "Google";
            sut.editedWebPageUrl = null;
            sut.editedWebPageType = "Kotisivu";
            expect(sut.hasEditedWebPageUrl()).toBeFalsy();
        });
        it("has no edited web url site when edited web page url is empty", () =>
        {
            sut.editedWebPageName = "Google";
            sut.editedWebPageUrl = "";
            sut.editedWebPageType = "Kotisivu";
            expect(sut.hasEditedWebPageUrl()).toBeFalsy();
        });
    });

    describe("adding a web page", () =>
    {
        it("web page is not added without a name", () =>
        {
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            sut.addWebPage("", "http://www.google.fi", "type");
            expect(sut.webPages.length).toBe(0);
        });
        it("web page is not added without a url", () =>
        {
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            sut.addWebPage("Google", null, "type");
            expect(sut.webPages.length).toBe(0);
        });
        it("web page is not added without a type", () =>
        {
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            sut.addWebPage("Google", "http://www.google.fi", null);
            expect(sut.webPages.length).toBe(0);
        });
        it("name, url and type are added", () =>
        {
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            sut.addWebPage("Google", "http://www.google.fi", "type");
            expect(sut.webPages.length).toBe(1);

            var addedSite: OrganizationRegister.WebPage = sut.webPages[0];
            expect(addedSite.name).toEqual("Google");
            expect(addedSite.address).toEqual("http://www.google.fi");
        });
        it("same url cannot be added twice", () =>
        {
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            sut.addWebPage("Google", "http://www.google.fi", "type");
            sut.addWebPage("Google", "http://www.google.fi", "type");
            expect(sut.webPages.length).toBe(1);
        });
    });

    describe("removing a web page", () =>
    {
        it("removing from an empty collection", () =>
        {
            sut.removeWebPage("http://www.google.fi");
            expect(sut.webPages.length).toBe(0);
        });
        it("removing a web page that doesn't exist", () =>
        {
            sut.addWebPage("Global google", "http://www.google.com", "type");

            sut.removeWebPage("http://www.google.fi");
            expect(sut.webPages.length).toBe(1);
        });
        it("removing a web page that exists", () =>
        {
            sut.addWebPage("Global google", "http://www.google.com", "type");
            sut.addWebPage("Local google", "http://www.google.fi", "type");

            sut.removeWebPage("http://www.google.fi");
            expect(sut.webPages.length).toBe(1);

            var remainingSite: OrganizationRegister.WebPage = sut.webPages[0];
            expect(remainingSite.name).toEqual("Global google");
            expect(remainingSite.address).toEqual("http://www.google.com");
        });
    });

    describe("visiting address parts", () =>
    {
        it("initially has no visiting address parts", () =>
        {
            sut.visitingStreetAddress = null;
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistrict = null;
            expect(sut.hasVisitingAddressParts()).toBeFalsy();
        });
        it("has visiting address parts when street address is set", () =>
        {
            sut.visitingStreetAddress = "katu 1";
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistrict = null;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when postal code is set", () =>
        {
            sut.visitingStreetAddress = null;
            sut.visitingAddressPostalCode = "20500";
            sut.visitingAddressPostalDistrict = null;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when postal district is set", () =>
        {
            sut.visitingStreetAddress = null;
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistrict = "Hesa";
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when street address and postal code are set", () =>
        {
            sut.visitingStreetAddress = "katu 1";
            sut.visitingAddressPostalCode = "12345";
            sut.visitingAddressPostalDistrict = null;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when street address and postal district are set", () =>
        {
            sut.visitingStreetAddress = "katu 1";
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistrict = "Hesa";
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when postal code and postal district are set", () =>
        {
            sut.visitingStreetAddress = null;
            sut.visitingAddressPostalCode = "12345";
            sut.visitingAddressPostalDistrict = "Hesa";
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when street address, postal code and postal district are set", () =>
        {
            sut.visitingStreetAddress = "katu 1";
            sut.visitingAddressPostalCode = "12345";
            sut.visitingAddressPostalDistrict = "Hesa";
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has no visiting address parts when street address is empty", () =>
        {
            sut.visitingStreetAddress = "";
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistrict = null;
            expect(sut.hasVisitingAddressParts()).toBeFalsy();
        });
        it("has no visiting address parts when postal code is empty", () =>
        {
            sut.visitingStreetAddress = null;
            sut.visitingAddressPostalCode = "";
            sut.visitingAddressPostalDistrict = null;
            expect(sut.hasVisitingAddressParts()).toBeFalsy();
        });
        it("has no visiting address parts when postal district is empty", () =>
        {
            sut.visitingStreetAddress = null;
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistrict = "";
            expect(sut.hasVisitingAddressParts()).toBeFalsy();
        });
    });
});
