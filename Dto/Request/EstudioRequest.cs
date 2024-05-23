using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.Dto.Request
{
    public class EstudioRequest
    {
        public String institucion {  get; set; }
        public String titulo { get; set; }
        public int tiempo { get; set; }
    }
}