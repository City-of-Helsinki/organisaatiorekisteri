using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class ServiceLifeEvent
    {
        public Guid LifeEventId { get; set; }
        public Guid ServiceId { get; set; }
        public virtual LifeEvent LifeEvent { get; set; }
        public virtual Service Service { get; set; }
    }
}
