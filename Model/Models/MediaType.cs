using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class MediaType
    {
        public MediaType()
        {
            Buildings = new HashSet<Building>();
            Metros = new HashSet<Metro>();
            Regions = new HashSet<Region>();
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Counter { get; set; } = null!;
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<Metro> Metros { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
