using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Building
    {
        public int Id { get; set; }
        public int BuildingForeignId { get; set; }
        public string BuildingKind { get; set; } = null!;

        public virtual MediaType BuildingForeign { get; set; } = null!;
    }
}
