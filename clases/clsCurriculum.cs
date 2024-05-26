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
        public IQueryable ConsultarEstudios(int id)
        {
            Curriculum curriculum = jobfinder.Curricula.Find(id);
            if (curriculum == null)
            {
                return null;
            }
            return from C in jobfinder.Set<Curriculum>()
                   join E in jobfinder.Set<Estudio>()
                   on C.id equals E.curriculum_id
                   where C.id == curriculum.id
                   select new
                   {
                       institucion = E.institucion,
                       anio = E.anio,
                       titulo = E.titulo
                   };

        }
        public IQueryable ConsultarExperiencias(int id)
        {
            Curriculum curriculum = jobfinder.Curricula.Find(id);
            if (curriculum == null)
            {
                return null;
            }
            return from C in jobfinder.Set<Curriculum>()
                   join E in jobfinder.Set<Experiencia>()
                   on C.id equals E.curriculum_id
                   where C.id == curriculum.id
                   select new
                   {
                       institucion = E.empresa,
                       anio = E.anios,
                       titulo = E.cargo
                   };

        }
    }
}