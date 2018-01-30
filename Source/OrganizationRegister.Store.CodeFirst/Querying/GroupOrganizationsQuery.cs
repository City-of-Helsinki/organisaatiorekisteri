using System;
using System.Collections.Generic;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class GroupOrganizationsQuery
    {
        private readonly IQueryable<Organization> organizations;
        private readonly Guid groupId;

        public GroupOrganizationsQuery(IQueryable<Organization> organizations, Guid groupId)
        {
            if (organizations == null)
            {
                throw new ArgumentNullException("organizations");
            }
            this.organizations = organizations;
            this.groupId = groupId;

        }

        public IEnumerable<Organization> Execute()
        {
            //TODO:Get orgs with the specified groupId
            var now = DateTime.Now.Date;
            return
                organizations.Where(
                    o => o.Active
                    && (!o.ValidFrom.HasValue || o.ValidFrom.HasValue && o.ValidFrom.Value <= now)
                    && (!o.ValidTo.HasValue || o.ValidTo.HasValue && o.ValidTo.Value >= now)
                    && o.AuthorizationGroups.Any(data => data.GroupId == groupId)
                    );
        }
    }
}
