using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Commanding.Service.CommandHandlers;
using OrganizationRegister.Commanding.Service.Commands;
using OrganizationRegister.Store.CodeFirst;

namespace OrganizationRegister.Commanding.Tests.Service.CommandHandlers
{
    [TestClass]
    public class SetServiceClassificationHandlerTests
    {
        private SetServiceClassificationHandler sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new SetServiceClassificationHandler(Substitute.For<IStoreContext>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullServiceClasses()
        {
            var command = new SetServiceClassification(Guid.NewGuid(), Guid.NewGuid(), null, new List<Guid> { Guid.NewGuid() }, new List<Guid> { Guid.NewGuid() }, null, null);
            sut.Execute(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyServiceClassCollection()
        {
            var command = new SetServiceClassification(Guid.NewGuid(), Guid.NewGuid(), new List<Guid>(), new List<Guid> { Guid.NewGuid() }, new List<Guid> { Guid.NewGuid() }, null, null);
            sut.Execute(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ServiceClassCollectionWithDuplicateIds()
        {
            Guid serviceClassId = Guid.NewGuid();
            var command = new SetServiceClassification(Guid.NewGuid(), Guid.NewGuid(), new List<Guid> { serviceClassId, serviceClassId, Guid.NewGuid() }, 
                new List<Guid> { Guid.NewGuid() }, new List<Guid> { Guid.NewGuid() }, null, null);
            sut.Execute(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullOntologyTerms()
        {
            var command = new SetServiceClassification(Guid.NewGuid(), Guid.NewGuid(), new List<Guid> { Guid.NewGuid() }, null, new List<Guid> { Guid.NewGuid() }, null, null);
            sut.Execute(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyOntologyTermCollection()
        {
            var command = new SetServiceClassification(Guid.NewGuid(), Guid.NewGuid(), new List<Guid> { Guid.NewGuid() }, new List<Guid>(), new List<Guid> { Guid.NewGuid() }, null, null);
            sut.Execute(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OntologyTermCollectionWithDuplicateIds()
        {
            Guid ontologyTermId = Guid.NewGuid();
            var command = new SetServiceClassification(Guid.NewGuid(), Guid.NewGuid(), new List<Guid> { Guid.NewGuid() }, new List<Guid> { ontologyTermId, ontologyTermId, Guid.NewGuid() },
                new List<Guid> { Guid.NewGuid() }, null, null);
            sut.Execute(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullTargetGroups()
        {
            var command = new SetServiceClassification(Guid.NewGuid(), Guid.NewGuid(), new List<Guid> { Guid.NewGuid() }, new List<Guid> { Guid.NewGuid() }, null, null, null);
            sut.Execute(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyTargetGroupCollection()
        {
            var command = new SetServiceClassification(Guid.NewGuid(), Guid.NewGuid(), new List<Guid> { Guid.NewGuid() }, new List<Guid> { Guid.NewGuid() }, new List<Guid>(), null, null);
            sut.Execute(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TargetGroupCollectionWithDuplicateIds()
        {
            Guid targetGroupId = Guid.NewGuid();
            var command = new SetServiceClassification(Guid.NewGuid(), Guid.NewGuid(), new List<Guid> { Guid.NewGuid() }, new List<Guid> { Guid.NewGuid() }, 
                new List<Guid> { targetGroupId, targetGroupId, Guid.NewGuid() }, null, null);
            sut.Execute(command);
        }
    }
}
