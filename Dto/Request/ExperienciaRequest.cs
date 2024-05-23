using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.Dto.Request
{
    public class ExperienciaRequest
    {
        public String empresa { get; set; }
        public String cargo { get; set; }
        public int tiempo { get; set; }
    }
}
