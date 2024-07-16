using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOmodels
{
    public class MediaDTO
    {
        public MediaType? mediaType { get; set; }
        public List<string>? Building { get; set; }
        public List<string>? Region { get; set; }
        public List<string>? Room { get; set; }
        public List<string>? Metro { get; set; }

    }
}
