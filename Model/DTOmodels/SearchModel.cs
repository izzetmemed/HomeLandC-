using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOmodels
{
    public class SearchModel
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string[]? Metro { get; set; }
        public string[]? Region { get; set; }
        public string[]? Room { get; set; }
        public string[]? Building { get; set; }
    }
}
