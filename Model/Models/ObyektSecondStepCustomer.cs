using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class ObyektSecondStepCustomer
    {
        public int SecondStepCustomerId { get; set; }
        public int? SecondStepCustomerForeignId { get; set; }
        public string? FullName { get; set; }
        public string? Number { get; set; }
        public DateTime? DirectCustomerDate { get; set; }

        public virtual Obyekt? SecondStepCustomerForeign { get; set; }
    }
}
