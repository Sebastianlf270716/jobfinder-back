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
        public List<RespuestaOferta> consultarTodas()
        {
            try
            {
                List<Oferta> ofertas = jobfinder.Ofertas.ToList();
                List<RespuestaOferta> ofertaResponses = new List<RespuestaOferta>();
                foreach (var oferta in ofertas)
                {
                    Gestion gestion = jobfinder.Gestions.FirstOrDefault(g => g.oferta_id == oferta.id);

                    RespuestaOferta ofertaResponse = new RespuestaOferta();
                    ofertaResponse.id = oferta.id;
                    ofertaResponse.empresa = jobfinder.Perfils.Find(jobfinder.Empleadors.Find(gestion.empleador_id).id_perfil).nombre;
                    ofertaResponse.nombre = oferta.nombre;
                    ofertaResponse.cargo = oferta.cargo;
                    ofertaResponse.anios_experiencia = oferta.anios_experiencia;
                    ofertaResponse.ciudad = oferta.ciudad;
                    ofertaResponse.salario = oferta.salario;
                    ofertaResponse.numero_visualizaciones = oferta.numero_visualizaciones;
                    List<Funcion> funciones = jobfinder.Funcions.Where(f => f.oferta_id == oferta.id).ToList();
                    ofertaResponse.funciones = funciones;

                    ofertaResponses.Add(ofertaResponse);
                }
                return ofertaResponses;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void RegistrarVisita(int id)
        {
            try
            {
                Oferta oferta = jobfinder.Ofertas.FirstOrDefault(o => o.id == id);
                oferta.numero_visualizaciones += 1;
                jobfinder.Ofertas.AddOrUpdate(oferta);
                jobfinder.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string RegistrarCandidato(int idUsuario, int idOferta)
        {
            try
            {
                if (jobfinder.Usuario_Oferta.FirstOrDefault(e => e.oferta_id == idOferta && e.usuario_id == idUsuario) == null)
                {
                    Usuario_Oferta usuario_Oferta = new Usuario_Oferta();
                    usuario_Oferta.oferta_id = idOferta;
                    usuario_Oferta.usuario_id = idUsuario;
                    jobfinder.Usuario_Oferta.Add(usuario_Oferta);
                    jobfinder.SaveChanges();
                    return "Ha aplicado a la oferta exitosamente";
                }
                else
                {
                    return "No puede aplicar 2 veces a una misma oferta";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<int> ConsultarCandidatos(List<Oferta> ofertas)
        {
            List<int> candidatos = new List<int>();
            foreach (var oferta in ofertas)
            {
                List<Usuario_Oferta> usuario_Ofertas = jobfinder.Usuario_Oferta.Where(uo => uo.oferta_id==oferta.id).ToList();
                candidatos.Add(usuario_Ofertas.Count);
            }
            return candidatos;
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

            reporteOferta.pVsV = Math.Round(((decimal)reporteOferta.candidatos / (decimal)reporteOferta.visualizaciones) * 100, 2);

            return reporteOferta;

        }

    }

    public class RespuestaOferta
    {
        public int id;
        public string empresa;
        public string nombre;
        public string cargo;
        public int anios_experiencia;
        public decimal salario;
        public string ciudad;
        public int numero_visualizaciones;
        public List<Funcion> funciones;

    }

}