using System;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class ServiceTargetGroup
    {
        public Guid TargetGroupId { get; set; }
        public Guid ServiceId { get; set; }
        public virtual TargetGroup TargetGroup { get; set; }
        public virtual Service Service { get; set; }
    }
}
