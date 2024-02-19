using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class MaxPrice
    {
        public int Id { get; set; }
        public int MaxPriceForeignId { get; set; }
        public int Price { get; set; }

        public virtual MediaType MaxPriceForeign { get; set; } = null!;
    }
}
