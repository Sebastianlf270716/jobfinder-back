using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.Dto.Response
{
    public class ReporteOferta
    {
        public string nombre;
        public int candidatos;
        public int visualizaciones;
        public decimal pVsV;
        public int masculino;
        public int femenino;
        public int otro;
    }
}