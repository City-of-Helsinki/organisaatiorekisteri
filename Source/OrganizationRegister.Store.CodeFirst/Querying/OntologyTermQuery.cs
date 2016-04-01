using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class OntologyTermQuery
    {
        private readonly IQueryable<OntologyTerm> ontologyTerms;
        public OntologyTermQuery(IQueryable<OntologyTerm> ontologyTerms)
        {
            if (ontologyTerms == null)
            {
                throw new ArgumentNullException("ontologyTerms");
            }
            this.ontologyTerms = ontologyTerms;
        }

        public IEnumerable<OntologyTerm> Execute(string partOfName)
        {
            string lowerCasePartOfName = partOfName.ToLower();
            return ontologyTerms.Where(term => term.LowerCaseName.Contains(lowerCasePartOfName));
        }
    }
}
