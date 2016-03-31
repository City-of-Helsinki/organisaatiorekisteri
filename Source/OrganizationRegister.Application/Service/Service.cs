using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Service
{
    internal class Service : IService
    {
        private readonly LocalizedTextsContainer localizedTextsContainer;
        private IEnumerable<Language> languages;
        private IEnumerable<IHierarchicalClass> serviceClasses;
        private IEnumerable<IClass> ontologyTerms;
        private IEnumerable<IHierarchicalClass> targetGroups;
        private IEnumerable<IHierarchicalClass> lifeEvents;

        public Service(Guid id, long numericId, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions, IEnumerable<LocalizedText> shortDescriptions,
            IEnumerable<Language> languages, IEnumerable<string> dataLanguageCodes)
            : this(id, names, descriptions, shortDescriptions, languages, dataLanguageCodes)
        {
            NumericId = numericId;
        }

        public Service(Guid id, IEnumerable<LocalizedText> names, IEnumerable<LocalizedText> descriptions, IEnumerable<LocalizedText> shortDescriptions, 
            IEnumerable<Language> languages, IEnumerable<string> dataLanguageCodes)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Service must have an identifier.", "id");
            }
            Id = id;
            localizedTextsContainer = new LocalizedTextsContainer(dataLanguageCodes);
            Names = names;
            Descriptions = descriptions;
            ShortDescriptions = shortDescriptions;
            Languages = languages;
        }

        public Guid Id { get; private set; }
        public long NumericId { get; private set; }

        public IEnumerable<LocalizedText> Names
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.Name); }
            set { localizedTextsContainer.Set(LocalizedProperty.Name, new MandatoryLocalizedSingleTexts(value)); }
        }

        public IEnumerable<LocalizedText> Descriptions
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.Description); }
            set { localizedTextsContainer.Set(LocalizedProperty.Description, new MandatoryLocalizedSingleTexts(value)); }
        }

        public IEnumerable<LocalizedText> ShortDescriptions
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.ShortDescription); }
            set { localizedTextsContainer.Set(LocalizedProperty.ShortDescription, new MandatoryLocalizedSingleTexts(value)); }
        }

        public IEnumerable<LocalizedText> AlternateNames
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.AlternateName); }
            set { localizedTextsContainer.Set(LocalizedProperty.AlternateName, new LocalizedSingleTexts(value)); }
        }

        public IEnumerable<LocalizedText> UserInstructions
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.UserInstruction); }
            set { localizedTextsContainer.Set(LocalizedProperty.UserInstruction, new LocalizedSingleTexts(value)); }
        }

        public IEnumerable<LocalizedText> Requirements
        {
            get { return localizedTextsContainer.GetTexts(LocalizedProperty.Requirement); }
            set { localizedTextsContainer.Set(LocalizedProperty.Requirement, new LocalizedSingleTexts(value)); }
        }

        public string GetAlternateName(string languageCode)
        {
            LocalizedSingleTexts names = (LocalizedSingleTexts) localizedTextsContainer.GetTexts(LocalizedProperty.AlternateName);
            return names.GetValue(languageCode);
        }

        public string GetDescription(string languageCode)
        {
            LocalizedSingleTexts descriptions = (LocalizedSingleTexts) localizedTextsContainer.GetTexts(LocalizedProperty.Description);
            return descriptions.GetValue(languageCode);
        }

        public string GetShortDescription(string languageCode)
        {
            LocalizedSingleTexts descriptions = (LocalizedSingleTexts) localizedTextsContainer.GetTexts(LocalizedProperty.ShortDescription);
            return descriptions.GetValue(languageCode);
        }

        public string GetUserInstructions(string languageCode)
        {
            LocalizedSingleTexts instructions = (LocalizedSingleTexts) localizedTextsContainer.GetTexts(LocalizedProperty.UserInstruction);
            return instructions.GetValue(languageCode);
        }

        public string GetRequirements(string languageCode)
        {
            LocalizedSingleTexts requirements = (LocalizedSingleTexts) localizedTextsContainer.GetTexts(LocalizedProperty.Requirement);
            return requirements.GetValue(languageCode);
        }

        public IEnumerable<Language> Languages
        {
            get { return languages; }
            set
            {
                if (value == null || !value.Any())
                {
                    throw new ArgumentException("There must be at least one language code.");
                }
                languages = value.ToList();
            }
        }

        public IEnumerable<string> LanguagesCodes
        {
            get { return Languages.Select(l => l.Code); }
        }

        public IEnumerable<IHierarchicalClass> ServiceClasses
        {
            get { return serviceClasses; }
            set { serviceClasses = value.ToList(); }

        }

        public IEnumerable<IClass> OntologyTerms
        {
            get { return ontologyTerms; }
            set { ontologyTerms = value.OrderBy(term => term.Name).ToList(); }

        }

        public IEnumerable<IHierarchicalClass> TargetGroups
        {
            get { return targetGroups; }
            set { targetGroups = value.ToList(); }

        }

        public IEnumerable<IHierarchicalClass> LifeEvents
        {
            get { return lifeEvents; }
            set { lifeEvents = value.ToList(); }

        }

        public IEnumerable<LocalizedText> Keywords
        {
            get { return GroupByLanguageOrderAlphabeticallyAndJoinWithCommas(localizedTextsContainer.GetTexts(LocalizedProperty.Keyword)); }
            set { localizedTextsContainer.Set(LocalizedProperty.Keyword, new LocalizedTexts(value)); }
        }

        private static IEnumerable<LocalizedText> GroupByLanguageOrderAlphabeticallyAndJoinWithCommas(LocalizedTexts texts)
        {
            return texts.GroupBy(text => text.LanguageCode)
                .Select(group => new LocalizedText(group.Key, string.Join(", ", group.Select(text => text.LocalizedValue).OrderBy(text => text))));
        }
    }
}
