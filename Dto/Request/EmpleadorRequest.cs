using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.Dto.Request
{
    public class EmpleadorRequest
    {
        public string nombre { get; set; }
        public string ciudad { get; set; } 
        public string actividad { get; set; }
        public string email { get; set; }
        public string descripcion { get; set; }
        public string contrasenia { get; set; }

    }
}