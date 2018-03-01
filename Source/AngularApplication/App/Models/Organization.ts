"use strict";

module OrganizationRegister
{
    export class Organization implements Affecto.Base.IModel
    {
        
        public descriptionsAsHtml: Array<LocalizedText>;
        public validFromText: string;
        public validToText: string;
        public validFrom: string; // Web API data
        public validTo: string; // Web API data
        public webPageUrl: string;
        public webPageName: string;
        public webPageType: string;
        public editedWebPageUrl: string;
        public editedWebPageName: string;
        public editedWebPageType: string;
        public visitingStreetAddresses: Array<LocalizedText>;
        public visitingAddressPostalDistricts: Array<LocalizedText>;
        public visitingAddressPostalCode: string;
        public postalStreetAddressStreets: Array<LocalizedText>;
        public postalStreetAddressPostalDistricts: Array<LocalizedText>;
        public postalStreetAddressPostalCode: string;
        public postalPostOfficeBoxAddressPostOfficeBox: string;
        public postalPostOfficeBoxAddressPostalCode: string;
        public postalPostOfficeBoxAddressPostalDistricts: Array<LocalizedText>;
        public postalAddressTypes: PostalAddressTypes;
        public authorizationGroupName: string;
        public authorizationGroupRole: UserRole;
        public editedAuthorizationGroupName: string;
        public editedAuthorizationGroupRole: UserRole;

      


        constructor(public id?: string,
            public numericId?: number,
            public names?: Array<LocalizedText>,
            public businessId?: string,
            private descriptions?: Array<LocalizedText>,
            public oid?: string,
            public type?: string,
            public municipalityCode?: number,
            public validFromDate?: Date,
            public validToDate?: Date,
            public phoneNumber?: string,
            public phoneCallChargeType?: string,
            public phoneCallChargeInfos?: Array<LocalizedText>,
            public emailAddress?: string,
            public webPages?: Array<WebPage>,
            public visitingAddress?: StreetAddress,
            public visitingAddressQualifiers?: Array<LocalizedText>,
            public useVisitingAddressAsPostalAddress?: boolean,
            public postalStreetAddress?: StreetAddress,
            public postalPostOfficeBoxAddress?: PostOfficeBoxAddress,
            public homepageUrls?: Array<LocalizedText>,
            public nameAbbreviations?: Array<LocalizedText>,
            public isSubOrganization?: boolean,
            public canBeTransferredToFsc?: boolean,
            public canBeResponsibleDeptForService?: boolean,
            public authorizationGroups?: Array<AuthorizationGroup>)
        {
            this.initializeLocalizedNames(names);
            this.initializeLocalizedNameAbbreviations(nameAbbreviations);
            this.initializeLocalizedDescriptions(descriptions);
            this.initializeLocalizedHomepageUrls(homepageUrls);
            this.initializeLocalizedCallChargeInfos(phoneCallChargeInfos);
            this.setValidityTexts();
            this.initializeVisitingAddress(visitingAddress, visitingAddressQualifiers);
            this.initializePostalAddress(postalStreetAddress, postalPostOfficeBoxAddress);
            if (this.webPages == null)
            {
                this.webPages = new Array<WebPage>();
            }

            if (this.authorizationGroups == null)
            {
                this.authorizationGroups = new Array<AuthorizationGroup>();
            }
        }


        private initializeLocalizedCallChargeInfos(infos?: Array<LocalizedText>): void
        {
            this.phoneCallChargeInfos = this.setLocalizedTexts(infos, [""]);

            this.phoneCallChargeInfos.forEach((info) =>
            {
               
                info.localizedTitle = this.getPhoneCallChargeInfoLocalizedTitle(info.languageCode, info.localizedTitle);
            });
        }

        private getPhoneCallChargeInfoLocalizedTitle(languageCode: string, curTitle: string): string
        {

            // TODO: get these from api
            let title: string = "";
            switch (languageCode)
            {
                case "fi":
                    title = "esim. 1 €/puhelu + 0,90€/min + pvm/mpm";
                    break;
                case "sv":
                    title = "esim. 1 €/telefonsamtal + 0,90€/min + msa/lna";
                    break;
                case "en":
                    title = "esim. 1 €/call + 0,90€/min + mobile call charge / local network charge";
                    break;
            }

            return (curTitle + ": " + title);
        }

        private initializeLocalizedNames(names?: Array<LocalizedText>): void
        {
            this.names = this.setLocalizedTexts(names, ["fi"]);
        }

        private initializeLocalizedNameAbbreviations(nameAbbreviations?: Array<LocalizedText>): void
        {
            this.nameAbbreviations = this.setLocalizedTexts(nameAbbreviations,[""]);
        }

        private initializeLocalizedHomepageUrls(urls?: Array<LocalizedText>): void
        {
            this.homepageUrls = this.setLocalizedTexts(urls, [""]);
        }

        private initializeLocalizedDescriptions(descs?: Array<LocalizedText>): void
        {
            this.descriptions = this.setLocalizedTexts(descs, [""]);
            this.descriptionsAsHtml = new Array<LocalizedText>();
            this.descriptions.forEach((desc) =>
            {
                this.descriptionsAsHtml
                    .push(new LocalizedText(desc.languageCode,
                        desc.localizedValue != null
                        ? Affecto.HtmlContent.escapeAndReplaceNewLines(desc.localizedValue)
                        : ""));
            });
        }

        private setLocalizedTexts(texts: Array<LocalizedText>, requiredLangs: string[]): Array<LocalizedText>
        {
            let langs = DataLocalization.languageCodes;
            let localizedTexts = new Array<LocalizedText>();

            langs.forEach((item) =>
            {
                // init LocalizedText with existing value or create with empty value + set isrequired flag
                localizedTexts.push(new LocalizedText(String(item),
                    this.getLocalizedTextValue(texts, String(item)),
                    (requiredLangs.indexOf(String(item)) >= 0)));
            });

            return localizedTexts;
        }
    
        private getLocalizedTextValue(texts: Array<LocalizedText>, languageCode: string): string
        {
            if (texts != null && texts.some((arrVal: LocalizedText) => (languageCode === arrVal.languageCode)))
            {
                return (texts.filter((arrVal: LocalizedText) => (languageCode === arrVal.languageCode)))[0]
                    .localizedValue;
            }
            else
            {
                return "";
            }
        }

        private getLocalizedTextsWithValues(texts: Array<LocalizedText>)
        {
            var localizedTexts = new Array<LocalizedText>();
            texts.forEach((item) =>
            {
                if (item.localizedValue != null && item.localizedValue !== "")
                    localizedTexts.push(item);
            });
            return localizedTexts;
        }

        private setValidityTexts()
        {
            this.validFromText = Affecto.DateConverter.toFinnishDate(this.validFromDate);
            this.validToText = Affecto.DateConverter.toFinnishDate(this.validToDate);
            this.validFrom = Affecto.DateConverter.toISO8601Date(this.validFromDate);
            this.validTo = Affecto.DateConverter.toISO8601Date(this.validToDate);
        }

        private initializeVisitingAddress(visitingAddress?: StreetAddress,
            visitingAddressQualifiers?: Array<LocalizedText>): void
        {
            let streetAddresses = new Array<LocalizedText>();
            let postalDistricts = new Array<LocalizedText>();

            if (visitingAddress != null)
            {
                this.visitingAddressPostalCode = visitingAddress.postalCode;
                streetAddresses = visitingAddress.streetAddresses;
                postalDistricts = visitingAddress.postalDistricts;
            }
            this.visitingStreetAddresses = this.setLocalizedTexts(streetAddresses, ["fi"]);
            this.visitingAddressPostalDistricts = this.setLocalizedTexts(postalDistricts, ["fi"]);
            this.visitingAddressQualifiers = this.setLocalizedTexts(visitingAddressQualifiers, [""]);;
            
        }

        private initializePostalAddress(streetAddress?: StreetAddress, postOfficeBoxAddress?: PostOfficeBoxAddress):
        void
        {
            this.postalAddressTypes = new PostalAddressTypes();

            // postal street address
            let streetAddresses = new Array<LocalizedText>();
            let postalDistricts = new Array<LocalizedText>();
            if (streetAddress != null &&
                streetAddress.streetAddresses != null &&
                streetAddress.streetAddresses.length > 0 &&
                streetAddress.postalDistricts != null &&
                streetAddress.postalDistricts.length > 0)
            {
                this.postalStreetAddressPostalCode = streetAddress.postalCode;
                this.addPostalAddressType(PostalAddressType[PostalAddressType.SeparateStreetAddress]);
                streetAddresses = streetAddress.streetAddresses;
                postalDistricts = streetAddress.postalDistricts;
            }
            else
            {
                if (this.useVisitingAddressAsPostalAddress)
                {
                    this.addPostalAddressType(PostalAddressType[PostalAddressType.SameAsVisitingAddress]);
                }
                else
                {
                    this.setAvailablePostalStreetAddressTypes();
                }
            }
            this.postalStreetAddressStreets = this.setLocalizedTexts(streetAddresses, ["fi"]);
            this.postalStreetAddressPostalDistricts = this.setLocalizedTexts(postalDistricts, ["fi"]);

            // po-box address
            let postOfficeBoxPostalDistricts = new Array<LocalizedText>();
            if (postOfficeBoxAddress != null &&
                postOfficeBoxAddress.postalDistricts != null &&
                postOfficeBoxAddress.postalDistricts.length > 0)
            {
                this.postalPostOfficeBoxAddressPostOfficeBox = postOfficeBoxAddress.postOfficeBox;
                this.postalPostOfficeBoxAddressPostalCode = postOfficeBoxAddress.postalCode;
                postOfficeBoxPostalDistricts = postOfficeBoxAddress.postalDistricts;
                this.addPostalAddressType(PostalAddressType[PostalAddressType.PostOfficeBoxAddress]);
            }
            else
            {
                this.postalAddressTypes.available.setAvailable(PostalAddressType.PostOfficeBoxAddress);
            }

            this.postalPostOfficeBoxAddressPostalDistricts = this.setLocalizedTexts(postOfficeBoxPostalDistricts, ["fi"]);
        }

        

        public get effectivePostalStreetAddressStreets(): Array<LocalizedText>
        {
            if (this.useVisitingAddressAsPostalAddress)
            {
                return this.visitingStreetAddresses;
            }
            return this.postalStreetAddressStreets;
        }

        public get effectivePostalStreetAddressPostalCode(): string
        {
            if (this.useVisitingAddressAsPostalAddress)
            {
                return this.visitingAddressPostalCode;
            }
            return this.postalStreetAddressPostalCode;
        }

        public get effectivePostalStreetAddressPostalDistricts(): Array<LocalizedText>
        {
            if (this.useVisitingAddressAsPostalAddress)
            {
                return this.visitingAddressPostalDistricts;
            }
            return this.postalStreetAddressPostalDistricts;
        }

        public isMunicipality(): boolean
        {
            // TODO: make value configurable
            return this.type === "Kunta";
        }

        public isMunincipalSubOrganization(): boolean
        {
            // TODO: make values configurable
            return (this.type === "Kunta" ||
                    this.type === "Kunnan liikelaitos" ||
                    this.type === "Kunnan konserniyhteisö") && this.isSubOrganization;
        }

        public get typeProperty(): string {
            return this.type;
        }

        public set typeProperty(value: string) {
            this.type = value;
            if (!this.isMunicipality()) {
                this.municipalityCode = null;
            }

            if (!this.isMunincipalSubOrganization()) {
                this.canBeResponsibleDeptForService = false;
            }
        }

        public initializeLocalizedTexts(): void
        {
            this.initializeLocalizedNames(this.names);
            this.initializeLocalizedNameAbbreviations(this.nameAbbreviations);
            this.initializeLocalizedDescriptions(this.descriptions);
            this.initializeLocalizedHomepageUrls(this.homepageUrls);
            this.initializeLocalizedCallChargeInfos(this.phoneCallChargeInfos);
            this.initializeVisitingAddress(this.visitingAddress,this.visitingAddressQualifiers);
        }

        public generateBasicInformationLocalizedAndFormattedTexts(): void
        {
            this.names = this.getLocalizedTextsWithValues(this.names);
            this.nameAbbreviations = this.getLocalizedTextsWithValues(this.nameAbbreviations);
            this.descriptions = this.getLocalizedTextsWithValues(this.descriptions);
            this.descriptions.forEach((desc) =>
            {
                this.descriptionsAsHtml = new Array<LocalizedText>();
                this.descriptionsAsHtml
                    .push(new LocalizedText(desc.languageCode,
                        desc.localizedValue != null
                        ? Affecto.HtmlContent.escapeAndReplaceNewLines(desc.localizedValue)
                        : ""));
            });

          

            this.setValidityTexts();
        }

        public generateContactinformationLocalizedTexts(): void
        {
            var urls = this.getLocalizedTextsWithValues(this.homepageUrls);
            urls.forEach((url,i) =>
            {
                urls[i].localizedValue = url.localizedValue.indexOf("http") === 0 ? url.localizedValue : "http://" + url.localizedValue;   
            });
            this.homepageUrls = urls;

            this.phoneCallChargeInfos = this.getLocalizedTextsWithValues(this.phoneCallChargeInfos);
        }

       
        public generateVisitingAddressLocalizedTexts(): void
        {
            this.visitingAddress = new StreetAddress(this.getLocalizedTextsWithValues(this.visitingStreetAddresses),
                this.visitingAddressPostalCode,
                this.getLocalizedTextsWithValues(this.visitingAddressPostalDistricts));

            this.visitingAddressQualifiers = this.getLocalizedTextsWithValues(this.visitingAddressQualifiers);
        }

        public generatePostalAddressLocalizedTexts(): void
        {
            this.postalStreetAddress = new StreetAddress(this
                .getLocalizedTextsWithValues(this.postalStreetAddressStreets),
                this.postalStreetAddressPostalCode,
                this.getLocalizedTextsWithValues(this.postalStreetAddressPostalDistricts));

            this.postalPostOfficeBoxAddress = new PostOfficeBoxAddress(this.postalPostOfficeBoxAddressPostOfficeBox,
                this.postalPostOfficeBoxAddressPostalCode, this.getLocalizedTextsWithValues(this.postalPostOfficeBoxAddressPostalDistricts));
              
        }

        public hasMunicipalityCode(): boolean
        {
            return this.municipalityCode != null;
        }

        public hasBusinessId(): boolean
        {
            return this.businessId != null && this.businessId !== "";
        }

        public hasPhoneNumber(): boolean
        {
            return this.phoneNumber != null && this.phoneNumber !== "";
        }

        public hasPhoneCallChargeType(): boolean
        {
            return this.phoneCallChargeType != null && this.phoneCallChargeType !== "";
        }

        public hasPhoneCallChargeInfos(): boolean
        {
            return this.phoneCallChargeInfos
                .some((arrVal: LocalizedText) => (arrVal.localizedValue != null && arrVal.localizedValue !== ""));
        }

        public hasEmailAddress(): boolean
        {
            return this.emailAddress != null && this.emailAddress !== "";
        }

        public hasWebPage(): boolean
        {
            return this.hasWebPageUrl() &&
                this.webPageName != null &&
                this.webPageName !== "" &&
                this.webPageType != null &&
                this.webPageType !== "";
        }

        public hasWebPageUrl(): boolean
        {
            return this.webPageUrl != null && this.webPageUrl !== "";
        }

        public hasHomepageUrls(): boolean
        {
            return this.homepageUrls
                .some((arrVal: LocalizedText) => (arrVal.localizedValue != null && arrVal.localizedValue !== ""));
        }

        public hasEditedWebPage(): boolean
        {
            return this.hasEditedWebPageUrl() &&
                this.editedWebPageName != null &&
                this.editedWebPageName !== "" &&
                this.editedWebPageType != null &&
                this.editedWebPageType !== "";
        }


        public hasEditedWebPageUrl(): boolean
        {
            return this.editedWebPageUrl != null && this.editedWebPageUrl !== "";
        }


        public hasEditedAuthorizationGroup(): boolean {
            return this.editedAuthorizationGroupName != null &&
                this.editedAuthorizationGroupName !== "" &&
                this.editedAuthorizationGroupRole != null;
        }

        public hasContactInformation(): boolean
        {
            return this
                .hasPhoneNumber() ||
                this.hasEmailAddress() ||
                this.webPages.length > 0 ||
                this.hasPhoneCallChargeType() ||
                this.hasPhoneCallChargeInfos() ||
                this.hasHomepageUrls();
        }

        public hasVisitingAddressPostalCode(): boolean
        {
            return this.visitingAddressPostalCode != null && this.visitingAddressPostalCode !== "";
        }

        public hasPostalStreetAddressPostalCode(): boolean
        {
            return this.postalStreetAddressPostalCode != null && this.postalStreetAddressPostalCode !== "";
        }

        public hasPostalPostOfficeBoxAddressPostalCode(): boolean
        {
            return this
                .postalPostOfficeBoxAddressPostalCode !=
                null &&
                this.postalPostOfficeBoxAddressPostalCode !== "";
        }

        
        public hasVisitingAddressParts(): boolean
        {
            return (this.visitingAddressPostalCode != null && this.visitingAddressPostalCode !== "" ||
                (this.visitingStreetAddresses != null  && this.visitingStreetAddresses.some((arrVal: LocalizedText) => arrVal.localizedValue != null && arrVal.localizedValue !== ""))||
                (this.visitingAddressPostalDistricts != null && this.visitingAddressPostalDistricts.some((arrVal: LocalizedText) => arrVal.localizedValue !== null && arrVal.localizedValue !== "")));

        }


        public hasAuthorizationGroup(): boolean
        {
            return this.authorizationGroupName != null &&
                this.authorizationGroupName !== "" &&
                this.authorizationGroupRole != null;
        }

        public authorizationGroupExists(): boolean
        {
            for (let i = 0; i < this.authorizationGroups.length; i++)
            {
                if (this.authorizationGroups[i].name.toLocaleUpperCase() === this.authorizationGroupName.toLocaleUpperCase())
                {
                    return true;
                }
            }
            return false;
        }

        public addWebPage(name: string, url: string, pageType: string): void
        {
            if (name != null && name !== "" && url != null && url !== "" && pageType != null && pageType !== "" && !this.containsWebPageUrl(url))
            {
                this.webPages.push(new WebPage(name, url, pageType));
            }
        }

        public removeWebPage(url: string): void
        {
            for (var i = 0; i < this.webPages.length; i++)
            {
                if (this.webPages[i].address === url)
                {
                    this.webPages.splice(i, 1);
                    break;
                }
            }
        }



        public addAuthorizationGroup(groupName: string, groupRoleId: string, groupRoleName: string): void
        {
            if (groupName != null && groupName !== "" && groupRoleId != null && groupRoleId !== "" && !this.containsAuthorazionGroupName(groupName)) {
                this.authorizationGroups.push(new AuthorizationGroup(groupName, groupRoleId, groupRoleName));
            }
        }
      
      
        public removeAuthorizationGroup(groupName: string): void
        {
            for (var i = 0; i < this.authorizationGroups.length; i++) {
                if (this.authorizationGroups[i].name === groupName) {
                    this.authorizationGroups.splice(i, 1);
                    break;
                }
            }
        }


        public isAdded(): boolean
        {
            return this.id != null;
        }

        public clearPostalPostOfficeBoxAddress(): void
        {
            this.postalPostOfficeBoxAddressPostOfficeBox = null;
            this.postalPostOfficeBoxAddressPostalCode = null;
            this.postalPostOfficeBoxAddressPostalDistricts = this.setLocalizedTexts(new Array<LocalizedText>(), ["fi"]);
        }

        public clearPostalStreetAddress(): void
        {
            this.postalStreetAddressStreets = this.setLocalizedTexts(new Array<LocalizedText>(), ["fi"]);
            this.postalStreetAddressPostalCode = null;
            this.postalStreetAddressPostalDistricts = this.setLocalizedTexts(new Array<LocalizedText>(), ["fi"]);
        }

        private containsWebPageUrl(url: string): boolean
        {
            return this.webPages.length > 0 && !this.webPages.every((item: WebPage) =>
            {
                return item.address !== url;
            });
        }


        private containsAuthorazionGroupName(name: string): boolean {
            return this.authorizationGroups.length > 0 && !this.authorizationGroups.every((group: AuthorizationGroup) =>
            {
                return group.name !== name;
            });
        }

        public addPostalAddressType(addressType: string): void
        {
            this.postalAddressTypes.add(addressType);
            if (PostalAddressType[PostalAddressType.SameAsVisitingAddress] === addressType)
            {
                this.useVisitingAddressAsPostalAddress = true;
            }
        }

        public removePostOfficeBoxPostalAddress(): void
        {
            this.clearPostalPostOfficeBoxAddress();
            this.postalAddressTypes.remove(PostalAddressType.PostOfficeBoxAddress);
            this.postalAddressTypes.available.setAvailable(PostalAddressType.PostOfficeBoxAddress);
        }

        public removeSeparateStreetPostalAddress(): void
        {
            this.clearPostalStreetAddress();
            this.postalAddressTypes.remove(PostalAddressType.SeparateStreetAddress);
            this.setAvailablePostalStreetAddressTypes();
        }

        public removeSameAsVisitingAddressPostalAddress(): void
        {
            this.useVisitingAddressAsPostalAddress = false;
            this.postalAddressTypes.remove(PostalAddressType.SameAsVisitingAddress);
            this.setAvailablePostalStreetAddressTypes();
        }

        public setAvailablePostalStreetAddressTypes(): void
        {
            this.postalAddressTypes.setSameAsVisitingAddressPostalAddressAvailability(this.hasVisitingAddressParts());
            this.postalAddressTypes.setSeparateStreetAddressPostalAddressAvailability();
        }

        public canAddPostalAddress(): boolean
        {
            return this.postalAddressTypes.canAddPostalAddress();
        }

        public isValidValidity(): boolean
        {
            return this.validFromDate == null || this.validToDate == null || this.validFromDate <= this.validToDate;
        }

       
    }
}