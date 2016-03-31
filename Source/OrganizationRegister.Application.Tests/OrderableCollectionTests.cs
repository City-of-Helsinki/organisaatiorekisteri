using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationRegister.Application.Settings;

namespace OrganizationRegister.Application.Tests
{
    [TestClass]
    public class OrderableCollectionTests
    {
        private OrderableCollection<Language> sut;

        [TestMethod]
        public void OrderingEmptyCollection()
        {
            sut = new OrderableCollection<Language>(new List<Language>());

            IEnumerable<IOrderable> result = sut.Order();

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void OrderingCollectionWhereAllItemsHaveOrderNumbers()
        {
            sut = new OrderableCollection<Language>(new List<Language> { CreateLanguage(2), CreateLanguage(1) });

            IReadOnlyCollection<IOrderable> result = sut.Order().ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result.First().OrderNumber);
            Assert.AreEqual(2, result.Last().OrderNumber);
        }

        [TestMethod]
        public void OrderingCollectionWhereNoItemHasOrderNumber()
        {
            sut = new OrderableCollection<Language>(new List<Language> { CreateLanguage("d"), CreateLanguage("a") });

            IReadOnlyCollection<IOrderable> result = sut.Order().ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("a", result.First().Name);
            Assert.AreEqual("d", result.Last().Name);
        }

        private static Language CreateLanguage(string name)
        {
            return new Language("fi", name, null);
        }

        private static Language CreateLanguage(int orderNumber)
        {
            return new Language("fi", "suomi", orderNumber);
        }
    }
}
