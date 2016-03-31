"use strict";

module OrganizationRegister
{
    export class Tree
    {
        public value: Array<Hierarchical>;
        public expanded: Array<Hierarchical>;

        constructor(items?: Array<Hierarchical>)
        {
            if (items == null)
            {
                this.value = new Array<Hierarchical>();
            }
            else
            {
                this.value = items;
            }
            this.expanded = new Array<Hierarchical>();
        }

        public remove(id: string): void
        {
            this.removeFromCollection(id, this.value);
        }

        public containsRoot(id: string): boolean
        {
            return this.containsOnTopLevel(id, this.value);
        }

        public contains(id: string): boolean
        {
            return this.containsOnAnyLevel(id, this.value);
        }

        public get(id: string): Hierarchical
        {
            return this.getFromCollection(id, this.value);
        }

        public getCopyWithParentChainAndNoChildren(id: string): Hierarchical
        {
            return this.getClassCopyWithParentChainAndNoChildrenFromCollection(id, this.value);
        }

        public getIds(): Array<string>
        {
            return this.getCollectionIds(this.value);
        }

        public expandAll(): void
        {
            if (this.expanded.length === 0)
            {
                this.pushToExpanded(this.value);
            }
        }

        public collapseAll(): void
        {
            this.expanded.splice(0, this.expanded.length);
        }

        private containsOnTopLevel(id: string, collection: Array<Hierarchical>): boolean
        {
            if (collection != null)
            {
                return collection.some((item: Hierarchical) => item.id === id);
            }
            return false;
        }

        private removeFromCollection(id: string, collection: Array<Hierarchical>): void
        {
            if (collection != null)
            {
                if (this.containsOnTopLevel(id, collection))
                {
                    var foundClass: Hierarchical = this.getFromCollection(id, collection);
                    collection.splice(collection.indexOf(foundClass, 0), 1);
                }
                else
                {
                    collection.forEach((item: Hierarchical) =>
                    {
                        this.removeFromCollection(id, item.children);
                    });
                }
            }
        }

        private getFromCollection(id: string, collection: Array<Hierarchical>): Hierarchical
        {
            if (collection != null)
            {
                if (this.containsOnTopLevel(id, collection))
                {
                    return collection.filter((item: Hierarchical) => item.id === id)[0];
                }
                for (var i = 0; i < collection.length; i++)
                {
                    var foundClass: Hierarchical = this.getFromCollection(id, collection[i].children);
                    if (foundClass != null)
                    {
                        return foundClass;
                    }
                }
            }
            return null;
        }

        private containsOnAnyLevel(id: string, collection: Array<Hierarchical>): boolean
        {
            if (collection != null)
            {
                if (this.containsOnTopLevel(id, collection))
                {
                    return true;
                }
                for (var i = 0; i < collection.length; i++)
                {
                    if (this.containsOnAnyLevel(id, collection[i].children))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private getClassCopyWithParentChainAndNoChildrenFromCollection(id: string, collection: Array<Hierarchical>): Hierarchical
        {
            if (collection != null)
            {
                if (this.containsOnTopLevel(id, collection))
                {
                    var foundClass: Hierarchical = this.getFromCollection(id, collection);
                    return new Hierarchical(foundClass.id, foundClass.name, new Array<Hierarchical>());
                }
                for (var i = 0; i < collection.length; i++)
                {
                    if (this.containsOnAnyLevel(id, collection[i].children))
                    {
                        return new Hierarchical(collection[i].id, collection[i].name,
                            new Array<Hierarchical>(this.getClassCopyWithParentChainAndNoChildrenFromCollection(id, collection[i].children)));
                    }
                }
            }
            return null;
        }

        private getCollectionIds(collection: Array<Hierarchical>): Array<string>
        {
            var ids: Array<string> = new Array<string>();
            if (collection != null && collection.length > 0)
            {
                for (var i = 0; i < collection.length; i++)
                {
                    ids.push(collection[i].id);
                    ids = ids.concat(this.getCollectionIds(collection[i].children));
                }
            }
            return ids;
        }

        private pushToExpanded(collection: Array<Hierarchical>): void
        {
            collection.forEach((item: Hierarchical) =>
            {
                if (item.children != null && item.children.length > 0)
                {
                    this.expanded.push(item);
                    this.pushToExpanded(item.children);
                }
            });
        }
    }
}   