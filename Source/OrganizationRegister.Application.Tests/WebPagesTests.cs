using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Common;

namespace OrganizationRegister.Application.Tests
{
    [TestClass]
    public class WebPagesTests
    {
        private WebPages sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PageNamesMustBeDistinct()
        {
            const string pageName = "site";
            sut = new WebPages(new List<WebPage> { new WebPage(pageName, "www.google.com", "type"), new WebPage(pageName, "www.google.fí", "type") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PageAddressesMustBeDistinct()
        {
            const string pageAddress = "www.google.fi";
            sut = new WebPages(new List<WebPage> { new WebPage("site1", pageAddress, "type"), new WebPage("site2", pageAddress, "type") });
        }

        [TestMethod]
        public void EmptyWebSitesCollection()
        {
            sut = new WebPages(Enumerable.Empty<WebPage>());

            Assert.IsFalse(Enumerable.Any<WebPage>(sut));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyWebSiteNameIsNotAllowed()
        {
            sut = new WebPages(new List<WebPage> { new WebPage(string.Empty, "www.google.fi", "type") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullWebSiteNameIsNotAllowed()
        {
            sut = new WebPages(new List<WebPage> { new WebPage(null, "www.google.fi", "type") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyWebPageAddressIsNotAllowed()
        {
            sut = new WebPages(new List<WebPage> { new WebPage("site", string.Empty, "type") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullWebPageAddressIsNotAllowed()
        {
            sut = new WebPages(new List<WebPage> { new WebPage("site", null, "type") });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyWebPageTypeIsNotAllowed()
        {
            sut = new WebPages(new List<WebPage> { new WebPage("site", "www.google.fi", string.Empty) });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullWebPageTypeIsNotAllowed()
        {
            sut = new WebPages(new List<WebPage> { new WebPage("site", "www.google.fi", null) });
        }
    }
}
