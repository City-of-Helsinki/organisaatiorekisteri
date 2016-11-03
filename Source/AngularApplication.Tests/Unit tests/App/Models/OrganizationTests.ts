"use strict";

describe("Organization", () =>
{
    var sut: OrganizationRegister.Organization;

    beforeEach(() =>
    {
        sut = new OrganizationRegister.Organization();
    });

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

    describe("phone call charge type", () =>
    {
        it("has no phone call charge type when information is null", () =>
        {
            expect(sut.hasPhoneCallChargeType()).toBeFalsy();
        });
        it("has no phone call charge type when information is empty", () =>
        {
            sut.phoneCallChargeType = "";
            expect(sut.hasPhoneCallChargeType()).toBeFalsy();
        });
        it("has phone call charge type when information is defined", () =>
        {
            sut.phoneCallChargeType = "Muu";
            expect(sut.hasPhoneCallChargeType()).toBeTruthy();
        });
    });

    describe("contact information", () =>
    {
        it("has no contact information when no phone number, call fee, email address or web pages are set", () =>
        {
            sut.phoneNumber = null;
            sut.phoneCallChargeType = null;
            sut.emailAddress = null;
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeFalsy();
        });
        it("has no contact information when phone number, call fee, email address or web pages are empty", () =>
        {
            sut.phoneNumber = "";
            sut.phoneCallChargeType = "";
            sut.emailAddress = "";
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeFalsy();
        });
        it("has contact information when phone number is set", () =>
        {
            sut.phoneNumber = "112";
            sut.phoneCallChargeType = null;
            sut.emailAddress = null;
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeTruthy();
        });
        it("has contact information when phone call cost is set", () =>
        {
            sut.phoneCallChargeType = "Muu";
            sut.phoneNumber = null;
            sut.emailAddress = null;
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeTruthy();
        });
        it("has contact information when email address is set", () =>
        {
            sut.emailAddress = "me@here.fi";
            sut.phoneCallChargeType = null;
            sut.phoneNumber = null;
            sut.webPages = new Array<OrganizationRegister.WebPage>();
            expect(sut.hasContactInformation()).toBeTruthy();
        });
        it("has contact information when there are web pages", () =>
        {
            sut.webPages.push(new OrganizationRegister.WebPage("home", "www.home.fi", "type"));
            sut.phoneCallChargeType = null;
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
        
        let streets = new Array<OrganizationRegister.LocalizedText>(new OrganizationRegister.LocalizedText("fi", "Katu 1 A", false));
        
        it("visiting street address is effective postal street address when visiting address is used as the postal address", () =>
        {
            sut.useVisitingAddressAsPostalAddress = true;
            sut.visitingStreetAddresses = streets;
            expect(sut.effectivePostalStreetAddressStreets).toEqual(streets);
        });
        it("separate postal street address is effective postal street address when visiting address is not used as the postal address", () =>
        {
            let postalAddressStreets = new Array<OrganizationRegister.LocalizedText>(new OrganizationRegister.LocalizedText("fi", "Katu 2 B", false));

            sut.useVisitingAddressAsPostalAddress = false;
            sut.visitingStreetAddresses = streets;
            sut.postalStreetAddressStreets = postalAddressStreets;
            expect(sut.effectivePostalStreetAddressStreets).toEqual(postalAddressStreets);
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
        let districts = new Array<OrganizationRegister.LocalizedText>(new OrganizationRegister.LocalizedText("fi", "Helsinki", false));

        it("visiting street address postal district is effective postal street address postal district when visiting address is used as the postal address", () =>
        {
            
            sut.useVisitingAddressAsPostalAddress = true;
            sut.visitingAddressPostalDistricts = districts;
            expect(sut.effectivePostalStreetAddressPostalDistricts).toEqual(districts);
        });
        it("separate postal street address postal district is effective postal street address postal district when visiting address is not used as the postal address", () =>
        {

            let postalAddressDistricts = new Array<OrganizationRegister.LocalizedText>(new OrganizationRegister.LocalizedText("fi", "Helsinki", false));

            var visitingAddressPostalDistrict: string = "Town";
            var postalAddressPostalDistrict: string = "City";
            sut.useVisitingAddressAsPostalAddress = false;
            sut.visitingAddressPostalDistricts = districts;
            sut.postalStreetAddressPostalDistricts = postalAddressDistricts;
            expect(sut.effectivePostalStreetAddressPostalDistricts).toEqual(postalAddressDistricts);
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
        it("postal address can be added when there are no postal addresses", () =>
        {
            expect(sut.canAddPostalAddress()).toBeTruthy();
        });
        it("only one postal address can be added", () =>
        {
            sut.postalAddressTypes.add(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SeparateStreetAddress]);
            expect(sut.canAddPostalAddress()).toBeFalsy();
        });
        it("trying to add a second postal address", () =>
        {
            sut.postalAddressTypes.add(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.SeparateStreetAddress]);
            expect(() => sut.postalAddressTypes.add(OrganizationRegister.PostalAddressType[OrganizationRegister.PostalAddressType.PostOfficeBoxAddress])).toThrow();
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
        let streets = new Array<OrganizationRegister.LocalizedText>(new OrganizationRegister.LocalizedText("fi", "Katu 1 A", false));
        let districts = new Array<OrganizationRegister.LocalizedText>(new OrganizationRegister.LocalizedText("fi", "Helsinki", false));

        it("initially has no visiting address parts", () =>
        {
            sut.visitingStreetAddresses = null;
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistricts = null;
            expect(sut.hasVisitingAddressParts()).toBeFalsy();
        });
        it("has visiting address parts when street address is set", () =>
        {
            
            sut.visitingStreetAddresses = streets;
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistricts = null;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when postal code is set", () =>
        {
          
            sut.visitingStreetAddresses = null;
            sut.visitingAddressPostalCode = "20500";
            sut.visitingAddressPostalDistricts = null;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when postal district is set", () =>
        {
           
            sut.visitingStreetAddresses = null;
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistricts = districts;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when street address and postal code are set", () =>
        {
            sut.visitingStreetAddresses = streets;
            sut.visitingAddressPostalCode = "12345";
            sut.visitingAddressPostalDistricts = null;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when street address and postal district are set", () =>
        {
            sut.visitingStreetAddresses = streets;
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistricts = districts;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when postal code and postal district are set", () =>
        {
            sut.visitingStreetAddresses = null;
            sut.visitingAddressPostalCode = "12345";
            sut.visitingAddressPostalDistricts = districts;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has visiting address parts when street address, postal code and postal district are set", () =>
        {
            sut.visitingStreetAddresses = streets;
            sut.visitingAddressPostalCode = "12345";
            sut.visitingAddressPostalDistricts = districts;
            expect(sut.hasVisitingAddressParts()).toBeTruthy();
        });
        it("has no visiting address parts when street address is empty", () =>
        {
            let emptyStreets = new Array<OrganizationRegister.LocalizedText>(new OrganizationRegister.LocalizedText("fi", "", false));
            sut.visitingStreetAddresses = emptyStreets;
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistricts = null;
            expect(sut.hasVisitingAddressParts()).toBeFalsy();
        });
        it("has no visiting address parts when postal code is empty", () =>
        {
            sut.visitingStreetAddresses = null;
            sut.visitingAddressPostalCode = "";
            sut.visitingAddressPostalDistricts = null;
            expect(sut.hasVisitingAddressParts()).toBeFalsy();
        });
        it("has no visiting address parts when postal district is empty", () =>
        {
            let emptyDistricts = new Array<OrganizationRegister.LocalizedText>(new OrganizationRegister.LocalizedText("fi", "", false));
            sut.visitingStreetAddresses = null;
            sut.visitingAddressPostalCode = null;
            sut.visitingAddressPostalDistricts = emptyDistricts;
            expect(sut.hasVisitingAddressParts()).toBeFalsy();
        });
    });

    describe("validity validation", () =>
    {
        it("is not valid when from date is greater than to date", () =>
        {
            sut.validFromDate = new Date("2016-01-01");
            sut.validToDate = new Date("2015-01-01");
            expect(sut.isValidValidity()).toBeFalsy();
        });

        it("is valid when from date is equal to to date", () =>
        {
            sut.validFromDate = new Date("2016-01-01");
            sut.validToDate = new Date("2016-01-01");
            expect(sut.isValidValidity()).toBeTruthy();
        });

        it("is valid when from date and to date are empty", () =>
        {
            sut.validFromDate = null;
            sut.validToDate = null;
            expect(sut.isValidValidity()).toBeTruthy();
        });

        it("is valid when only from date is set", () =>
        {
            sut.validFromDate = new Date("2016-01-01");
            sut.validToDate = null;
            expect(sut.isValidValidity()).toBeTruthy();
        });

        it("is valid when only to date is set", () =>
        {
            sut.validFromDate = null;
            sut.validToDate = new Date("2016-01-01");
            expect(sut.isValidValidity()).toBeTruthy();
        });

    });

    describe("localized data in basic information", () =>
    {
        let langs = OrganizationRegister.DataLocalization.languageCodes;
        let existingValue = new OrganizationRegister.LocalizedText("fi", "localized value", true);
        let existingValues = new Array<OrganizationRegister.LocalizedText>(existingValue);
        let sut = new OrganizationRegister.Organization(null, null, existingValues, null, existingValues, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, existingValues);

        it("should initially exists for each data to be localized", () =>
        {
            expect(sut.names.length).toEqual(langs.length);
            expect(sut.nameAbbreviations.length).toEqual(langs.length);
            expect(sut.descriptionsAsHtml.length).toEqual(langs.length); 
            expect(sut.phoneCallChargeInfos.length).toEqual(langs.length); 
        });

        it("should have localized values for existing localized data", () =>
        {
            expect(sut.names
                    .some((arrVal: OrganizationRegister.LocalizedText) => (
                        existingValue.localizedValue === arrVal.localizedValue &&
                        existingValue.languageCode === arrVal.languageCode )))
                .toBeTruthy();

            expect(sut.nameAbbreviations
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    existingValue.localizedValue === arrVal.localizedValue &&
                    existingValue.languageCode === arrVal.languageCode)))
                .toBeTruthy();

            expect(sut.descriptionsAsHtml
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    existingValue.localizedValue === arrVal.localizedValue &&
                    existingValue.languageCode === arrVal.languageCode)))
                .toBeTruthy();
        });

        it("should not have localized values for non existing localized data",
        () =>
        {
            expect(sut.names
                    .some((arrVal: OrganizationRegister.LocalizedText) => (
                       arrVal.localizedValue !== "" &&
                    arrVal.languageCode !== existingValue.languageCode)))
                .toBeFalsy();

            expect(sut.nameAbbreviations
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    arrVal.localizedValue !== "" &&
                    arrVal.languageCode !== existingValue.languageCode)))
                .toBeFalsy();

            expect(sut.descriptionsAsHtml
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    arrVal.localizedValue !== "" &&
                    arrVal.languageCode !== existingValue.languageCode)))
                .toBeFalsy();
        });

        it("should exists only for data with localized values set", () =>
        {
            sut.generateBasicInformationLocalizedAndFormattedTexts();

            expect(sut.names.length).toEqual(existingValues.length);
            expect(sut.names[0].localizedValue).toEqual(existingValue.localizedValue);
            expect(sut.names[0].languageCode).toEqual(existingValue.languageCode);

            expect(sut.nameAbbreviations.length).toEqual(existingValues.length);
            expect(sut.nameAbbreviations[0].localizedValue).toEqual(existingValue.localizedValue);
            expect(sut.nameAbbreviations[0].languageCode).toEqual(existingValue.languageCode);

            expect(sut.descriptionsAsHtml.length).toEqual(existingValues.length);
            expect(sut.descriptionsAsHtml[0].localizedValue).toEqual(existingValue.localizedValue);
            expect(sut.descriptionsAsHtml[0].languageCode).toEqual(existingValue.languageCode);

        });

    });

    describe("localized data in contact information", () =>
    {
        let langs = OrganizationRegister.DataLocalization.languageCodes;
        let existingValue = new OrganizationRegister.LocalizedText("fi", "localized value", true);
        let existingValues = new Array<OrganizationRegister.LocalizedText>(existingValue);
        let sut = new OrganizationRegister.Organization(null, null, null,null, null, null,null,null,null,null,null,null,existingValues);

        it("should initially exists for each data to be localized", () =>
        {
           
            expect(sut.phoneCallChargeInfos.length).toEqual(langs.length);
        });

        it("should have localized values for existing localized data", () =>
        {
           
            expect(sut.phoneCallChargeInfos
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    existingValue.localizedValue === arrVal.localizedValue &&
                    existingValue.languageCode === arrVal.languageCode)))
                .toBeTruthy();
        });

        it("should not have localized values for non existing localized data",
            () =>
            {
              
                expect(sut.phoneCallChargeInfos
                    .some((arrVal: OrganizationRegister.LocalizedText) => (
                        existingValue.localizedValue === arrVal.localizedValue &&
                        existingValue.languageCode === arrVal.languageCode)))
                    .toBeTruthy();
            });

        it("should exists only for data with localized values set", () =>
        {
            sut.generateContactinformationLocalizedTexts();

            expect(sut.phoneCallChargeInfos.length).toEqual(existingValues.length);
            expect(sut.phoneCallChargeInfos[0].localizedValue).toEqual(existingValue.localizedValue);
            expect(sut.phoneCallChargeInfos[0].languageCode).toEqual(existingValue.languageCode);
        });

    });


    describe("localized data in addresses", () =>
    {
        let langs = OrganizationRegister.DataLocalization.languageCodes;
        let existingValue = new OrganizationRegister.LocalizedText("fi", "localized value");
        let existingValues = new Array<OrganizationRegister.LocalizedText>(existingValue);
        let streetAddress = new OrganizationRegister.StreetAddress(existingValues, "00100", existingValues);
        let poBoxAdress = new OrganizationRegister.PostOfficeBoxAddress("Helsinki","00100", existingValues);

        let sutStreetAdr = new OrganizationRegister
            .Organization(null,
                null,
                existingValues,
                null,
                existingValues,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                streetAddress,
                existingValues,
                false,
                streetAddress);
        let sutPoBox = new OrganizationRegister
            .Organization(null,
                null,
                existingValues,
                null,
                existingValues,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            null,
                null,
                streetAddress,
                existingValues,
                false,
                null,
                poBoxAdress);

       
        it("should initially exists for each address data to be localized", () =>
        {
            expect(sutStreetAdr.visitingStreetAddresses.length).toEqual(langs.length);
            expect(sutStreetAdr.visitingAddressPostalDistricts.length).toEqual(langs.length);
            expect(sutStreetAdr.visitingAddressQualifiers.length).toEqual(langs.length);
            expect(sutStreetAdr.postalStreetAddressStreets.length).toEqual(langs.length);
            expect(sutStreetAdr.postalStreetAddressPostalDistricts.length).toEqual(langs.length);
            expect(sutPoBox.postalPostOfficeBoxAddressPostalDistricts.length).toEqual(langs.length);
        });

        it("should have localized values for existing localized address data", () =>
        {
            expect(sutStreetAdr.visitingStreetAddresses
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    existingValue.localizedValue === arrVal.localizedValue &&
                    existingValue.languageCode === arrVal.languageCode)))
                .toBeTruthy();

            expect(sutStreetAdr.visitingAddressPostalDistricts
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    existingValue.localizedValue === arrVal.localizedValue &&
                    existingValue.languageCode === arrVal.languageCode)))
                .toBeTruthy();

            expect(sutStreetAdr.visitingAddressQualifiers
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    existingValue.localizedValue === arrVal.localizedValue &&
                    existingValue.languageCode === arrVal.languageCode)))
                .toBeTruthy();

            expect(sutStreetAdr.postalStreetAddressStreets
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    existingValue.localizedValue === arrVal.localizedValue &&
                    existingValue.languageCode === arrVal.languageCode)))
                .toBeTruthy();

            expect(sutStreetAdr.postalStreetAddressPostalDistricts
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    existingValue.localizedValue === arrVal.localizedValue &&
                    existingValue.languageCode === arrVal.languageCode)))
                .toBeTruthy();

            expect(sutPoBox.postalPostOfficeBoxAddressPostalDistricts
                .some((arrVal: OrganizationRegister.LocalizedText) => (
                    existingValue.localizedValue === arrVal.localizedValue &&
                    existingValue.languageCode === arrVal.languageCode)))
                .toBeTruthy();
        });

        it("should not have localized values for non existing localized address data",
            () =>
            {
                expect(sutStreetAdr.visitingStreetAddresses
                    .some((arrVal: OrganizationRegister.LocalizedText) => (
                        arrVal.localizedValue !== "" &&
                        arrVal.languageCode !== existingValue.languageCode)))
                    .toBeFalsy();

                expect(sutStreetAdr.visitingAddressPostalDistricts
                    .some((arrVal: OrganizationRegister.LocalizedText) => (
                        arrVal.localizedValue !== "" &&
                        arrVal.languageCode !== existingValue.languageCode)))
                    .toBeFalsy();

                expect(sutStreetAdr.visitingAddressQualifiers
                    .some((arrVal: OrganizationRegister.LocalizedText) => (
                        arrVal.localizedValue !== "" &&
                        arrVal.languageCode !== existingValue.languageCode)))
                    .toBeFalsy();

                expect(sutStreetAdr.postalStreetAddressStreets
                    .some((arrVal: OrganizationRegister.LocalizedText) => (
                        arrVal.localizedValue !== "" &&
                        arrVal.languageCode !== existingValue.languageCode)))
                    .toBeFalsy();

                expect(sutStreetAdr.postalStreetAddressPostalDistricts
                    .some((arrVal: OrganizationRegister.LocalizedText) => (
                        arrVal.localizedValue !== "" &&
                        arrVal.languageCode !== existingValue.languageCode)))
                    .toBeFalsy();

                expect(sutPoBox.postalPostOfficeBoxAddressPostalDistricts
                    .some((arrVal: OrganizationRegister.LocalizedText) => (
                        arrVal.localizedValue !== "" &&
                        arrVal.languageCode !== existingValue.languageCode)))
                    .toBeFalsy();

            });

        it("should exists only for address data with localized values set ", () =>
        {
            sutStreetAdr.generateVisitingAddressLocalizedTexts();
            expect(sutStreetAdr.visitingAddress.streetAddresses.length).toEqual(existingValues.length);
            expect(sutStreetAdr.visitingAddress.streetAddresses[0].localizedValue).toEqual(existingValue.localizedValue);
            expect(sutStreetAdr.visitingAddress.postalDistricts.length).toEqual(existingValues.length);
            expect(sutStreetAdr.visitingAddress.postalDistricts[0].localizedValue).toEqual(existingValue.localizedValue);
            expect(sutStreetAdr.visitingAddressQualifiers.length).toEqual(existingValues.length);
            expect(sutStreetAdr.visitingAddressQualifiers[0].localizedValue).toEqual(existingValue.localizedValue);

            sutStreetAdr.generatePostalAddressLocalizedTexts();
            expect(sutStreetAdr.postalStreetAddress.streetAddresses.length).toEqual(existingValues.length);
            expect(sutStreetAdr.postalStreetAddress.streetAddresses[0].localizedValue).toEqual(existingValue.localizedValue);
            expect(sutStreetAdr.postalStreetAddress.postalDistricts.length).toEqual(existingValues.length);
            expect(sutStreetAdr.postalStreetAddress.postalDistricts[0].localizedValue).toEqual(existingValue.localizedValue);

            sutPoBox.generatePostalAddressLocalizedTexts();
            expect(sutPoBox.postalPostOfficeBoxAddress.postalDistricts.length).toEqual(existingValues.length);
            expect(sutPoBox.postalPostOfficeBoxAddress.postalDistricts[0].languageCode).toEqual(existingValue.languageCode);

        });

    });


});
