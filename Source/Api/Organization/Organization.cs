﻿using System;
using System.Collections.Generic;
using ServiceRegister.Api.Location;
using ServiceRegister.Common;

namespace ServiceRegister.Api.Organization
{
    public class Organization
    {
        public Guid Id { get; set; }
        public long NumericId { get; set; }
        public string BusinessId { get; set; }
        public string Oid { get; set; }
        public string Type { get; set; }
        public IEnumerable<LocalizedText> Names { get; set; }
        public IEnumerable<LocalizedText> Descriptions { get; set; }
        public string MunicipalityCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneCallFee { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<WebPage> WebPages { get; set; }
        public StreetAddress VisitingAddress { get; set; }
        public IEnumerable<LocalizedText> VisitingAddressQualifiers { get; set; }
        public StreetAddress PostalStreetAddress { get; set; }
        public PostOfficeBoxAddress PostalPostOfficeBoxAddress { get; set; }
        public bool UseVisitingAddressAsPostalAddress { get; set; }
        public bool IsSubOrganization { get; set; }
    }
}