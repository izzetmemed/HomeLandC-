using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Medium
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Type { get; set; }
        public string? Region { get; set; }
        public string? SellOrRent { get; set; }
        public string? Metro { get; set; }
        public string? Building { get; set; }
        public byte? Room { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }
    }
}
