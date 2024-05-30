using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsFuncion
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public void Insertar(Funcion funcion)
        {
            try
            {
                jobfinder.Funcions.Add(funcion);
                jobfinder.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable obtenerFuncionesOferta(int id_oferta)
        {
            return from F in jobfinder.Set<Funcion>()
                   where F.oferta_id == id_oferta
                   select new
                   {
                       descripcion = F.descripcion
                   };
        }

        public void Actualizar(Funcion funcion)
        {
            jobfinder.Funcions.AddOrUpdate(funcion);
            jobfinder.SaveChanges();
        }

        public void Eliminar(Funcion funcion)
        {
            jobfinder.Funcions.Remove(funcion);
            jobfinder.SaveChanges();
        }

        public void eliminarFuncionesOferta(int id_oferta)
        {
            var funciones = jobfinder.Funcions.Where(f => f.oferta_id == id_oferta);
            jobfinder.Funcions.RemoveRange(funciones);
            jobfinder.SaveChanges();
        }
    }
}