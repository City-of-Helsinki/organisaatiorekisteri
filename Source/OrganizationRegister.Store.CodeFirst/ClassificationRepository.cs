using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Application.Classification;
using OrganizationRegister.Store.CodeFirst.Model;
using OrganizationRegister.Store.CodeFirst.Querying;
using IClass = OrganizationRegister.Application.Classification.IClass;

namespace OrganizationRegister.Store.CodeFirst
{
    internal class ClassificationRepository : IClassificationRepository
    {
        private readonly IStoreContext context;

        public ClassificationRepository(IStoreContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.context = context;
        }

        public IReadOnlyCollection<IHierarchicalClass> GetLifeEventHierarchy()
        {
            return ClassHierarchyBuilder.CreateOrderedClassHierarchy(context.LifeEvents);
        }

        public IReadOnlyCollection<IHierarchicalClass> GetServiceClassHierarchy()
        {
            return ClassHierarchyBuilder.CreateOrderedClassHierarchy(context.ServiceClasses);
        }

        public IReadOnlyCollection<IHierarchicalClass> GetOntologyTermHierarchy()
        {
            return ClassHierarchyBuilder.CreateOrderedClassHierarchy(context.OntologyTerms);
        }

        public IReadOnlyCollection<IHierarchicalClass> GetTargetGroupHierarchy()
        {
            return ClassHierarchyBuilder.CreateOrderedClassHierarchy(context.TargetGroups);
        }

        public IReadOnlyCollection<IClass> GetFlatOntologyTerms(string searchText)
        {
            IList<OntologyTerm> terms = GetDatabaseOntologyTerms(searchText);
            IEnumerable<OntologyTerm> termsStartingWithFoundText = FilterTermsStartingWithText(searchText, terms);
            IEnumerable<OntologyTerm> termsNotStartingWithFoundText = FilterTermsNotStartingWithText(searchText, terms);
            return termsStartingWithFoundText.Concat(termsNotStartingWithFoundText).Select(term => ClassFactory.CreateClass(term.Id, term.Name)).ToList();
        }

        public IReadOnlyCollection<IClass> GetFlatOntologyTerms(string searchText, int maxResults)
        {
            if (maxResults <= 0)
            {
                throw new ArgumentException(string.Format("Maximun result count '{0}' must be more than zero.", maxResults));
            }

            IList<OntologyTerm> terms = GetDatabaseOntologyTerms(searchText);
            IEnumerable<OntologyTerm> termsStartingWithFoundText = FilterTermsStartingWithText(searchText, terms);
            if (termsStartingWithFoundText.Count() >= maxResults)
            {
                return termsStartingWithFoundText.Take(maxResults).Select(term => ClassFactory.CreateClass(term.Id, term.Name)).ToList();
            }

            IEnumerable<OntologyTerm> termsNotStartingWithFoundText = FilterTermsNotStartingWithText(searchText, terms);
            return termsStartingWithFoundText.Concat(termsNotStartingWithFoundText).Take(maxResults).Select(term => ClassFactory.CreateClass(term.Id, term.Name)).ToList();
        }

        private static IOrderedEnumerable<OntologyTerm> FilterTermsNotStartingWithText(string searchText, IList<OntologyTerm> terms)
        {
            return terms.Where(term => !term.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)).OrderBy(term => term.LowerCaseName);
        }

        private static IOrderedEnumerable<OntologyTerm> FilterTermsStartingWithText(string searchText, IList<OntologyTerm> terms)
        {
            return terms.Where(term => term.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)).OrderBy(term => term.LowerCaseName);
        }

        private IList<OntologyTerm> GetDatabaseOntologyTerms(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText) || searchText.Length < 2)
            {
                return new List<OntologyTerm>();
            }

            var query = new OntologyTermQuery(context.OntologyTerms);
            return query.Execute(searchText).ToList();
        }
    }
}
