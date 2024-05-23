using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobfinder_back.Dto.Request
{
    public class CurriculumRequest
    {
        public List<EstudioRequest> estudios;
        public List<ExperienciaRequest> experiencias;
    }
}