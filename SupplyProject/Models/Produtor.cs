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
    
    public partial class Produtor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produtor()
        {
            this.Produto_produtor = new HashSet<Produto_produtor>();
        }
    
        public int idProdutor { get; set; }
        public string nome_produtor { get; set; }
        public string logradouro_produtor { get; set; }
        public string numlogradouro_produtor { get; set; }
        public string cnpj_produtor { get; set; }
        public string telefone_produtor { get; set; }
        public string email_produtor { get; set; }
        public string CEP { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string pais { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produto_produtor> Produto_produtor { get; set; }
    }
}
