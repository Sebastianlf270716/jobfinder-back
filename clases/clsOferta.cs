﻿using jobfinder_back.Models;
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
                    ofertaResponse.ciudad = oferta.ciudad;
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