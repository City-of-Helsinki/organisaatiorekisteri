using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Classification;
using OrganizationRegister.Application.Classification;

namespace OrganizationRegister.Api.Tests.Classification
{
    [TestClass]
    public class ClassMapperTests
    {
        private ClassMapper sut;
        private IClass source;
        private Class destination;

        [TestInitialize]
        public void Setup()
        {
            sut = new ClassMapper();
            source = Substitute.For<IClass>();
        }

        [TestMethod]
        public void IdIsMapped()
        {
            Guid id = Guid.NewGuid();
            source.Id.Returns(id);

            destination = sut.Map(source);

            Assert.AreEqual(id, destination.Id);
        }

        [TestMethod]
        public void NameIsMapped()
        {
            const string name = "event";
            source.Name.Returns(name);

            destination = sut.Map(source);

            Assert.AreEqual(name, destination.Name);
        }
    }
}
