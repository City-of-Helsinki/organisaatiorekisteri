"use strict";

module OrganizationRegister
{
    export class AvailablePostalAddressTypes
    {
        public types: Array<string>;

        constructor()
        {
            this.types = new Array<string>();
        }

        public setAvailable(addressType: PostalAddressType): void
        {
            var addressTypeString: string = PostalAddressType[addressType];
            if (!this.contains(addressTypeString))
            {
                this.types.push(addressTypeString);
                this.sortTypes();
            }
        }

        public setUnavailable(addressType: PostalAddressType): void
        {
            var addressTypeString: string = PostalAddressType[addressType];
            for (var i = 0; i < this.types.length; i++)
            {
                if (this.types[i] === addressTypeString)
                {
                    this.types.splice(i, 1);
                    break;
                }
            }
        }

        public any(): boolean
        {
            return this.types.length > 0;
        }

        public firstOrDefault(): string
        {
            if (this.any())
            {
                return this.types[0];
            }
            return null;
        }

        public contains(addressType: string): boolean
        {
            return this.types.length > 0 && !this.types.every((item: string) =>
            {
                return item !== addressType;
            });
        }

        private sortTypes(): void
        {
            if (this.types.indexOf((PostalAddressType[PostalAddressType.SameAsVisitingAddress])) !== 0)
            {
                this.moveType(this.types.indexOf(PostalAddressType[PostalAddressType.SameAsVisitingAddress]), 0);
            }
            if (this.types.indexOf((PostalAddressType[PostalAddressType.SeparateStreetAddress])) !== 1)
            {
                this.moveType(this.types.indexOf(PostalAddressType[PostalAddressType.SeparateStreetAddress]), 1);
            }
            if (this.types.indexOf((PostalAddressType[PostalAddressType.PostOfficeBoxAddress])) !== 2)
            {
                this.moveType(this.types.indexOf(PostalAddressType[PostalAddressType.PostOfficeBoxAddress]), 2);
            }
        }

        private moveType(currentIndex: number, newIndex: number): void
        {
            this.types.splice(newIndex, 0, this.types.splice(currentIndex, 1)[0]);
        }
    }
} 