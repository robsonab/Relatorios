//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Publicadores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Publicadores()
        {
            this.Relatorios = new HashSet<Relatorios>();
        }
    
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdGrupo { get; set; }
        public int IdTipo { get; set; }
        public bool Ativo { get; set; }
    
        public virtual Tipos Tipos { get; set; }

        public virtual Grupos Grupo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relatorios> Relatorios { get; set; }
    }
}
