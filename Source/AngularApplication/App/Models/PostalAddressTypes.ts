"use strict";

module OrganizationRegister
{
    export class PostalAddressTypes
    {
        public available: AvailablePostalAddressTypes;
        public added: Array<PostalAddressType>;

        constructor()
        {
            this.available = new AvailablePostalAddressTypes();
            this.added = new Array<PostalAddressType>();
        }

        public add(addressType: string): void
        {
            if (this.added.length > 0)
            {
                throw new Error("Only one postal address can be added.");
            }

            if (PostalAddressType[PostalAddressType.PostOfficeBoxAddress] === addressType)
            {
                this.added.push(PostalAddressType.PostOfficeBoxAddress);
                this.available.setUnavailable(PostalAddressType.PostOfficeBoxAddress);
            }
            else if (PostalAddressType[PostalAddressType.SameAsVisitingAddress] === addressType)
            {
                this.added.push(PostalAddressType.SameAsVisitingAddress);
                this.available.setUnavailable(PostalAddressType.SameAsVisitingAddress);
                this.available.setUnavailable(PostalAddressType.SeparateStreetAddress);
            }
            else if (PostalAddressType[PostalAddressType.SeparateStreetAddress] === addressType)
            {
                this.added.push(PostalAddressType.SeparateStreetAddress);
                this.available.setUnavailable(PostalAddressType.SameAsVisitingAddress);
                this.available.setUnavailable(PostalAddressType.SeparateStreetAddress);
            }
        }

        // todo: how to make generic array extensions?
        public contains(addressType: PostalAddressType): boolean
        {
            return this.added.length > 0 && !this.added.every((item: PostalAddressType) =>
            {
                return item !== addressType;
            });
        }

        // todo: how to make generic array extensions?
        public remove(addressType: PostalAddressType)
        {
            for (var i = 0; i < this.added.length; i++)
            {
                if (this.added[i] === addressType)
                {
                    this.added.splice(i, 1);
                    break;
                }
            }
        }

        public setSeparateStreetAddressPostalAddressAvailability(): void
        {
            if (this.contains(PostalAddressType.SeparateStreetAddress))
            {
                this.available.setUnavailable(PostalAddressType.SeparateStreetAddress);
            }
            else
            {
                this.available.setAvailable(PostalAddressType.SeparateStreetAddress);
            }
        }

        public setSameAsVisitingAddressPostalAddressAvailability(hasVisitingAddressParts: boolean): void
        {
            if (hasVisitingAddressParts && !this.contains(PostalAddressType.SeparateStreetAddress))
            {
                this.available.setAvailable(PostalAddressType.SameAsVisitingAddress);
            }
            else if (!hasVisitingAddressParts)
            {
                this.available.setUnavailable(PostalAddressType.SameAsVisitingAddress);
            }
        }

        public canAddPostalAddress(): boolean
        {
            return this.available.any() && this.added.length === 0;
        }
    }
}