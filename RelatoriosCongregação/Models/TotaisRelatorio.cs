using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RelatoriosCongregação.Models
{
    public class TotaisRelatorio
    {
        public string Tipo { get; set; }

        [Display(ShortName = "Qtd. Public.")]
        public int QtdPublicadores { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int Horas { get; set; }
        public int Videos { get; set; }
        public int Publicacoes { get; set; }
        public int Revisitas { get; set; }
        public int Estudos { get; set; }
    }

    public class RelatorioGeral
    {
        public IQueryable<TotaisRelatorio> Totais { get; set; }
        public IQueryable<Relatorios> Detalhado { get; set; }
        public List<PublicadoresPorGrupo> Irregulares { get; set; }
    }

    public class PublicadoresPorGrupo
    {
        public string Dirigente { get; set; }
        public List<string> Publicadores { get; set; }
    }
}