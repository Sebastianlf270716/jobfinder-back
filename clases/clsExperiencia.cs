using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsExperiencia
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public void Insertar(Experiencia experiencia)
        {
            try
            {
                jobfinder.Experiencias.Add(experiencia);
                jobfinder.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}