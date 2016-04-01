using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Store.CodeFirst.Model;
using OrganizationRegister.Store.CodeFirst.Querying;

namespace OrganizationRegister.Store.Tests.Querying
{
    [TestClass]
    public class ServiceQueryTests
    {
        private ServiceQuery sut;
        private List<Service> services;

        [TestInitialize]
        public void Setup()
        {
            services = new List<Service>();
            IQueryable<Service>  servicesQueryable = Substitute.For<IQueryable<Service>>();
            servicesQueryable.GetEnumerator().Returns(services.GetEnumerator());
            sut = new ServiceQuery(servicesQueryable);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ServiceIsFoundButTheOrganizationIdDoesNotMatch()
        {
            Guid serviceId = Guid.NewGuid();
            services.Add(new Service { Id = serviceId, OrganizationId = Guid.NewGuid() });

            sut.Execute(Guid.NewGuid(), serviceId);
        }
    }
}
