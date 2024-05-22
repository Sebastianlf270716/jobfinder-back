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
    }
}
