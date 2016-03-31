"use strict";

module OrganizationRegister
{
    export class Service implements Affecto.Base.IModel
    {
        public languageCodes: Array<string>;
        public languageCollection: Languages;
        public name: string;
        public alternateName: string;
        public description: string;
        public descriptionAsHtml: string;
        public shortDescription: string;
        public shortDescriptionAsHtml: string;
        public userInstruction: string;
        public userInstructionAsHtml: string;
        public requirement: string;
        public requirementAsHtml: string;
        public serviceClasses: Array<string>;
        public lifeEvents: Array<string>;
        public targetGroups: Array<string>;
        public keywordList: string;

        constructor(public id?: string, public numericId?: number, public names?: Array<LocalizedText>, public alternateNames?: Array<LocalizedText>,
            public descriptions?: Array<LocalizedText>, public shortDescriptions?: Array<LocalizedText>, public userInstructions?: Array<LocalizedText>,
            public requirements?: Array<LocalizedText>, languages?: Array<Language>, serviceClasses?: Array<Hierarchical>, public ontologyTerms?: Array<Class>,
            targetGroups?: Array<Hierarchical>, lifeEvents?: Array<Hierarchical>, public keywords?: Array<LocalizedText>)
        {
            if (this.hasItems(names))
            {
                this.name = names[0].localizedValue;
            }
            if (this.hasItems(descriptions))
            {
                this.description = descriptions[0].localizedValue;
                this.descriptionAsHtml = Affecto.HtmlContent.escapeAndReplaceNewLines(this.description);
            }
            if (this.hasItems(alternateNames))
            {
                this.alternateName = alternateNames[0].localizedValue;
            }
            if (this.hasItems(shortDescriptions))
            {
                this.shortDescription = shortDescriptions[0].localizedValue;
                this.shortDescriptionAsHtml = Affecto.HtmlContent.escapeAndReplaceNewLines(this.shortDescription);
            }
            if (this.hasItems(userInstructions))
            {
                this.userInstruction = userInstructions[0].localizedValue;
                this.userInstructionAsHtml = Affecto.HtmlContent.escapeAndReplaceNewLines(this.userInstruction);
            }
            if (this.hasItems(requirements))
            {
                this.requirement = requirements[0].localizedValue;
                this.requirementAsHtml = Affecto.HtmlContent.escapeAndReplaceNewLines(this.requirement);
            }
            this.languagesProperty = languages;
            if (this.hasItems(keywords))
            {
                this.keywordList = keywords[0].localizedValue;
            }
            this.initializeClassificationCollections(serviceClasses, targetGroups, lifeEvents);
        }

        public generateBasicInformationLocalizedAndFormattedTexts(): void
        {
            if (this.name != null)
            {
                this.names = new Array<LocalizedText>(new LocalizedText("fi", this.name));
            }
            if (this.alternateName != null)
            {
                this.alternateNames = new Array<LocalizedText>(new LocalizedText("fi", this.alternateName));
            }
            if (this.description != null)
            {
                this.descriptions = new Array<LocalizedText>(new LocalizedText("fi", this.description));
            }
            if (this.shortDescription != null)
            {
                this.shortDescriptions = new Array<LocalizedText>(new LocalizedText("fi", this.shortDescription));
            }
            if (this.userInstruction != null)
            {
                this.userInstructions = new Array<LocalizedText>(new LocalizedText("fi", this.userInstruction));
            }
            if (this.requirement != null)
            {
                this.requirements = new Array<LocalizedText>(new LocalizedText("fi", this.requirement));
            }
            if (this.description != null)
            {
                this.descriptionAsHtml = Affecto.HtmlContent.escapeAndReplaceNewLines(this.description);
            }
            if (this.shortDescription != null)
            {
                this.shortDescriptionAsHtml = Affecto.HtmlContent.escapeAndReplaceNewLines(this.shortDescription);
            }
            if (this.userInstruction != null)
            {
                this.userInstructionAsHtml = Affecto.HtmlContent.escapeAndReplaceNewLines(this.userInstruction);
            }
            if (this.requirement != null)
            {
                this.requirementAsHtml = Affecto.HtmlContent.escapeAndReplaceNewLines(this.requirement);
            }
        }

        public generateKeywordsLocalizedTexts(): void
        {
            if (this.keywordList != null)
            {
                this.keywords = new Array<LocalizedText>(new LocalizedText("fi", this.keywordList));
            }
        }

        public get languageNameList(): string
        {
            return this.languageCollection.commaSeparatedNames;
        }

        public get languagesProperty(): Array<Language>
        {
            if (this.languageCollection == null)
            {
                return new Array<Language>();
            }
            return this.languageCollection.value;
        }

        public set languagesProperty(value: Array<Language>)
        {
            if (this.languageCollection == null)
            {
                this.languageCollection = new Languages(value);
            }
            else
            {
                this.languageCollection.value = value;
            }
            this.languageCodes = this.languageCollection.codes;
        }

        public isAdded(): boolean
        {
            return this.id != null;
        }

        public setClassification(serviceClasses: Tree, targetGroups: Tree, lifeEvents: Tree): void
        {
            if (serviceClasses == null)
            {
                this.serviceClasses = new Array<string>();
            }
            else
            {
                this.serviceClasses = serviceClasses.getIds();
            }

            if (targetGroups == null)
            {
                this.targetGroups = new Array<string>();
            }
            else
            {
                this.targetGroups = targetGroups.getIds();
            }

            if (lifeEvents == null)
            {
                this.lifeEvents = new Array<string>();
            }
            else
            {
                this.lifeEvents = lifeEvents.getIds();
            }
        }

        private hasItems(array: Array<any>): boolean
        {
            return array != null && array.length > 0;
        }

        private initializeClassificationCollections(serviceClasses: Array<Hierarchical>, targetGroups: Array<Hierarchical>, lifeEvents: Array<Hierarchical>): void
        {
            this.serviceClasses = new Array<string>();
            this.targetGroups = new Array<string>();
            this.lifeEvents = new Array<string>();
            this.setClassification(new Tree(serviceClasses), new Tree(targetGroups), new Tree(lifeEvents));
        }
    }
}