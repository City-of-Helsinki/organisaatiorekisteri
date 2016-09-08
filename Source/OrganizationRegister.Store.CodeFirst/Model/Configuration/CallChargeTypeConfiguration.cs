using System.Data.Entity.ModelConfiguration;

namespace OrganizationRegister.Store.CodeFirst.Model.Configuration
{
    internal class CallChargeTypeConfiguration : EntityTypeConfiguration<CallChargeType>
    {
        public CallChargeTypeConfiguration()
        {
            HasKey(callChargeType => callChargeType.Id);
            Property(callChargeType => callChargeType.Type).IsRequired();
            Property(callChargeType => callChargeType.OrderNumber).IsRequired();
        }
    }
}