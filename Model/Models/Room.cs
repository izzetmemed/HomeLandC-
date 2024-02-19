using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Room
    {
        public int Id { get; set; }
        public int RoomForeignId { get; set; }
        public int RoomCount { get; set; }

        public virtual MediaType RoomForeign { get; set; } = null!;
    }
}
