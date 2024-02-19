using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Region
    {
        public int Id { get; set; }
        public int RegionForeignId { get; set; }
        public string RegionName { get; set; } = null!;

        public virtual MediaType RegionForeign { get; set; } = null!;
    }
}
