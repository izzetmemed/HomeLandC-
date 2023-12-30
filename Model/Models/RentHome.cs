using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class RentHome
    {
        public RentHome()
        {
            ImgNames = new HashSet<ImgName>();
            SecondStepCustomers = new HashSet<SecondStepCustomer>();
        }

        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Number { get; set; }
        public string? Region { get; set; }
        public string? Address { get; set; }
        public string? Floor { get; set; }
        public string? Metro { get; set; }
        public string? CoordinateX { get; set; }
        public string? CoordinateY { get; set; }
        public byte? Room { get; set; }
        public string? Repair { get; set; }
        public string? Building { get; set; }
        public string? İtem { get; set; }
        public bool? Bed { get; set; }
        public bool? Wardrobe { get; set; }
        public bool? TableChair { get; set; }
        public bool? CentralHeating { get; set; }
        public bool? GasHeating { get; set; }
        public bool? Combi { get; set; }
        public bool? Tv { get; set; }
        public bool? WashingClothes { get; set; }
        public bool? AirConditioning { get; set; }
        public bool? Sofa { get; set; }
        public bool? Wifi { get; set; }
        public short? Area { get; set; }
        public DateTime? Date { get; set; }
        public int? Price { get; set; }
        public string? Addition { get; set; }
        public bool? IsCalledWithHomeOwnFirstStep { get; set; }
        public bool? IsCalledWithCustomerFirstStep { get; set; }
        public bool? IsPaidHomeOwnFirstStep { get; set; }
        public bool? IsPaidCustomerFirstStep { get; set; }
        public bool? IsCalledWithHomeOwnThirdStep { get; set; }
        public bool? Boy { get; set; }
        public bool? Girl { get; set; }
        public bool? WorkingBoy { get; set; }
        public bool? Family { get; set; }

        public virtual ICollection<ImgName> ImgNames { get; set; }
        public virtual ICollection<SecondStepCustomer> SecondStepCustomers { get; set; }
    }
}
