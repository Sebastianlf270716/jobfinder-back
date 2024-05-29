using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsOferta
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public int Insertar(Oferta oferta)
        {
            try
            {
                jobfinder.Ofertas.Add(oferta);
                jobfinder.SaveChanges();

                return oferta.id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable obtenerOfertasEmpleador(int id_empleador)
        {
            return from O in jobfinder.Set<Oferta>()
                   join G in jobfinder.Set<Gestion>()
                   on O.id equals G.oferta_id
                   where G.empleador_id == id_empleador
                   select new
                   {
                       id = O.id,
                       nombre = O.nombre,
                       ciudad = O.ciudad,
                       salario = O.salario
                   };
        }

        public IQueryable obtenerOfertasAdministrador()
        {
            return from O in jobfinder.Set<Oferta>()
                   join G in jobfinder.Set<Gestion>()
                   on O.id equals G.oferta_id
                   select new
                   {
                       id = O.id,
                       nombre = O.nombre,
                       ciudad = O.ciudad,
                       salario = O.salario
                   };
        }

        public Oferta GetOferta(int id)
        {
            try
            {
                Oferta oferta = jobfinder.Ofertas.Find(id);

                return oferta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarOferta(Oferta oferta)
        {
            try
            {
                jobfinder.Ofertas.AddOrUpdate(oferta);
                jobfinder.SaveChanges();
                return "Se modificó la oferta correctamente";
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public void EliminarOferta(int id)
        {
            try
            {
                Oferta oferta = jobfinder.Ofertas.Find(id);
                jobfinder.Ofertas.Remove(oferta);
                jobfinder.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}