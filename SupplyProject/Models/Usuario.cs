//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupplyProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Demanda_produtor = new HashSet<Demanda_produtor>();
            this.Pedido_usuario = new HashSet<Pedido_usuario>();
            this.Produto_armazem = new HashSet<Produto_armazem>();
        }
    
        public int idUsuario { get; set; }
        public string nome_usuario { get; set; }
        public string tipo_usuario { get; set; }
        public string senha_usuario { get; set; }
        public string email_usuario { get; set; }
        public string telefone_usuario { get; set; }
        public int Armazem_idArmazem { get; set; }
    
        public virtual Armazem Armazem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Demanda_produtor> Demanda_produtor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido_usuario> Pedido_usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produto_armazem> Produto_armazem { get; set; }
    }
}