using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;

namespace jobfinder_back.Dto.Response
{
    public class OfertaResponse
    {
        public int id;
        public string nombre;
        public string cargo;
        public decimal salario;
        public string ciudad;
        public int anios_experiencia;
        public IQueryable funciones;
    }
}