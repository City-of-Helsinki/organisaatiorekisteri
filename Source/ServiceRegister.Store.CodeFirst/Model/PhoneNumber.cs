﻿using System;

namespace ServiceRegister.Store.CodeFirst.Model
{
    public class PhoneNumber
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string PhoneCallFee { get; set; }
    }
}