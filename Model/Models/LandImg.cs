using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class LandImg
    {
        public int ImgId { get; set; }
        public int? ImgIdForeignId { get; set; }
        public string? ImgPath { get; set; }

        public virtual Land? ImgIdForeign { get; set; }
    }
}
