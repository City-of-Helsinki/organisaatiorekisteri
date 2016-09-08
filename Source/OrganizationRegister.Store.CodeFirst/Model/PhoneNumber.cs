using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OrganizationRegister.Store.CodeFirst.Model
{
    public class PhoneNumber
    {

        public PhoneNumber()
        {
            LanguageSpecifications = new Collection<PhoneNumberLanguageSpecification>();
        }

        public Guid Id { get; set; }

        public string Number { get; set; }
        //public string PhoneCallFee { get; set; }
        public Guid ChargeTypeId { get; set; }
        public virtual CallChargeType ChargeType { get; set; }
        public virtual ICollection<PhoneNumberLanguageSpecification> LanguageSpecifications { get; set; }

    }
}