using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsAdministrador
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();
        public IQueryable ConsultarAdministrador(Perfil perfil)
        {
            Perfil _perfil = jobfinder.Perfils.FirstOrDefault(p => p.email == perfil.email && p.contrasenia == perfil.contrasenia);
            if (_perfil == null)
            {
                return null;
            }
            return from P in jobfinder.Set<Perfil>()
                   join A in jobfinder.Set<Administrador>()
                   on P.id_perfil equals A.id_perfil
                   where P.id_perfil == _perfil.id_perfil
                   select new
                   {
                       id = A.id,
                       tipo_perfil = "Administrador"
                   };
        }
    }
}