using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Obyekt
    {
        public Obyekt()
        {
            ObyektImgs = new HashSet<ObyektImg>();
            ObyektSecondStepCustomers = new HashSet<ObyektSecondStepCustomer>();
        }

        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Number { get; set; }
        public string? Region { get; set; }
        public string? İtem { get; set; }
        public string? Address { get; set; }
        public string? SellOrRent { get; set; }
        public string? Metro { get; set; }
        public string? CoordinateX { get; set; }
        public string? CoordinateY { get; set; }
        public byte? Room { get; set; }
        public string? Repair { get; set; }
        public short? Area { get; set; }
        public DateTime? Date { get; set; }
        public int? Price { get; set; }
        public string? Addition { get; set; }
        public bool? IsCalledWithHomeOwnFirstStep { get; set; }
        public bool? IsCalledWithCustomerFirstStep { get; set; }
        public bool? IsPaidHomeOwnFirstStep { get; set; }
        public bool? IsPaidCustomerFirstStep { get; set; }
        public bool? IsCalledWithHomeOwnThirdStep { get; set; }

        public virtual ICollection<ObyektImg> ObyektImgs { get; set; }
        public virtual ICollection<ObyektSecondStepCustomer> ObyektSecondStepCustomers { get; set; }
    }
}
