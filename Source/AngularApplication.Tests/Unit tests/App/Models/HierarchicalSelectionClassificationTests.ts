"use strict";

describe("hierarchical selection classification", () =>
{
    var sut: OrganizationRegister.HierarchicalSelectionClassification;
    var availableClasses: OrganizationRegister.Tree;

    // Creates the following tree:
    // 1            2
    //       2.1           2.2
    //  2.1.1   2.1.2
    function createClassificationWithHierarchyOfAvailableClasses()
    {
        var class211: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("2.1.1", "leaf1", null);
        var class212: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("2.1.2", "leaf2", null);
        var class21: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("2.1", "child1", new Array<OrganizationRegister.Hierarchical>(class211, class212));
        var class22: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("2.2", "child2", null);
        var class1: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("1", "root1", null);
        var class2: OrganizationRegister.Hierarchical = new OrganizationRegister.Hierarchical("2", "root2", new Array<OrganizationRegister.Hierarchical>(class21, class22));
        
        availableClasses = new OrganizationRegister.Tree(new Array<OrganizationRegister.Hierarchical>(class1, class2));
        sut = new OrganizationRegister.HierarchicalSelectionClassification(availableClasses);        
    }

    describe("adding a class", () =>
    {
        it("unavailable class cannot be added", () =>
        {
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection("5", true);

            expect(() => sut.addSelected()).toThrow();
        });
        it("nothing is added if nothing is selected", () =>
        {
            createClassificationWithHierarchyOfAvailableClasses();

            sut.addSelected();

            expect(sut.areClassesAdded()).toBeFalsy();
            expect(sut.added.length).toBe(0);
        });
        it("adding a root class when it has no children", () =>
        {
            var rootWithNoChildren: string = "1";
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection(rootWithNoChildren, true);

            sut.addSelected();

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe(rootWithNoChildren);
            expect(sut.added[0].children.length).toBe(0);
        });
        it("adding a root class when it has children", () =>
        {
            var rootWithChildren: string = "2";
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection(rootWithChildren, true);

            sut.addSelected();

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe(rootWithChildren);
            expect(sut.added[0].children.length).toBe(0);
        });
        it("adding a child class with a grand parent", () =>
        {
            var childClassId: string = "2.1.1";
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection(childClassId, true);

            sut.addSelected();

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe("2");
            expect(sut.added[0].children.length).toBe(1);
            expect(sut.added[0].children[0].id).toBe("2.1");
            expect(sut.added[0].children[0].children.length).toBe(1);
            expect(sut.added[0].children[0].children[0].id).toBe(childClassId);
        });
        it("adding a second child class", () =>
        {
            createClassificationWithHierarchyOfAvailableClasses();

            sut.toggleSelection("2.1", true);
            sut.addSelected();
            sut.toggleSelection("2.2", true);
            sut.addSelected();

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe("2");
            expect(sut.added[0].children.length).toBe(2);
            expect(sut.added[0].children[0].id).toBe("2.1");
            expect(sut.added[0].children[1].id).toBe("2.2");
            expect(sut.added[0].children[0].children.length).toBe(0);
            expect(sut.added[0].children[1].children.length).toBe(0);
        });
        it("adding a second leaf class", () =>
        {
            createClassificationWithHierarchyOfAvailableClasses();

            sut.toggleSelection("2.1.1", true);
            sut.addSelected();
            sut.toggleSelection("2.1.2", true);
            sut.addSelected();

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe("2");
            expect(sut.added[0].children.length).toBe(1);
            expect(sut.added[0].children[0].id).toBe("2.1");
            expect(sut.added[0].children[0].children.length).toBe(2);
            expect(sut.added[0].children[0].children[0].id).toBe("2.1.1");
            expect(sut.added[0].children[0].children[1].id).toBe("2.1.2");
        });
    });

    describe("removing a class", () =>
    {
        it("root class with children", () =>
        {
            var rootClassId: string = "2";
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection(rootClassId, true);
            sut.addSelected();

            sut.remove(rootClassId);

            expect(sut.areClassesAdded()).toBeFalsy();
            expect(sut.added.length).toBe(0);
        });
        it("leaf class", () =>
        {
            var leafClassId: string = "2.2";
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection(leafClassId, true);
            sut.addSelected();

            sut.remove(leafClassId);

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe("2");
            expect(sut.added[0].children.length).toBe(0);
        });
        it("child class with children", () =>
        {
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection("2.1.1", true);
            sut.addSelected();

            sut.remove("2.1");

            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe("2");
            expect(sut.added[0].children.length).toBe(0);
        });
    });
});
