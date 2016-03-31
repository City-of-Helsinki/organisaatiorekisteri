"use strict";

module OrganizationRegister
{
    export class HierarchicalSelectionClassification extends Classification
    {
        constructor(available: Tree, addedIds?: Array<string>)
        {
            super(available, addedIds);
        }

        public addSelected(): void
        {
            if (this.selectedClassId != null && !this.addedCollection.contains(this.selectedClassId))
            {
                var rootClass: Hierarchical = this.availableCollection.getCopyWithParentChainAndNoChildren(this.selectedClassId);
                if (rootClass == null)
                {
                    throw new ClassNotAvailableException("Selected class " + this.selectedClassId + " not available.");
                }
                if (this.addedCollection.containsRoot(rootClass.id))
                {
                    this.addChildToAddedParent(rootClass, rootClass.children[0]);
                }
                else
                {
                    this.added.push(rootClass);
                }
            }
        }

        private addChildToAddedParent(parent: Hierarchical, child: Hierarchical): void
        {
            if (this.addedCollection.contains(child.id))
            {
                this.addChildToAddedParent(parent.children[0], child.children[0]);
            }
            else
            {
                var addedParent: Hierarchical = this.addedCollection.get(parent.id);
                addedParent.children.push(child);
            }
        }
    }
}