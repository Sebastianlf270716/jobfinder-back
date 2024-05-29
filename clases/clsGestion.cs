using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsGestion
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public void Insertar(int id_usuario, int id_oferta, string tipo_perfil)
        {
            Gestion gestion = new Gestion();
            try
            {
                if(tipo_perfil == "Empleador")
                {
                    gestion.empleador_id = id_usuario;
                    gestion.administrador_id = 1;
                }
                gestion.oferta_id = id_oferta;
                jobfinder.Gestions.Add(gestion);
                jobfinder.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarGestion(int id_oferta)
        {
            var gestiones = jobfinder.Gestions.Where(g => g.oferta_id == id_oferta);
            jobfinder.Gestions.RemoveRange(gestiones);
            jobfinder.SaveChanges();
        }
    }
}