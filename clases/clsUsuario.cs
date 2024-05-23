using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.clases
{
    public class clsUsuario
    {
        private jobfinderEntities jobfinder = new jobfinderEntities();

        public string Insertar(Usuario usuario)
        {
            try
            {
                jobfinder.Usuarios.Add(usuario);
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