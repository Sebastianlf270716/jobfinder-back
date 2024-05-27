using jobfinder_back.clases;
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
    [RoutePrefix("api/Curriculum")]
    public class CurriculumController : ApiController
    {
        [HttpGet]
        [Route("ConsultarEstudios")]
        public IQueryable ConsultarEstudios(int id)
        {
            clsCurriculum _curriculum = new clsCurriculum();
            return _curriculum.ConsultarEstudios(id);
        }

        [HttpGet]
        [Route("ConsultarExperiencias")]
        public IQueryable ConsultarExperiencias(int id)
        {
            clsCurriculum _curriculum = new clsCurriculum();
            return _curriculum.ConsultarExperiencias(id);
        }
    }
}