using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelatoriosCongregação.Models
{
    [Table("Grupos")]
    public class Grupos
    {
        [Key(), DatabaseGenerated( DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        [StringLength(50), Required(AllowEmptyStrings = false, ErrorMessage = "Nome do grupo obrigatório")]
        public string Grupo { get; set; }

        [StringLength(50), Required(AllowEmptyStrings = false, ErrorMessage = "Nome do Dirigente obrigatório")]
        public string Dirigente { get; set; }                
        public virtual ICollection<Publicadores> Publicadores { get; set; }
    }
}