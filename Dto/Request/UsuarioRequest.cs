using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.Dto.Request
{
    public class UsuarioRequest
    {
        public int id {  get; set; }
        public String nombre { get; set; }
        public String telefono { get; set; }
        public String ciudad { get; set; }
        public String email { get; set; }
        public String contrasenia { get; set; }
        public String genero { get; set; }
        public String perfil { get; set; }
        public int curriculum_id { get; set; }
        public CurriculumRequest curriculum { get; set; }







    }
}