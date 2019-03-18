using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class GroupOrganizationsQuery
    {
        private readonly IQueryable<Organization> organizations;

        public GroupOrganizationsQuery(IQueryable<Organization> organizations)
        {
            this.organizations = organizations ?? throw new ArgumentNullException("organizations");
        }
        
        public IEnumerable<Organization> Execute(Guid groupId)
        {
            var now = DateTime.Now.Date;
            return
                organizations.Where(
                    o => o.Active
                    && (!o.ValidFrom.HasValue || o.ValidFrom.HasValue && o.ValidFrom.Value <= now)
                    && (!o.ValidTo.HasValue || o.ValidTo.HasValue && o.ValidTo.Value >= now)
                    && o.AuthorizationGroups.Any(data => data.GroupId == groupId)
                    );
        }

        public IEnumerable<Organization> Execute(Guid groupId, Guid roleId)
        {
            var now = DateTime.Now.Date;
            return
                organizations.Where(
                    o => o.Active
                    && (!o.ValidFrom.HasValue || o.ValidFrom.HasValue && o.ValidFrom.Value <= now)
                    && (!o.ValidTo.HasValue || o.ValidTo.HasValue && o.ValidTo.Value >= now)
                    && o.AuthorizationGroups.Any(data => data.GroupId == groupId && data.RoleId == roleId)
                    );
        }

    }
}
