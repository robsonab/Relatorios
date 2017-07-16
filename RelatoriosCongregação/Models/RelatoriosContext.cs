using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RelatoriosCongregação.Models
{
    public class RelatoriosContext : DbContext
    {
        public RelatoriosContext(): base("name=CampoEntities")
        {
                
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Grupos> Grupos { get; set; }
    }
}