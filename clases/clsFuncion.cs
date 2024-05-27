using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}