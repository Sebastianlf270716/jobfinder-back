using jobfinder_back.clases;
using jobfinder_back.Dto.Request;
using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jobfinder_back.Controllers
{
    public class EmpleadorController : ApiController
    {
        public string Post([FromBody] EmpleadorRequest empleadorRequest)
        {
            //Comentario
            Perfil perfil = new Perfil();

            perfil.email = empleadorRequest.email;
            perfil.nombre = empleadorRequest.nombre;
            perfil.contrasenia = empleadorRequest.contrasenia;

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
    }
}