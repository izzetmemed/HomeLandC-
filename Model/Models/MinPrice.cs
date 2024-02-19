using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class MinPrice
    {
        public int Id { get; set; }
        public int MinPriceForeignId { get; set; }
        public int Price { get; set; }

        public virtual MediaType MinPriceForeign { get; set; } = null!;
    }
}
