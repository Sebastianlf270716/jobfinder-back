using jobfinder_back.Models;
using jobfinder_back.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobfinder_back.Dto.Request;
using System.Data.Entity.Migrations;

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
            Empleador _empleador = jobfinder.Empleadors.FirstOrDefault(e => e.id_perfil == _perfil.id_perfil);
            if (_empleador == null)
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
                       actividad = E.actividad,
                       descripcion = E.descripcion,
                       tipo_perfil = "Empleador"
                   };
        }
        public string Eliminar(int id)
        {
            try
            {
                Empleador empleador = jobfinder.Empleadors.FirstOrDefault(e => e.id == id);
                if (empleador == null)
                {
                    return null;
                }

                List<Gestion> gestiones = jobfinder.Gestions.Where(g => g.empleador_id == empleador.id).ToList();
                List<Oferta> ofertas = new List<Oferta>();
                foreach (var gestion in gestiones)
                {
                    Oferta oferta = jobfinder.Ofertas.FirstOrDefault(o => o.id == gestion.oferta_id);
                    ofertas.Add(oferta);
                }
                if (gestiones.Count > 0)
                {
                    jobfinder.Gestions.RemoveRange(gestiones);
                    jobfinder.SaveChanges();
                }

                foreach (var oferta in ofertas)
                {
                    List<Funcion> funciones = jobfinder.Funcions.Where(f => f.oferta_id == oferta.id).ToList();
                    jobfinder.Funcions.RemoveRange(funciones);
                    jobfinder.SaveChanges();
                }

                jobfinder.Ofertas.RemoveRange(ofertas);
                jobfinder.SaveChanges();

                Perfil perfil = jobfinder.Perfils.FirstOrDefault(p => p.id_perfil == empleador.id_perfil);
                jobfinder.Empleadors.Remove(empleador);
                jobfinder.SaveChanges();
                jobfinder.Perfils.Remove(perfil);
                jobfinder.SaveChanges();
                return "Perfil eliminado exitosamente";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Actualizar(EmpleadorRequest empleador)
        {
            try
            {
                Empleador _empleador = jobfinder.Empleadors.FirstOrDefault(e => e.id == empleador.id);
                _empleador.ciudad = empleador.ciudad;
                _empleador.actividad = empleador.actividad;
                _empleador.descripcion = empleador.descripcion;

                jobfinder.Empleadors.AddOrUpdate(_empleador);
                jobfinder.SaveChanges();

                Perfil _perfil = jobfinder.Perfils.FirstOrDefault(p => p.id_perfil == _empleador.id_perfil);
                _perfil.nombre = empleador.nombre;

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
