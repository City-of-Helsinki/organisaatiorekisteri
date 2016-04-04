using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Organization;
using OrganizationRegister.Application.Localization;
using OrganizationRegister.Application.Organization;
using OrganizationRegister.Common;

namespace OrganizationRegister.Api.Tests.Organization
{
    [TestClass]
    public class OrganizationNameMapperTests
    {
        private OrganizationNameMapper sut;
        private IOrganizationName source;
        private OrganizationName destination;

        [TestInitialize]
        public void Setup()
        {
            source = Substitute.For<IOrganizationName>();
            sut = new OrganizationNameMapper();
        }

        [TestMethod]
        public void NamesAreMapped()
        {
            LocalizedTexts names = new LocalizedSingleTexts(new List<LocalizedText>
            {
                new LocalizedText("fi", "nimi"),
                new LocalizedText("en", "name")
            });
            source.Names.Returns(names);

            destination = sut.Map(source);

            Assert.AreEqual(2, destination.Names.Count());
            Assert.IsTrue(destination.Names.Any(name => name.LanguageCode.Equals("fi") && name.LocalizedValue.Equals("nimi")));
            Assert.IsTrue(destination.Names.Any(name => name.LanguageCode.Equals("en") && name.LocalizedValue.Equals("name")));
        }

        [TestMethod]
        public void IdIsMapped()
        {
            Guid organizationId = Guid.NewGuid();
            source.Id.Returns(organizationId);

            destination = sut.Map(source);

            Assert.AreEqual(organizationId, destination.Id);
        }
    }
}
