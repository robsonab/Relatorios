﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RelatoriosCongregação.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FieldServiceEntities : DbContext
    {
        public FieldServiceEntities()
            : base("name=FieldServiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Publicadores> Publicadores { get; set; }
        public virtual DbSet<Relatorios> Relatorios { get; set; }
        public virtual DbSet<Tipos> Tipos { get; set; }
    }
}
