using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class CallChargeType
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int OrderNumber { get; set; }
    }
}
