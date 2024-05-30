using jobfinder_back.Dto.Response;
using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
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

        public ReporteOferta estadisticaOferta(int id)
        {
            Oferta oferta = new Oferta();
            ReporteOferta reporteOferta = new ReporteOferta();

            oferta = jobfinder.Ofertas.Find(id);

            reporteOferta.nombre = oferta.nombre;

            var candidatos = jobfinder.Usuario_Oferta.Where(c => c.oferta_id == id);

            reporteOferta.candidatos = candidatos.Count();

            int masculino = 0;
            int femenino = 0;
            int otro = 0;

            foreach (var candidato in candidatos)
            {
                Usuario usuario = jobfinder.Usuarios.Find(candidato.usuario_id);

                switch (usuario.genero)
                {
                    case "Masculino":
                        masculino++;
                        break;
                    case "Femenino":
                        femenino++;
                        break;
                    case "Otro":
                        otro++;
                        break;
                    default:
                        break;
                }

            }

            reporteOferta.visualizaciones = oferta.numero_visualizaciones;
            reporteOferta.masculino = masculino;
            reporteOferta.femenino = femenino;
            reporteOferta.otro = otro;

            reporteOferta.pVsV = (reporteOferta.candidatos / reporteOferta.visualizaciones) * 100;

            return reporteOferta;

        }


    }
}