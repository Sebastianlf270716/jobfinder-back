using jobfinder_back.clases;
using jobfinder_back.Dto.Request;
using jobfinder_back.Dto.Response;
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

            clsGestion _gestion = new clsGestion();
            _gestion.Insertar(ofertaRequest.usuario_id, oferta_id, ofertaRequest.tipo_perfil);

            return "Se guardó la oferta exitosamente";
        }

        [HttpGet]
        [Route("OfertasEmpleador")]
        public IQueryable obtenerOfertasEmpleador(int id_empleador)
        {
           clsOferta _oferta = new clsOferta();
            
            return _oferta.obtenerOfertasEmpleador(id_empleador);
        }

        [HttpGet]
        [Route("OfertasAdministrador")]
        public IQueryable obtenerOfertasAdministrador()
        {
            clsOferta _oferta = new clsOferta();

            return _oferta.obtenerOfertasAdministrador();
        }

        [HttpGet]
        [Route("Obtener")]
        public OfertaResponse getOferta(int id)
        {
            clsOferta _oferta = new clsOferta();
            Oferta oferta = _oferta.GetOferta(id);

            clsFuncion _funcion = new clsFuncion();
            IQueryable funciones = _funcion.obtenerFuncionesOferta(id);

            OfertaResponse ofertaResponse = new OfertaResponse();

            ofertaResponse.id = oferta.id;
            ofertaResponse.nombre = oferta.nombre;
            ofertaResponse.cargo = oferta.cargo;
            ofertaResponse.ciudad = oferta.ciudad;
            ofertaResponse.salario = oferta.salario;
            ofertaResponse.anios_experiencia = oferta.anios_experiencia;
            ofertaResponse.funciones = funciones;


            return ofertaResponse;
        }

        [HttpPost]
        [Route("Actualizar")]
        public string ActualizarOferta([FromBody] OfertaRequest ofertaRequest)
        {
            Oferta oferta = new Oferta();

            oferta.id = (int)ofertaRequest.id;
            oferta.nombre = ofertaRequest.nombre;
            oferta.cargo = ofertaRequest.cargo;
            oferta.salario = ofertaRequest.salario;
            oferta.ciudad = ofertaRequest.ciudad;
            oferta.anios_experiencia = ofertaRequest.anios_experiencia;

            clsOferta _oferta = new clsOferta();

            foreach (string ofertaFuncion in ofertaRequest.funciones)
            {
                Funcion modelFuncion = new Funcion();

                modelFuncion.descripcion = ofertaFuncion;
                modelFuncion.oferta_id = oferta.id;

                clsFuncion _funcion = new clsFuncion();

                _funcion.Actualizar(modelFuncion);
            }

            return _oferta.ActualizarOferta(oferta);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public void eliminarOferta(int id)
        {
            clsFuncion _funcion = new clsFuncion();
            _funcion.eliminarFuncionesOferta(id);

            clsGestion _gestion = new clsGestion();
            _gestion.eliminarGestion(id);

            clsOferta _oferta = new clsOferta();
            _oferta.EliminarOferta(id);            
        }
        public List<RespuestaOferta> Get()
        {
            clsOferta _oferta = new clsOferta();
            return _oferta.consultarTodas();
        }
        [HttpGet]
        [Route("RegistrarVisita")]
        public void RegistrarVisita(int id)
        {
            clsOferta oferta = new clsOferta();
            oferta.RegistrarVisita(id);
        }

        [HttpGet]
        [Route("RegistrarCandidato")]
        public string RegistrarCandidato(int usuario_id, int oferta_id)
        {
            clsOferta oferta = new clsOferta();
            return oferta.RegistrarCandidato(usuario_id, oferta_id);
        }

        [HttpPost]
        [Route("ConsultarCandidatos")]
        public List<int> ConsultarCandidatos(List<Oferta> ofertas)
        {
            clsOferta oferta = new clsOferta();
            return oferta.ConsultarCandidatos(ofertas);
        }

    }
}