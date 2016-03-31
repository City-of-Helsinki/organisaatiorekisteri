"use strict";

describe("Service", () =>
{
    var sut: OrganizationRegister.Service;

    function assertClassificationCollectionLength(length: number)
    {
        expect(sut.serviceClasses.length).toBe(length);
        expect(sut.targetGroups.length).toBe(length);
        expect(sut.lifeEvents.length).toBe(length);
    }

    describe("Language names", () =>
    {
        it("Language names are listed separated with a comma", () =>
        {
            sut = new OrganizationRegister.Service(null, null, null, null, null, null, null, null,
                new Array<OrganizationRegister.Language>(new OrganizationRegister.Language("fi", "finnish"), new OrganizationRegister.Language("en", "english"),
                    new OrganizationRegister.Language("sv", "swedish")));
            expect(sut.languageNameList).toEqual("finnish, english, swedish");
        });
        it("Language names collection with one language", () =>
        {
            sut = new OrganizationRegister.Service(null, null, null, null, null, null, null, null,
                new Array<OrganizationRegister.Language>(new OrganizationRegister.Language("fi", "finnish")));
            expect(sut.languageNameList).toEqual("finnish");
        });
        it("Null language names collection", () =>
        {
            sut = new OrganizationRegister.Service();
            expect(sut.languageNameList).toEqual("");
        });
        it("Empty language names collection", () =>
        {
            sut = new OrganizationRegister.Service(null, null, null, null, null, null, null, null, new Array<OrganizationRegister.Language>());
            expect(sut.languageNameList).toEqual("");
        });
    });

    describe("Classification", () =>
    {
        it("Setting null classifications", () =>
        {
            sut = new OrganizationRegister.Service();

            sut.setClassification(null, null, null);

            assertClassificationCollectionLength(0);
        });
        it("Setting initial classifications", () =>
        {
            var serviceClasses: Array<OrganizationRegister.Hierarchical> = new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical("1", "class", null));
            var targetGroups: Array<OrganizationRegister.Hierarchical> = new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical("3", "class", null));
            var lifeEvents: Array<OrganizationRegister.Hierarchical> = new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical("4", "class", null));

            sut = new OrganizationRegister.Service(null, null, null, null, null, null, null, null, new Array<OrganizationRegister.Language>(), serviceClasses, null, targetGroups,
                lifeEvents);

            assertClassificationCollectionLength(1);
            expect(sut.serviceClasses[0]).toBe("1");
            expect(sut.targetGroups[0]).toBe("3");
            expect(sut.lifeEvents[0]).toBe("4");
        });
        it("Updating initial classifications", () =>
        {
            var firstClassification: Array<OrganizationRegister.Hierarchical> = new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical("1", "class", null));
            var secondClassification: Array<OrganizationRegister.Hierarchical> = new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical("2", "class", null));
            var thirdClassification: Array<OrganizationRegister.Hierarchical> = new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical("3", "class", null));
            sut = new OrganizationRegister.Service(null, null, null, null, null, null, null, null, new Array<OrganizationRegister.Language>(), firstClassification, null, secondClassification,
                thirdClassification);

            sut.setClassification(new OrganizationRegister.Tree(thirdClassification), new OrganizationRegister.Tree(secondClassification),
                new OrganizationRegister.Tree(firstClassification));

            assertClassificationCollectionLength(1);
            expect(sut.serviceClasses[0]).toBe("3");
            expect(sut.targetGroups[0]).toBe("2");
            expect(sut.lifeEvents[0]).toBe("1");
        });
        it("Setting hierarchical classification", () =>
        {
            var child: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1.1.1", "leaf", null);
            var childAndParent: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1.1", "child", new Array<OrganizationRegister.Hierarchical>(child));
            var parent: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1", "parent", new Array<OrganizationRegister.Hierarchical>(childAndParent));
            var serviceClasses: OrganizationRegister.Tree = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(parent));
            sut = new OrganizationRegister.Service();

            sut.setClassification(serviceClasses, null, null);

            expect(sut.serviceClasses.length).toBe(3);
            expect(sut.serviceClasses.indexOf("1")).toBeGreaterThan(-1);
            expect(sut.serviceClasses.indexOf("1.1")).toBeGreaterThan(-1);
            expect(sut.serviceClasses.indexOf("1.1.1")).toBeGreaterThan(-1);
        });
        it("Clearing classification", () =>
        {
            var child: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1.1.1", "leaf", null);
            var childAndParent: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1.1", "child", new Array<OrganizationRegister.Hierarchical>(child));
            var parent: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1", "parent", new Array<OrganizationRegister.Hierarchical>(childAndParent));
            var serviceClasses: OrganizationRegister.Tree = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(parent));
            sut = new OrganizationRegister.Service();

            sut.setClassification(serviceClasses, null, null);
            sut.setClassification(new OrganizationRegister.Tree(), null, null);

            assertClassificationCollectionLength(0);
        });
        it("Setting classifications on the same level", () =>
        {
            var child1: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1.1", "child", null);
            var child2: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1.2", "child", null);
            var parent1: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1", "parent", new Array<OrganizationRegister.Hierarchical>(child1, child2));
            var parent2: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("2", "parent", null);
            var serviceClasses: OrganizationRegister.Tree = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(parent1, parent2));
            sut = new OrganizationRegister.Service();

            sut.setClassification(serviceClasses, null, null);

            expect(sut.serviceClasses.length).toBe(4);
            expect(sut.serviceClasses.indexOf("1")).toBeGreaterThan(-1);
            expect(sut.serviceClasses.indexOf("2")).toBeGreaterThan(-1);
            expect(sut.serviceClasses.indexOf("1.1")).toBeGreaterThan(-1);
            expect(sut.serviceClasses.indexOf("1.2")).toBeGreaterThan(-1);
        });
    });
});
