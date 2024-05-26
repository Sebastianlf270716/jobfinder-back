using jobfinder_back.clases;
using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace jobfinder_back.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Administrador")]
    public class AdministradorController : ApiController
    {
        [HttpPost]
        [Route("IniciarSesion")]
        public IQueryable IniciarSesion([FromBody] Perfil perfil)
        {
            clsAdministrador admin = new clsAdministrador();
            return admin.ConsultarAdministrador(perfil);
        }
    }
}