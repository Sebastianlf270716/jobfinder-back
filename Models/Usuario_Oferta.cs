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
    
    public partial class Usuario_Oferta
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public int oferta_id { get; set; }
    
        public virtual Oferta Oferta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
