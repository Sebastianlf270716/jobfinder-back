//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jobfinder_back.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Estudio
    {
        public int id { get; set; }
        public string institucion { get; set; }
        public int anio { get; set; }
        public string titulo { get; set; }
        public int curriculum_id { get; set; }
    
        public virtual Curriculum Curriculum { get; set; }
    }
}
