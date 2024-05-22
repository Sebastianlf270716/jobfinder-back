using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsPerfil
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public int Insertar(Perfil perfil)
        {
            try
            {
                jobfinder.Perfils.Add(perfil);
                jobfinder.SaveChanges();

                return perfil.id_perfil;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}