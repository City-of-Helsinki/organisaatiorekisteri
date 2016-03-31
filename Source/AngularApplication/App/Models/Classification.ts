"use strict";

module OrganizationRegister
{
    export class Classification
    {
        protected selectedClassId: string;

        public addedCollection: Tree;

        constructor(protected availableCollection: Tree, addedIds?: Array<string>)
        {
            if (this.availableCollection == null)
            {
                this.availableCollection = new Tree();
            }

            this.addedCollection = new Tree();
            if (addedIds != null)
            {
                addedIds.forEach((id: string) =>
                {
                    this.toggleSelection(id, true);
                    this.addSelected();
                    this.toggleSelection(id, false);
                });                
            }
        }

        public get added(): Array<Hierarchical>
        {
            return this.addedCollection.value;
        }

        public get available(): Array<Hierarchical>
        {
            return this.availableCollection.value;
        }

        public get expandedAvailable(): Array<Hierarchical>
        {
            return this.availableCollection.expanded;
        }

        public isClassSelected(): boolean
        {
            return this.selectedClassId != null;
        }

        public areClassesAdded(): boolean
        {
            return this.added.length > 0;
        }

        public expandAvailable(): void
        {
            this.availableCollection.expandAll();
        }

        public collapseAvailable(): void
        {
            this.availableCollection.collapseAll();
        }

        public toggleSelection(classId: string, selected: boolean): void
        {
            if (selected)
            {
                this.selectedClassId = classId;
            }
            else
            {
                this.selectedClassId = null;
            }
        }

        public addSelected(): void
        {
            if (this.selectedClassId != null && !this.addedCollection.containsRoot(this.selectedClassId))
            {
                var availableClass: Hierarchical = this.availableCollection.get(this.selectedClassId);
                if (availableClass == null)
                {
                    throw new ClassNotAvailableException("Selected class " + this.selectedClassId + " not available.");
                }
                this.added.push(new Hierarchical(availableClass.id, availableClass.name, new Array<Hierarchical>()));
            }
        }

        public remove(classId: string): void
        {
            this.addedCollection.remove(classId);
        }
    }
}