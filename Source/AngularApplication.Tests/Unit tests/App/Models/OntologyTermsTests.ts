"use strict";

describe("ontology terms", () =>
{
    var sut: OrganizationRegister.OntologyTerms;

    describe("initialization without pre-added terms", () =>
    {
        it("nothing is added", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            expect(sut.added.length).toBe(0);
            expect(sut.areTermsAdded()).toBeFalsy();
        });
        it("nothing is available", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            expect(sut.available.length).toBe(0);
        });
    });

    describe("initialization with pre-added terms", () =>
    {
        it("added terms are populated", () =>
        {
            sut = new OrganizationRegister.OntologyTerms(new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2")));
            expect(sut.added.length).toBe(2);
            expect(sut.areTermsAdded()).toBeTruthy();
        });
        it("nothing is available", () =>
        {
            sut = new OrganizationRegister.OntologyTerms(new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2")));
            expect(sut.available.length).toBe(0);
        });
    });

    describe("available terms", () =>
    {
        it("clearing", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.clearAvailable();
            expect(sut.available.length).toBe(0);
        });
    });

    describe("removing terms", () =>
    {
        it("nothing is removed when nothing is added", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.remove("1");
            expect(sut.areTermsAdded()).toBeFalsy();
        });
        it("nothing is removed when removing an item that is not added", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.added = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.remove("3");
            expect(sut.added.length).toBe(2);
        });
        it("removing an item that is added", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.added = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.remove("1");
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe("2");
        });
    });

    describe("selecting a term", () =>
    {
        it("nothing is selected at first", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            expect(sut.isTermSelected()).toBeFalsy();
        });
        it("setting empty selection", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.selected = new Array<OrganizationRegister.Class>();
            expect(sut.isTermSelected()).toBeFalsy();
        });
        it("setting a single selection", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.selected = new Array<OrganizationRegister.Class>(sut.available[0]);
            expect(sut.isTermSelected()).toBeTruthy();
        });
        it("setting multiple selections", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.selected = new Array<OrganizationRegister.Class>(sut.available[0], sut.available[1]);
            expect(sut.isTermSelected()).toBeTruthy();
        });
    });

    describe("adding terms", () =>
    {
        it("adding selected term", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.selected = new Array<OrganizationRegister.Class>(sut.available[0]);
            sut.addSelected();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe("1");
        });
        it("adding multiple selected terms", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.selected = new Array<OrganizationRegister.Class>(sut.available[0], sut.available[1]);
            sut.addSelected();
            expect(sut.added.length).toBe(2);
            expect(sut.added[0].id).toBe("1");
            expect(sut.added[1].id).toBe("2");
        });
        it("adding selected term twice", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.selected = new Array<OrganizationRegister.Class>(sut.available[0]);
            sut.addSelected();
            sut.addSelected();
            expect(sut.added.length).toBe(1);
        });
        it("adding some new terms and some already added terms", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.selected = new Array<OrganizationRegister.Class>(sut.available[0]);
            sut.addSelected();
            sut.selected = new Array<OrganizationRegister.Class>(sut.available[0], sut.available[1]);
            sut.addSelected();
            expect(sut.added.length).toBe(2);
            expect(sut.added[0].id).toBe("1");
            expect(sut.added[1].id).toBe("2");
        });
        it("adding when nothing is selected", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.addSelected();
            expect(sut.added.length).toBe(0);
        });
        it("adding an unavailable term", () =>
        {
            sut = new OrganizationRegister.OntologyTerms();
            sut.available = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("1", "term1"), new OrganizationRegister.Class("2", "term2"));
            sut.selected = new Array<OrganizationRegister.Class>(new OrganizationRegister.Class("3", "term3"));
            expect(() => sut.addSelected()).toThrow();
        });
    });
});
