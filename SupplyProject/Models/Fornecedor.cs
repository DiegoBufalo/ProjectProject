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
    
    public partial class Fornecedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fornecedor()
        {
            this.Produto_fornecedor = new HashSet<Produto_fornecedor>();
        }
    
        public int idFornecedor { get; set; }
        public string nome_fornecedor { get; set; }
        public string cnpj_fornecedor { get; set; }
        public string logradouro_fornecedor { get; set; }
        public string numlogradouro_fornecedor { get; set; }
        public string telefone_fornecedor { get; set; }
        public string email_fornecedor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produto_fornecedor> Produto_fornecedor { get; set; }
    }
}