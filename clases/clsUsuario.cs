using jobfinder_back.Models;
using System;
using System.Collections.Generic;
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
        public IQueryable ConsultarUsuario(Perfil perfil)
        {
            Perfil _perfil = jobfinder.Perfils.FirstOrDefault(p => p.email == perfil.email && p.contrasenia == perfil.contrasenia);
            if (_perfil == null)
            {
                return null;
            }
            return from P in jobfinder.Set<Perfil>()
                   join U in jobfinder.Set<Usuario>()
                   on P.id_perfil equals U.id_perfil
                   where P.id_perfil == _perfil.id_perfil
                   select new
                   {
                       id = U.id,
                       nombre = P.nombre,
                       email = P.email,
                       telefono = U.telefono,
                       ciudad = U.ciudad,
                       genero = U.genero,
                       tipo_perfil = "Usuario"
                   };
        }

    }
}