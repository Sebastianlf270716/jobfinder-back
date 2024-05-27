using jobfinder_back.clases;
using jobfinder_back.Dto.Request;
using jobfinder_back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace jobfinder_back.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Oferta")]
    public class OfertaController : ApiController
    {
        [HttpPost]
        [Route("Crear")]
        public string CrearOferta([FromBody] OfertaRequest ofertaRequest)
        {
            Oferta oferta = new Oferta();

            oferta.nombre = ofertaRequest.nombre;
            oferta.cargo = ofertaRequest.cargo;
            oferta.salario = ofertaRequest.salario;
            oferta.ciudad = ofertaRequest.ciudad;
            oferta.anios_experiencia = ofertaRequest.anios_experiencia;
            oferta.numero_visualizaciones = 0;

            clsOferta _oferta = new clsOferta();
            int oferta_id = _oferta.Insertar(oferta);

            foreach (string ofertaFuncion in ofertaRequest.funciones)
            {
                Funcion modelFuncion = new Funcion();

                modelFuncion.descripcion = ofertaFuncion;
                modelFuncion.oferta_id = oferta_id;

                clsFuncion _funcion = new clsFuncion();

                _funcion.Insertar(modelFuncion);
            }

            return "Se guardó la oferta exitosamente";
        }
    }
}