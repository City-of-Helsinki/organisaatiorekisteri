"use strict";

describe("languages", () =>
{
    var sut: OrganizationRegister.Languages;

    describe("comma separated name list", () =>
    {
        it("name list is empty when language collection is null", () =>
        {
            sut = new OrganizationRegister.Languages(null);
            expect(sut.commaSeparatedNames).toEqual("");
        });
        it("name list is empty when language collection is empty", () =>
        {
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>());
            expect(sut.commaSeparatedNames).toEqual("");
        });
        it("multiple languages", () =>
        {
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>(new OrganizationRegister.Language("fi", "suomi"), new OrganizationRegister.Language("sv", "ruotsi"),
                new OrganizationRegister.Language("en", "englanti")));
            expect(sut.commaSeparatedNames).toEqual("suomi, ruotsi, englanti");
        });
        it("one language needs no separator", () =>
        {
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>(new OrganizationRegister.Language("fi", "suomi")));
            expect(sut.commaSeparatedNames).toEqual("suomi");
        });
    });

    describe("codes", () =>
    {
        it("no codes when language collection is null", () =>
        {
            sut = new OrganizationRegister.Languages(null);
            expect(sut.codes.length).toEqual(0);
        });
        it("no codes when language collection is empty", () =>
        {
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>());
            expect(sut.codes.length).toEqual(0);
        });
        it("multiple languages", () =>
        {
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>(new OrganizationRegister.Language("fi", "suomi"), new OrganizationRegister.Language("sv", "ruotsi"),
                new OrganizationRegister.Language("en", "englanti")));
            expect(sut.codes).toContain("fi");
            expect(sut.codes).toContain("en");
            expect(sut.codes).toContain("sv");
            expect(sut.codes.length).toEqual(3);
        });
        it("one language", () =>
        {
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>(new OrganizationRegister.Language("fi", "suomi")));
            expect(sut.codes).toContain("fi");
            expect(sut.codes.length).toEqual(1);
        });
    });

    describe("filter", () =>
    {
        it("language collection is null", () =>
        {
            sut = new OrganizationRegister.Languages(null);
            expect(sut.filter(null).length).toEqual(0);
        });
        it("language collection is empty", () =>
        {
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>());
            expect(sut.filter(null).length).toEqual(0);
        });
        it("codes filter is null", () =>
        {
            var expectedResult = new OrganizationRegister.Language("fi", "suomi");
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>(expectedResult));
            expect(sut.filter(null).length).toEqual(0);
        });
        it("codes filter is empty", () =>
        {
            var expectedResult = new OrganizationRegister.Language("fi", "suomi");
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>(expectedResult));
            expect(sut.filter(new Array<string>()).length).toEqual(0);
        });
        it("codes filter contains all languages", () =>
        {
            var expectedResult1 = new OrganizationRegister.Language("fi", "suomi");
            var expectedResult2 = new OrganizationRegister.Language("sv", "ruotsi");
            var filter = new Array<string>("fi", "sv");
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>(expectedResult1, expectedResult2));
            var result = sut.filter(filter);
            expect(result.length).toEqual(2);
            expect(result[0]).toEqual(expectedResult1);
            expect(result[1]).toEqual(expectedResult2);
        });
        it("codes filter contains some of the languages", () =>
        {
            var expectedResult = new OrganizationRegister.Language("fi", "suomi");
            var filter = new Array<string>("fi");
            sut = new OrganizationRegister.Languages(new Array<OrganizationRegister.Language>(expectedResult, new OrganizationRegister.Language("sv", "ruotsi"),
                new OrganizationRegister.Language("en", "englanti")));
            var result = sut.filter(filter);
            expect(result.length).toEqual(1);
            expect(result[0]).toEqual(expectedResult);
        });
    });
});
 