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
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        public string Post([FromBody] UsuarioRequest usuarioRequest)
        {
            Curriculum curriculum = new Curriculum();
            clsCurriculum _curriculum = new clsCurriculum();

            curriculum.perfil = usuarioRequest.perfil;

            int curriculum_id = _curriculum.Insertar(curriculum);

            clsEstudio _estudio = new clsEstudio();

            foreach (EstudioRequest estudio in usuarioRequest.curriculum.estudios)
            {
                Estudio modelEstudio = new Estudio();

                modelEstudio.institucion = estudio.institucion;
                modelEstudio.titulo = estudio.titulo;
                modelEstudio.anio = estudio.tiempo;
                modelEstudio.curriculum_id = curriculum_id;
                
                _estudio.Insertar(modelEstudio);
            }

            clsExperiencia _experiencia = new clsExperiencia();

            foreach (ExperienciaRequest experiencia in usuarioRequest.curriculum.experiencias)
            {
                Experiencia modelExperiencia = new Experiencia();

                modelExperiencia.empresa = experiencia.empresa;
                modelExperiencia.cargo = experiencia.cargo;
                modelExperiencia.anios = experiencia.tiempo;
                modelExperiencia.curriculum_id = curriculum_id;

                _experiencia.Insertar(modelExperiencia);
            }

            Perfil perfil = new Perfil();
            Cifrar cifrar = new Cifrar();

            perfil.email = usuarioRequest.email;
            perfil.nombre = usuarioRequest.nombre;
            perfil.contrasenia = cifrar.cifrarPassword(usuarioRequest.contrasenia);

            clsPerfil _perfil = new clsPerfil();

            int id_perfil = _perfil.Insertar(perfil);

            Usuario usuario = new Usuario();

            usuario.telefono = usuarioRequest.telefono;
            usuario.ciudad = usuarioRequest.ciudad;
            usuario.genero = usuarioRequest.genero;
            usuario.id_perfil = id_perfil;
            usuario.curriculum_id = curriculum_id;

            clsUsuario _usuario = new clsUsuario();

            return _usuario.Insertar(usuario);
        }

        public string Delete(int id)
        {
            clsUsuario _usuario = new clsUsuario();
            return _usuario.Eliminar(id);
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public IQueryable IniciarSesion([FromBody] Perfil perfil)
        {
            clsUsuario usuario = new clsUsuario();
            return usuario.ConsultarUsuario(perfil);
        }

        public string Put([FromBody] UsuarioRequest usuarioRequest)
        {
            clsUsuario _usuario = new clsUsuario();
            return _usuario.Actualizar(usuarioRequest);
        }
    }
}