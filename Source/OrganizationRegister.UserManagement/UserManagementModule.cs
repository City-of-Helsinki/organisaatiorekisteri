using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.AuditTrail.Interfaces;
using Affecto.AuditTrail.Interfaces.Model;
using Autofac;
using OrganizationRegister.Application.User;
using OrganizationRegister.UserManagement.Mapping;
using IdentityManagement = Affecto.IdentityManagement;

namespace OrganizationRegister.UserManagement
{
    public class UserManagementModule : Module
    {
        private readonly bool useMockDatabase;

        public UserManagementModule(bool useMockDatabase)
        {
            this.useMockDatabase = useMockDatabase;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<GroupService>().As<IGroupService>();
            builder.RegisterType<UserManagementTestEnvironment>();
            builder.RegisterType<MapperFactory>();
            builder.RegisterModule<IdentityManagement.Autofac.ModuleRegistration>();

            if (useMockDatabase)
            {
                builder.RegisterModule<IdentityManagement.Store.Mocking.MockPostgreDbRegistration>();
            }
            else
            {
                builder.RegisterModule<IdentityManagement.Store.PostgreSql.ModuleRegistration>();
            }

            builder.RegisterType<AuditTrailMock>().As<IAuditTrailService>();
        }
    }

    internal class AuditTrailMock : IAuditTrailService
    {
        public IEnumerable<IAuditTrailEntry> GetEntries()
        {
            return Enumerable.Empty<IAuditTrailEntry>();
        }

        public IAuditTrailResult GetEntries(IAuditTrailFilter filter)
        {
            return null;
        }

        public IEnumerable<IAuditTrailEntry> GetEntriesForSubject(Guid subjectId)
        {
            return Enumerable.Empty<IAuditTrailEntry>();
        }

        public IAuditTrailEntry GetEntry(Guid auditTrailEntryId)
        {
            return null;
        }

        public IAuditTrailEntry CreateEntry(Guid subjectId, string summary, string subjectName, string userName)
        {
            return null;
        }

        public IAuditTrailEntry CreateEntry(Guid subjectId, Guid userId, string summary, string subjectName, string userName)
        {
            return null;
        }
    }
}