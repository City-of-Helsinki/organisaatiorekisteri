using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class ServiceServiceClass
    {
        public Guid ServiceClassId { get; set; }
        public Guid ServiceId { get; set; }
        public virtual ServiceClass ServiceClass { get; set; }
        public virtual Service Service { get; set; }
    }
}
