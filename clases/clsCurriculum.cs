using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsCurriculum
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public int Insertar(Curriculum curriculum)
        {
            try
            {
                jobfinder.Curricula.Add(curriculum);
                jobfinder.SaveChanges();

                return curriculum.id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}