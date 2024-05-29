using jobfinder_back.clases;
using jobfinder_back.Dto.Request;
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
    [RoutePrefix("api/Empleador")]
    public class EmpleadorController : ApiController
    {
        public string Post([FromBody] EmpleadorRequest empleadorRequest)
        {
            Cifrar cifrar = new Cifrar();

            Perfil perfil = new Perfil();

            perfil.email = empleadorRequest.email;
            perfil.nombre = empleadorRequest.nombre;
            perfil.contrasenia = cifrar.cifrarPassword(empleadorRequest.contrasenia);

            clsPerfil _perfil = new clsPerfil();

            int id_perfil = _perfil.Insertar(perfil);

            Empleador empleador = new Empleador();

            empleador.ciudad = empleadorRequest.ciudad;
            empleador.descripcion = empleadorRequest.descripcion;
            empleador.actividad = empleadorRequest.actividad;
            empleador.id_perfil = id_perfil;

            clsEmpleador _empleador = new clsEmpleador();

            return _empleador.Insertar(empleador);
        }
        [HttpPost]
        [Route("IniciarSesion")]
        public IQueryable IniciarSesion([FromBody] Perfil perfil)
        {
            clsEmpleador empleador = new clsEmpleador();
            return empleador.ConsultarEmpleador(perfil);
        }
        public string Delete(int id)
        {
            clsEmpleador _empleador = new clsEmpleador();
            return _empleador.Eliminar(id);
        }

        public string Put([FromBody] EmpleadorRequest empleador)
        {
            clsEmpleador _empleador = new clsEmpleador();
            return _empleador.Actualizar(empleador);
        }
    }
}