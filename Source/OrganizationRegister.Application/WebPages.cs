using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Affecto.Identifiers;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application
{
    internal class WebPages : IEnumerable<WebPage>
    {
        private readonly List<WebPage> pages;

        public WebPages(IEnumerable<WebPage> pages)
        {
            if (pages == null)
            {
                throw new ArgumentNullException("pages");
            }
            List<WebPage> webSites = pages.ToList();
            if (webSites.Count != webSites.Select(site => site.Name).Distinct().Count())
            {
                throw new ArgumentException("Two or more web pages had the same name.", "pages");
            }
            if (webSites.Count != webSites.Select(site => site.Address).Distinct().Count())
            {
                throw new ArgumentException("Two or more web pages had the same address.", "pages");
            }
            if (webSites.Any(s => string.IsNullOrWhiteSpace(s.Name) || string.IsNullOrWhiteSpace(s.Address) || string.IsNullOrWhiteSpace(s.Type)))
            {
                throw new ArgumentException("A field of WebPage was null or empty.", "pages");
            }

            WebAddressSpecification specification = new WebAddressSpecification();
            foreach (WebPage site in webSites)
            {
                if (!specification.IsSatisfiedBy(site.Address))
                {
                    throw new ArgumentException(string.Format("Invalid url ({0}).", site.Address), "pages");
                }
            }
            this.pages = webSites;
        }

        public WebPages()
        {
            pages = new List<WebPage>();
        }

        public void Clear()
        {
            pages.Clear();
        }

        public IEnumerator<WebPage> GetEnumerator()
        {
            return pages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
