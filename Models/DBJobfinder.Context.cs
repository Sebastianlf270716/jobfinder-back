﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class jobfinderEntities : DbContext
    {
        public jobfinderEntities()
            : base("name=jobfinderEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administrador> Administradors { get; set; }
        public virtual DbSet<Curriculum> Curricula { get; set; }
        public virtual DbSet<Empleador> Empleadors { get; set; }
        public virtual DbSet<Estudio> Estudios { get; set; }
        public virtual DbSet<Experiencia> Experiencias { get; set; }
        public virtual DbSet<Funcion> Funcions { get; set; }
        public virtual DbSet<Gestion> Gestions { get; set; }
        public virtual DbSet<Oferta> Ofertas { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Usuario_Oferta> Usuario_Oferta { get; set; }
    }
}
