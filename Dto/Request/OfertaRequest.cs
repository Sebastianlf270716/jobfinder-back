﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.Dto.Request
{
    public class OfertaRequest
    {   
        public int? id {  get; set; }
        public string nombre { get; set; }
        public string cargo { get; set; }
        public decimal salario { get; set; }
        public string ciudad { get; set; }
        public int anios_experiencia { get; set; }
        public List<string> funciones { get; set; }
        public int usuario_id { get; set; }
        public string tipo_perfil {  get; set; }

    }
}