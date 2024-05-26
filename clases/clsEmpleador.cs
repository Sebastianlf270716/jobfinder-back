using jobfinder_back.Models;
using jobfinder_back.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.clases
{    
    public class clsEmpleador
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public string Insertar(Empleador empleador)
        {
            try
            {
                jobfinder.Empleadors.Add(empleador);
                jobfinder.SaveChanges();

                return "Se guardó exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public IQueryable ConsultarEmpleador(Perfil perfil)
        {
            Cifrar cifrar = new Cifrar();
            perfil.contrasenia = cifrar.cifrarPassword(perfil.contrasenia);
            Perfil _perfil = jobfinder.Perfils.FirstOrDefault(p => p.email == perfil.email && p.contrasenia == perfil.contrasenia);

            if (_perfil == null)
            {
                return null;
            }

            return from P in jobfinder.Set<Perfil>()
                   join E in jobfinder.Set<Empleador>()
                   on P.id_perfil equals E.id_perfil
                   where P.id_perfil == _perfil.id_perfil
                   select new
                   {
                       id = E.id,

                       nombre = P.nombre,
                       email = P.email,
                       ciudad = E.ciudad,
                       telefono = E.actividad,
                       genero = E.descripcion,
                       tipo_perfil = "Empleador"
                   };
        }

    }
}
