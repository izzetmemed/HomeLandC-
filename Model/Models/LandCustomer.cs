﻿using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class LandCustomer
    {
        public int SecondStepCustomerId { get; set; }
        public int? SecondStepCustomerForeignId { get; set; }
        public string? FullName { get; set; }
        public string? Number { get; set; }
        public DateTime? DirectCustomerDate { get; set; }
        public string? Email { get; set; }

        public virtual Land? SecondStepCustomerForeign { get; set; }
    }
}
