﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OrganizationRegister.Api.Settings;
using OrganizationRegister.Application.User;

namespace OrganizationRegister.Api.Tests.Settings
{
    [TestClass]
    public class RoleMapperTests
    {
        private RoleMapper sut;
        private IRole source;
        private Role destination;

        [TestInitialize]
        public void Setup()
        {
            sut = new RoleMapper();
            source = Substitute.For<IRole>();
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
            const string name = "Admin";
            source.Name.Returns(name);

            destination = sut.Map(source);

            Assert.AreEqual(name, destination.Name);
        }
    }
}