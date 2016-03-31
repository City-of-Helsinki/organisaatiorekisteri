using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.Patterns.Cqrs;
using OrganizationRegister.Commanding.Service.Commands;
using OrganizationRegister.Common;
using OrganizationRegister.Store.CodeFirst;
using OrganizationRegister.Store.CodeFirst.Querying;

namespace OrganizationRegister.Commanding.Service.CommandHandlers
{
    internal class SetServiceClassificationHandler : ICommandHandler<SetServiceClassification>
    {
        private readonly IStoreContext dbContext;

        public SetServiceClassificationHandler(IStoreContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            this.dbContext = dbContext;
        }

        public void Execute(SetServiceClassification command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (command.ServiceClasses == null || !command.ServiceClasses.Any())
            {
                throw new ArgumentException("Service service classes must be given.");
            }
            if (command.ServiceClasses.Count() != command.ServiceClasses.Distinct().Count())
            {
                throw new ArgumentException("Service service classes must be distinct.");
            }
            if (command.OntologyTerms == null || !command.OntologyTerms.Any())
            {
                throw new ArgumentException("Service ontology terms must be given.");
            }
            if (command.OntologyTerms.Count() != command.OntologyTerms.Distinct().Count())
            {
                throw new ArgumentException("Service ontology terms must be distinct.");
            }
            if (command.TargetGroups == null || !command.TargetGroups.Any())
            {
                throw new ArgumentException("Service target groups must be given.");
            }
            if (command.TargetGroups.Count() != command.TargetGroups.Distinct().Count())
            {
                throw new ArgumentException("Service target groups must be distinct.");
            }

            var query = new ServiceQuery(dbContext.Services);
            Store.CodeFirst.Model.Service service = query.Execute(command.OrganizationId, command.ServiceId);

            service.SetServiceClasses(command.ServiceClasses, dbContext);
            service.SetLifeEvents(command.LifeEvents, dbContext);
            service.SetOntologyTerms(command.OntologyTerms, dbContext);
            service.SetTargetGroups(command.TargetGroups, dbContext);
            service.SetKeywords(SplitKeywordTexts(command.Keywords), dbContext);

            dbContext.SaveChanges();
        }

        private static IEnumerable<LocalizedText> SplitKeywordTexts(IEnumerable<LocalizedText> keywordTexts)
        {
            if (keywordTexts == null)
            {
                return null;
            }

            return (from keywordText in keywordTexts
                    let words = keywordText.LocalizedValue.Split(',')
                    from word in words.Where(word => !string.IsNullOrEmpty(word))
                    select new LocalizedText
                    {
                        LocalizedValue = word.Trim(),
                        LanguageCode = keywordText.LanguageCode
                    }).ToList();
        }
    }
}
