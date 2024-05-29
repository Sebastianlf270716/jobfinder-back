using jobfinder_back.Dto.Request;
using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsUsuario
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public string Insertar(Usuario usuario)
        {
            try
            {
                jobfinder.Usuarios.Add(usuario);
                jobfinder.SaveChanges();

                return "Se guardó exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Eliminar(int id)
        {
            try
            {
                Usuario usuario = jobfinder.Usuarios.FirstOrDefault(u => u.id==id);
                if (usuario==null)
                {
                    return null;
                }
                Curriculum curriculum = jobfinder.Curricula.FirstOrDefault(c => c.id == usuario.curriculum_id);
                jobfinder.Usuarios.Remove(usuario);
                jobfinder.SaveChanges();
                List<Estudio> estudios = jobfinder.Estudios.Where(e => e.curriculum_id == curriculum.id).ToList();
                if (estudios!= null)
                {
                    jobfinder.Estudios.RemoveRange(estudios);
                    jobfinder.SaveChanges();
                }

                List<Experiencia> experiencias = jobfinder.Experiencias.Where(e => e.curriculum_id == curriculum.id).ToList();
                if (experiencias != null)
                {
                    jobfinder.Experiencias.RemoveRange(experiencias);
                    jobfinder.SaveChanges();
                }

                jobfinder.Curricula.Remove(curriculum);
                jobfinder.SaveChanges();
                

                Perfil perfil = jobfinder.Perfils.FirstOrDefault(p => p.id_perfil==usuario.id_perfil);
                if (perfil != null)
                {
                    jobfinder.Perfils.Remove(perfil);
                    jobfinder.SaveChanges();
                }
                return "Usuario eliminado con éxito";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public IQueryable ConsultarUsuario(Perfil perfil)
        {
            Cifrar cifrar = new Cifrar();
            perfil.contrasenia = cifrar.cifrarPassword(perfil.contrasenia);
            Perfil _perfil = jobfinder.Perfils.FirstOrDefault(p => p.email == perfil.email && p.contrasenia == perfil.contrasenia);
            if (_perfil == null)
            {
                return null;
            }
            Usuario _user = jobfinder.Usuarios.FirstOrDefault(u => u.id_perfil == _perfil.id_perfil);
            if (_user ==null)
            {
                return null;
            }
            return from P in jobfinder.Set<Perfil>()
                   join U in jobfinder.Set<Usuario>()
                   on P.id_perfil equals U.id_perfil
                   join C in jobfinder.Set<Curriculum>()
                   on U.curriculum_id equals C.id
                   where P.id_perfil == _perfil.id_perfil
                   select new
                   {
                       id = U.id,
                       nombre = P.nombre,
                       email = P.email,
                       telefono = U.telefono,
                       ciudad = U.ciudad,
                       genero = U.genero,
                       curriculum_id = U.curriculum_id,
                       perfil = C.perfil,
                       tipo_perfil = "Usuario"
                   };
        }

        public string Actualizar(UsuarioRequest usuarioRequest)
        {
            try
            {
                Usuario _usuario = jobfinder.Usuarios.FirstOrDefault(u => u.id == usuarioRequest.id);
                _usuario.telefono = usuarioRequest.telefono;
                _usuario.ciudad = usuarioRequest.ciudad;
                _usuario.genero = usuarioRequest.genero;

                jobfinder.Usuarios.AddOrUpdate(_usuario);
                jobfinder.SaveChanges();

                Curriculum _curriculum = jobfinder.Curricula.FirstOrDefault(c => c.id == _usuario.curriculum_id);
                _curriculum.perfil = usuarioRequest.perfil;

                jobfinder.Curricula.AddOrUpdate(_curriculum);
                jobfinder.SaveChanges();

                clsEstudio _estudio = new clsEstudio();
                foreach (EstudioRequest estudio in usuarioRequest.curriculum.estudios)
                {
                    Estudio modelEstudio = new Estudio();

                    modelEstudio.institucion = estudio.institucion;
                    modelEstudio.titulo = estudio.titulo;
                    modelEstudio.anio = estudio.tiempo;
                    modelEstudio.curriculum_id = _curriculum.id;

                    _estudio.Insertar(modelEstudio);
                }

                clsExperiencia _experiencia = new clsExperiencia();
                foreach (ExperienciaRequest experiencia in usuarioRequest.curriculum.experiencias)
                {
                    Experiencia modelExperiencia = new Experiencia();

                    modelExperiencia.empresa = experiencia.empresa;
                    modelExperiencia.cargo = experiencia.cargo;
                    modelExperiencia.anios = experiencia.tiempo;
                    modelExperiencia.curriculum_id = _curriculum.id;

                    _experiencia.Insertar(modelExperiencia);
                }

                Perfil _perfil = jobfinder.Perfils.FirstOrDefault(p => p.id_perfil == _usuario.id_perfil);
                _perfil.nombre = usuarioRequest.nombre;

                jobfinder.Perfils.AddOrUpdate(_perfil);
                jobfinder.SaveChanges();

                return "Se actualizaron los datos correctamente";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

    }
}