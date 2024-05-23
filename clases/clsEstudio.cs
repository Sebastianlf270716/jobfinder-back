using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsEstudio
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public void Insertar(Estudio estudio)
        {
            try
            {
                jobfinder.Estudios.Add(estudio);
                jobfinder.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}