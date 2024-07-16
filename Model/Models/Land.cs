using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Land
    {
        public Land()
        {
            LandCustomers = new HashSet<LandCustomer>();
            LandImgs = new HashSet<LandImg>();
        }

        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Number { get; set; }
        public string? Region { get; set; }
        public string? Address { get; set; }
        public string? CoordinateX { get; set; }
        public string? CoordinateY { get; set; }
        public short? Area { get; set; }
        public DateTime? Date { get; set; }
        public int? Price { get; set; }
        public string? Addition { get; set; }
        public string? Document { get; set; }
        public bool? IsCalledWithHomeOwnFirstStep { get; set; }
        public bool? IsCalledWithCustomerFirstStep { get; set; }
        public bool? IsPaidHomeOwnFirstStep { get; set; }
        public bool? IsPaidCustomerFirstStep { get; set; }
        public bool? IsCalledWithHomeOwnThirdStep { get; set; }
        public string? Email { get; set; }
        public int? Looking { get; set; }
        public bool? Recommend { get; set; }

        public virtual ICollection<LandCustomer> LandCustomers { get; set; }
        public virtual ICollection<LandImg> LandImgs { get; set; }
    }
}
