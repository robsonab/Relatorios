namespace RelatoriosCongregação.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FieldServiceEntities : DbContext
    {
        public FieldServiceEntities()
            : base("name=FieldServiceModel")
        {
        }

        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<Publicadores> Publicadores { get; set; }
        public virtual DbSet<Relatorios> Relatorios { get; set; }
        public virtual DbSet<Tipos> Tipos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grupos>()
                .Property(e => e.Grupo)
                .IsUnicode(false);

            modelBuilder.Entity<Grupos>()
                .Property(e => e.Dirigente)
                .IsUnicode(false);

            modelBuilder.Entity<Grupos>()
                .HasMany(e => e.Publicadores)
                .WithRequired(e => e.Grupo)
                .HasForeignKey(e => e.IdGrupo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Publicadores>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Publicadores>()
                .HasMany(e => e.Relatorios)
                .WithRequired(e => e.Publicadores)
                .HasForeignKey(e => e.IdPublicador)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Relatorios>()
                .Property(e => e.AnoMes)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tipos>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Tipos>()
                .HasMany(e => e.Publicadores)
                .WithRequired(e => e.Tipos)
                .HasForeignKey(e => e.IdTipo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tipos>()
                .HasMany(e => e.Relatorios)
                .WithRequired(e => e.Tipos)
                .HasForeignKey(e => e.IdTipo)
                .WillCascadeOnDelete(false);
        }


    }
}
