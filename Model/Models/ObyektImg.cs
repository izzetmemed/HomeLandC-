﻿using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class ObyektImg
    {
        public int ImgId { get; set; }
        public int? ImgIdForeignId { get; set; }
        public string? ImgPath { get; set; }

        public virtual Obyekt? ImgIdForeign { get; set; }
    }
}