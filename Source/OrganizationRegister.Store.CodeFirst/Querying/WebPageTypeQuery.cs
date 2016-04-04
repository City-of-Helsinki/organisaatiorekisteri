using System;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class WebPageTypeQuery
    {
        private readonly IQueryable<WebPageType> webPageTypes;

        public WebPageTypeQuery(IQueryable<WebPageType> webPageTypes)
        {
            if (webPageTypes == null)
            {
                throw new ArgumentNullException("webPageTypes");
            }
            this.webPageTypes = webPageTypes;
        }

        public WebPageType Execute(string type)
        {
            try
            {
                return webPageTypes.Single(t => t.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("No or more than one web page types '{0}' found.", type), e);
            }
        }

        public WebPageType Execute(Guid id)
        {
            try
            {
                return webPageTypes.Single(t => t.Id.Equals(id));
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("No or more than one web page types with id '{0}' found.", id), e);
            }
        }
    }
}
