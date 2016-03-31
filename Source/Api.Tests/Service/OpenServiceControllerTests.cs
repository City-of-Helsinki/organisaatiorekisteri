using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Service;
using OrganizationRegister.Application.Service;

namespace OrganizationRegister.Api.Tests.Service
{
    [TestClass]
    public class OpenServiceControllerTests : ServiceControllerTests
    {
        private IServiceService serviceService;
        private OpenServiceController sut;

        [TestInitialize]
        public void Setup()
        {
            serviceService = Substitute.For<IServiceService>();
            SetupMappers();
            sut = new OpenServiceController(serviceService, mapperFactory);
        }

        [TestMethod]
        public void GetServices()
        {
            Guid organizationId = Guid.NewGuid();
            IServiceListItem appResult = Substitute.For<IServiceListItem>();
            ServiceListItem apiResult = new ServiceListItem();
            serviceService.GetServices(organizationId).Returns(new List<IServiceListItem> { appResult });
            serviceNameMapper.Map(appResult).Returns(apiResult);

            OkNegotiatedContentResult<IEnumerable<ServiceListItem>> result = sut.GetServices(organizationId) as OkNegotiatedContentResult<IEnumerable<ServiceListItem>>;

            Assert.IsNotNull(result);
            Assert.AreSame(apiResult, result.Content.Single());
        }

        [TestMethod]
        public void GetService()
        {
            Guid organizationId = Guid.NewGuid();
            Guid serviceId = Guid.NewGuid();
            IService appResult = Substitute.For<IService>();
            Api.Service.Service apiResult = new Api.Service.Service();
            serviceService.GetService(organizationId, serviceId).Returns(appResult);
            serviceMapper.Map(appResult).Returns(apiResult);

            OkNegotiatedContentResult<Api.Service.Service> result = sut.GetService(organizationId, serviceId) as OkNegotiatedContentResult<Api.Service.Service>;

            Assert.IsNotNull(result);
            Assert.AreSame(apiResult, result.Content);
        }
    }
}
