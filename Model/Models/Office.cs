using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Office
    {
        public Office()
        {
            OfficeCustomers = new HashSet<OfficeCustomer>();
            OfficeImgs = new HashSet<OfficeImg>();
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
        public string? Item { get; set; }
        public byte? Room { get; set; }
        public string? Repair { get; set; }
        public string? SellOrRent { get; set; }
        public string? Metro { get; set; }
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

        public virtual ICollection<OfficeCustomer> OfficeCustomers { get; set; }
        public virtual ICollection<OfficeImg> OfficeImgs { get; set; }
    }
}
