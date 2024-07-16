using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOmodels
{
    public class SearchDTO
    {
        public int PaginationCount { get; set; }
        public List<string> Data { get; set; }
    }
}
