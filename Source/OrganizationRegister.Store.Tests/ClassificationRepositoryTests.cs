using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Store.CodeFirst;

namespace OrganizationRegister.Store.Tests
{
    [TestClass]
    public class ClassificationRepositoryTests
    {
        private ClassificationRepository sut;
        private IStoreContext context;

        [TestInitialize]
        public void Setup()
        {
            context = Substitute.For<IStoreContext>();
            sut = new ClassificationRepository(context);
        }

        [TestMethod]
        public void GetFlatOntolotyTermsWithNullSearchText()
        {
            Assert.IsFalse(sut.GetFlatOntologyTerms(null).Any());
        }

        [TestMethod]
        public void GetFlatOntolotyTermsWithEmptySearchText()
        {
            Assert.IsFalse(sut.GetFlatOntologyTerms(string.Empty).Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFlatOntologyTermsWithZeroMaxResults()
        {
            sut.GetFlatOntologyTerms("text", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFlatOntologyTermsWithNegativeMaxResults()
        {
            sut.GetFlatOntologyTerms("text", -2);
        }
    }
}
