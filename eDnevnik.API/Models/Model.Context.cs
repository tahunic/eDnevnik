﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eDnevnik.API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class eDnevnikEntities : DbContext
    {
        public eDnevnikEntities()
            : base("name=eDnevnikEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AkademskaGodina> AkademskaGodina { get; set; }
        public virtual DbSet<Cas> Cas { get; set; }
        public virtual DbSet<Izostanak> Izostanak { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Nastavnik> Nastavnik { get; set; }
        public virtual DbSet<Ocjena> Ocjena { get; set; }
        public virtual DbSet<Predmet> Predmet { get; set; }
        public virtual DbSet<Razred> Razred { get; set; }
        public virtual DbSet<Ucenik> Ucenik { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
