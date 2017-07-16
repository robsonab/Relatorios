namespace RelatoriosCongregação.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Grupos> Grupos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grupos>()
                .Property(e => e.Grupo)
                .IsUnicode(false);

            modelBuilder.Entity<Grupos>()
                .Property(e => e.Dirigente)
                .IsUnicode(false);
        }
    }
}
