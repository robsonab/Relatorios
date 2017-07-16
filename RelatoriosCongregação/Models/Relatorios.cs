using System.ComponentModel.DataAnnotations;

namespace RelatoriosCongregação.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Relatorios
    {
        public int Id { get; set; }
        public string AnoMes { get; set; }
      
        [NotMapped]          
        public int Ano { get; set; }

        [NotMapped]
        public int Mes { get; set; }

        [Display(Description = "Publicador")]        
        public int IdPublicador { get; set; }

        public int IdTipo { get; set; }
        public int Horas { get; set; }
        public int Videos { get; set; }

        [Display(Name = "Publicações")]
        public int Publicacoes { get; set; }
        public int Revisitas { get; set; }
        public int Estudos { get; set; }

        public virtual Publicadores Publicadores { get; set; }
        public virtual Tipos Tipos { get; set; }
    }
}
