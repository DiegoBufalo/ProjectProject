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
    
    public partial class Produto_fornecedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produto_fornecedor()
        {
            this.PedidoFinal_usuario = new HashSet<PedidoFinal_usuario>();
        }
    
        public int idProduto_fornecedor { get; set; }
        public string nome_prodF { get; set; }
        public double preco_prodF { get; set; }
        public double peso_prodF { get; set; }
        public double largura_prodF { get; set; }
        public double altura_prodF { get; set; }
        public double profundidade_prodF { get; set; }
        public int quantidade_prodF { get; set; }
        public int tempo_producaoF { get; set; }
        public int Fornecedor_idFornecedor { get; set; }
    
        public virtual Fornecedor Fornecedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PedidoFinal_usuario> PedidoFinal_usuario { get; set; }
    }
}
