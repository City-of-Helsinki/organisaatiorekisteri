using System;
using System.Linq;
using OrganizationRegister.Store.CodeFirst.Model;

namespace OrganizationRegister.Store.CodeFirst.Querying
{
    internal class CallChargeTypeQuery
    {
        private readonly IQueryable<CallChargeType> callChargeTypes;

        public CallChargeTypeQuery(IQueryable<CallChargeType> callChargeTypes)
        {
            if (callChargeTypes == null)
            {
                throw new ArgumentNullException("callChargeTypes");
            }
            this.callChargeTypes = callChargeTypes.OrderBy(t => t.OrderNumber);
        }

        public CallChargeType Execute(string type)
        {
            try
            {
                return callChargeTypes.Single(t => t.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("No or more than one call charge types '{0}' found.", type), e);
            }
        }

        public CallChargeType Execute(Guid id)
        {
            try
            {
                return callChargeTypes.Single(t => t.Id.Equals(id));
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("No or more than one call charge type with id '{0}' found.", id), e);
            }
        }
    }
}
