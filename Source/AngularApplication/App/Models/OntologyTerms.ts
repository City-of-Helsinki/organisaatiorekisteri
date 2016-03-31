"use strict";

module OrganizationRegister
{
    export class OntologyTerms
    {
        public available: Array<Class>;
        public selected: Array<Class>;

        constructor(public added?: Array<Class>)
        {
            this.available = new Array<Class>();
            if (this.added == null)
            {
                this.added = new Array<Class>();
            }
        }

        public areTermsAdded(): boolean
        {
            return this.added.length > 0;
        }

        public isTermSelected(): boolean
        {
            return this.selected != null && this.selected.length > 0;
        }

        public remove(id: string): void
        {
            for (var i = 0; i < this.added.length; i++)
            {
                if (this.added[i].id === id)
                {
                    this.added.splice(i, 1);
                    return;
                }
            }
        }

        public addSelected(): void
        {
            if (this.isTermSelected())
            {
                if (!this.areSelectedAvailable())
                {
                    throw new ClassNotAvailableException("One or more selected ontology terms are not available.");
                }
                this.selected.forEach((item: Class) =>
                {
                    if (!this.isTermAdded(item))
                    {
                        this.added.push(item);
                    }
                });
            }
        }

        public clearAvailable(): void
        {
            this.available = new Array<Class>();
        }

        private areSelectedAlreadyAdded(): boolean
        {
            return this.areSelectedInCollection(this.added);
        }

        private areSelectedAvailable(): boolean
        {
            return this.areSelectedInCollection(this.available);
        }

        private areSelectedInCollection(collection: Array<Class>)
        {
            var foundSelected: Array<Class> = this.selected.filter((s: Class) =>
            {
                return collection.some((c: Class) =>
                {
                    return c.id === s.id;
                });
            });
            return foundSelected.length === this.selected.length;
        }

        private isTermAdded(term: Class): boolean
        {
            return this.added.some((item: Class) => item.id === term.id);
        }
    }
}   