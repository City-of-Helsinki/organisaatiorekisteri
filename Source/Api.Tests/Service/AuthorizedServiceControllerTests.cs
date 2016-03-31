using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Affecto.Patterns.Cqrs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Service;
using OrganizationRegister.Application.Service;
using OrganizationRegister.Commanding.Service.Commands;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Tests.Service
{
    [TestClass]
    public class AuthorizedServiceControllerTests : ServiceControllerTests
    {
        private IServiceService serviceService;
        private ICommandBus commandBus;
        private AuthorizedServiceController sut;

        [TestInitialize]
        public void Setup()
        {
            serviceService = Substitute.For<IServiceService>();
            commandBus = Substitute.For<ICommandBus>();
            SetupMappers();
            sut = new AuthorizedServiceController(serviceService, commandBus);
        }

        [TestMethod]
        public void AddService()
        {
            BasicInformation info = new BasicInformation
            {
                AlternateNames = new List<LocalizedText> { new LocalizedText("fi", "nimi"), new LocalizedText("en", "name") },
                ShortDescriptions = new List<LocalizedText> { new LocalizedText("fi", "kuv."), new LocalizedText("en", "desc.") },
                Names = new List<LocalizedText> { new LocalizedText("fi", "virallinen nimi"), new LocalizedText("en", "real name") },
                UserInstructions = new List<LocalizedText> { new LocalizedText("fi", "ohjeita"), new LocalizedText("en", "instructions") },
                LanguageCodes = new List<string> { "fi", "en" },
                Descriptions = new List<LocalizedText> { new LocalizedText("fi", "kuvaus"), new LocalizedText("en", "description") },
                Requirements = new List<LocalizedText> { new LocalizedText("fi", "vaatimuksia"), new LocalizedText("en", "reqs") }
            };
            Guid organizationId = new Guid();
            sut.AddService(organizationId, info);

            serviceService.Received(1).AddService(organizationId, info.Names, info.AlternateNames, info.Descriptions, info.ShortDescriptions,
                info.UserInstructions, info.LanguageCodes, info.Requirements);
        }

        [TestMethod]
        public void AddServiceReturnsServiceId()
        {
            Guid serviceId = Guid.NewGuid();
            serviceService.AddService(Arg.Any<Guid>(), Arg.Any<IEnumerable<LocalizedText>>(), Arg.Any<IEnumerable<LocalizedText>>(), Arg.Any<IEnumerable<LocalizedText>>(),
                Arg.Any<IEnumerable<LocalizedText>>(), Arg.Any<IEnumerable<LocalizedText>>(), Arg.Any<IEnumerable<string>>(), Arg.Any<IEnumerable<LocalizedText>>())
                .Returns(serviceId);

            var result = sut.AddService(Guid.NewGuid(), new BasicInformation()) as OkNegotiatedContentResult<Guid>;

            Assert.IsNotNull(result);
            Assert.AreEqual(serviceId, result.Content);
        }

        [TestMethod]
        public void SetServiceBasicInformation()
        {
            BasicInformation info = new BasicInformation
            {
                AlternateNames = new List<LocalizedText> { new LocalizedText("fi", "nimi"), new LocalizedText("en", "name") },
                ShortDescriptions = new List<LocalizedText> { new LocalizedText("fi", "kuv."), new LocalizedText("en", "desc.") },
                Names = new List<LocalizedText> { new LocalizedText("fi", "virallinen nimi"), new LocalizedText("en", "real name") },
                UserInstructions = new List<LocalizedText> { new LocalizedText("fi", "ohjeita"), new LocalizedText("en", "instructions") },
                LanguageCodes = new List<string> { "fi", "en" },
                Descriptions = new List<LocalizedText> { new LocalizedText("fi", "kuvaus"), new LocalizedText("en", "description") },
                Requirements = new List<LocalizedText> { new LocalizedText("fi", "vaatimuksia"), new LocalizedText("en", "reqs") }
            };
            Guid organizationId = Guid.NewGuid();
            Guid serviceId = Guid.NewGuid();
            sut.SetServiceBasicInformation(organizationId, serviceId, info);

            serviceService.Received(1).SetServiceBasicInformation(organizationId, serviceId, info.Names, info.AlternateNames, info.Descriptions, info.ShortDescriptions, 
                info.UserInstructions, info.LanguageCodes, info.Requirements);
        }

        [TestMethod]
        public void SetServiceClassification()
        {
            Api.Service.Classification classification = new Api.Service.Classification
            {
                Keywords = new List<LocalizedText> { new LocalizedText("fi", "word, key, class") },
                ServiceClasses = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                LifeEvents = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                OntologyTerms = new List<Guid> { Guid.NewGuid() },
                TargetGroups = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }
            };
            Guid organizationId = Guid.NewGuid();
            Guid serviceId = Guid.NewGuid();

            sut.SetServiceClassification(organizationId, serviceId, classification);

            commandBus.When(bus => bus.Send(Arg.Any<Envelope<ICommand>>())).Do(x =>
            {
                SetServiceClassification command = x.Arg<Envelope<ICommand>>().Body as SetServiceClassification;
                Assert.IsNotNull(command);
                Assert.AreEqual(organizationId, command.OrganizationId);
                Assert.AreEqual(serviceId, command.ServiceId);
                Assert.AreSame(classification.Keywords, command.Keywords);
                Assert.AreSame(classification.TargetGroups, command.TargetGroups);
                Assert.AreSame(classification.OntologyTerms, command.OntologyTerms);
                Assert.AreSame(classification.ServiceClasses, command.ServiceClasses);
                Assert.AreSame(classification.LifeEvents, command.LifeEvents);
            });
        }
    }
}
