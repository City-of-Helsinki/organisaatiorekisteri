"use strict";

describe("classification", () =>
{
    var sut: OrganizationRegister.Classification;
    var availableClasses: OrganizationRegister.Tree;

    function createSingleClassClassificationAndSelectOnlyClass(classId: string)
    {
        availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical(classId, "class", null)));
        sut = new OrganizationRegister.Classification(availableClasses);
        sut.toggleSelection(classId, true);
    }

    function createRootAndChildClassClassificationAndSelectChildClass(rootClassId: string, childClassId: string)
    {
        availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical(rootClassId, "root",
            new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical(childClassId, "leaf", null)))));
        sut = new OrganizationRegister.Classification(availableClasses);
        sut.toggleSelection(childClassId, true);
    }

    function createSingleClassClassificationWithTheClassAdded(classId: string)
    {
        availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical(classId, "class", null)));
        var addedIds: Array<string> = new Array<string>(classId);
        sut = new OrganizationRegister.Classification(availableClasses, addedIds);        
    }

    describe("initialization without pre-added classes", () =>
    {
        it("nothing is selected", () =>
        {
            sut = new OrganizationRegister.Classification(availableClasses);
            expect(sut.isClassSelected()).toBeFalsy();
        });
        it("nothing is added", () =>
        {
            sut = new OrganizationRegister.Classification(availableClasses);
            expect(sut.areClassesAdded()).toBeFalsy();
        });
        it("nothing is expanded", () =>
        {
            sut = new OrganizationRegister.Classification(availableClasses);
            expect(sut.expandedAvailable.length).toBe(0);
        });
    });

    describe("initialization with pre-added classes", () =>
    {
        it("nothing is selected", () =>
        {
            createSingleClassClassificationWithTheClassAdded("1");

            expect(sut.isClassSelected()).toBeFalsy();
        });
        it("classes are added", () =>
        {
            var classId: string = "1";
            createSingleClassClassificationWithTheClassAdded(classId);

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe(classId);
        });
        it("nothing is expanded", () =>
        {
            sut = new OrganizationRegister.Classification(availableClasses);
            expect(sut.expandedAvailable.length).toBe(0);
        });
    });

    describe("selecting a class", () =>
    {
        it("from the root", () =>
        {
            var rootClassId: string = "1";
            createSingleClassClassificationAndSelectOnlyClass(rootClassId);

            expect(sut.isClassSelected()).toBeTruthy();
        });
        it("from the leaf", () =>
        {
            var leafClassId: string = "1.1";
            createRootAndChildClassClassificationAndSelectChildClass("1", leafClassId);

            expect(sut.isClassSelected()).toBeTruthy();
        });
        it("unselecting a class", () =>
        {
            var leafClassId: string = "1.1";
            createRootAndChildClassClassificationAndSelectChildClass("1", leafClassId);

            sut.toggleSelection(leafClassId, false);

            expect(sut.isClassSelected()).toBeFalsy();
        });
    });

    describe("adding a class", () =>
    {
        it("unavailable class cannot be added", () =>
        {
            availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>());
            sut = new OrganizationRegister.Classification(availableClasses);
            sut.toggleSelection("1", true);

            expect(() => sut.addSelected()).toThrow();
        });
        it("nothing is added if nothing is selected", () =>
        {
            availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>());
            sut = new OrganizationRegister.Classification(availableClasses);

            sut.addSelected();

            expect(sut.areClassesAdded()).toBeFalsy();
            expect(sut.added.length).toBe(0);
        });
        it("adding a root class when it has no children", () =>
        {
            var rootClassId: string = "1";
            createSingleClassClassificationAndSelectOnlyClass(rootClassId);

            sut.addSelected();

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe(rootClassId);
        });
        it("adding a root class when it has children", () =>
        {
            var rootClassId: string = "1";
            var childClassId: string = "1.1";
            availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical(rootClassId, "root",
                new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical(childClassId, "leaf", null)))));
            sut = new OrganizationRegister.Classification(availableClasses);
            sut.toggleSelection(rootClassId, true);

            sut.addSelected();

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe(rootClassId);
            expect(sut.added[0].children.length).toBe(0);
        });
        it("adding a child class", () =>
        {
            var leafClassId: string = "1.1";
            createRootAndChildClassClassificationAndSelectChildClass("1", leafClassId);

            sut.addSelected();

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe(leafClassId);
            expect(sut.added[0].children.length).toBe(0);
        });
    });

    describe("removing a class", () =>
    {
        it("nothing is removed if nothing is available", () =>
        {
            availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>());
            sut = new OrganizationRegister.Classification(availableClasses);

            sut.remove("1");

            expect(sut.areClassesAdded()).toBeFalsy();
            expect(sut.added.length).toBe(0);
        });
        it("nothing is removed if nothing is added", () =>
        {
            var rootClassId: string = "1";
            createSingleClassClassificationAndSelectOnlyClass(rootClassId);

            sut.remove(rootClassId);

            expect(sut.areClassesAdded()).toBeFalsy();
            expect(sut.added.length).toBe(0);
        });
        it("root class with children", () =>
        {
            var rootClassId: string = "1";
            var childClassId: string = "1.1";
            availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical(rootClassId, "root",
                new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical(childClassId, "leaf", null)))));
            sut = new OrganizationRegister.Classification(availableClasses);
            sut.toggleSelection(rootClassId, true);
            sut.addSelected();
            sut.toggleSelection(childClassId, true);
            sut.addSelected();

            sut.remove(rootClassId);

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe(childClassId);
        });
        it("root class without children", () =>
        {
            var rootClassId: string = "1";
            createSingleClassClassificationWithTheClassAdded(rootClassId);

            sut.remove(rootClassId);

            expect(sut.areClassesAdded()).toBeFalsy();
            expect(sut.added.length).toBe(0);
        });
        it("child class", () =>
        {
            var leafClassId: string = "1.1";
            createRootAndChildClassClassificationAndSelectChildClass("1", leafClassId);
            sut.addSelected();

            sut.remove(leafClassId);

            expect(sut.areClassesAdded()).toBeFalsy();
            expect(sut.added.length).toBe(0);
        });
    });

    describe("expanding and collapsing available classes", () =>
    {
        it("expanding all expands classes with children", () =>
        {
            availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical("1", "root",
                new Array<OrganizationRegister.Hierarchical>(new OrganizationRegister.Hierarchical("1.1", "leaf", null))),
                new OrganizationRegister.Hierarchical("2", "another root", null)));
            sut = new OrganizationRegister.Classification(availableClasses);

            sut.expandAvailable();

            expect(sut.expandedAvailable.length).toBe(1);
            expect(sut.expandedAvailable[0].id).toBe("1");
        });
        it("collapsing all", () =>
        {
            createRootAndChildClassClassificationAndSelectChildClass("1", "1.1");
            sut = new OrganizationRegister.Classification(availableClasses);
            sut.expandAvailable();

            sut.collapseAvailable();

            expect(sut.expandedAvailable.length).toBe(0);
        });
    });
});