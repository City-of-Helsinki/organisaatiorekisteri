"use strict";
describe("hierarchical selection classification", function () {
    var sut;
    var availableClasses;
    // Creates the following tree:
    // 1            2
    //       2.1           2.2
    //  2.1.1   2.1.2
    function createClassificationWithHierarchyOfAvailableClasses() {
        var class211 = new OrganizationRegister.Hierarchical("2.1.1", "leaf1", null);
        var class212 = new OrganizationRegister.Hierarchical("2.1.2", "leaf2", null);
        var class21 = new OrganizationRegister.Hierarchical("2.1", "child1", new Array(class211, class212));
        var class22 = new OrganizationRegister.Hierarchical("2.2", "child2", null);
        var class1 = new OrganizationRegister.Hierarchical("1", "root1", null);
        var class2 = new OrganizationRegister.Hierarchical("2", "root2", new Array(class21, class22));
        availableClasses = new OrganizationRegister.Tree(new Array(class1, class2));
        sut = new OrganizationRegister.HierarchicalSelectionClassification(availableClasses);
    }
    describe("adding a class", function () {
        it("unavailable class cannot be added", function () {
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection("5", true);
            expect(function () { return sut.addSelected(); }).toThrow();
        });
        it("nothing is added if nothing is selected", function () {
            createClassificationWithHierarchyOfAvailableClasses();
            sut.addSelected();
            expect(sut.areClassesAdded()).toBeFalsy();
            expect(sut.added.length).toBe(0);
        });
        it("adding a root class when it has no children", function () {
            var rootWithNoChildren = "1";
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection(rootWithNoChildren, true);
            sut.addSelected();
            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe(rootWithNoChildren);
            expect(sut.added[0].children.length).toBe(0);
        });
        it("adding a root class when it has children", function () {
            var rootWithChildren = "2";
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection(rootWithChildren, true);
            sut.addSelected();
            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe(rootWithChildren);
            expect(sut.added[0].children.length).toBe(0);
        });
        it("adding a child class with a grand parent", function () {
            var childClassId = "2.1.1";
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
        it("adding a second child class", function () {
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
        it("adding a second leaf class", function () {
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
    describe("removing a class", function () {
        it("root class with children", function () {
            var rootClassId = "2";
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection(rootClassId, true);
            sut.addSelected();
            sut.remove(rootClassId);
            expect(sut.areClassesAdded()).toBeFalsy();
            expect(sut.added.length).toBe(0);
        });
        it("leaf class", function () {
            var leafClassId = "2.2";
            createClassificationWithHierarchyOfAvailableClasses();
            sut.toggleSelection(leafClassId, true);
            sut.addSelected();
            sut.remove(leafClassId);
            expect(sut.areClassesAdded()).toBeTruthy();
            expect(sut.added.length).toBe(1);
            expect(sut.added[0].id).toBe("2");
            expect(sut.added[0].children.length).toBe(0);
        });
        it("child class with children", function () {
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
//# sourceMappingURL=HierarchicalSelectionClassificationTests.js.map