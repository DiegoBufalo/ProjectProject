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
    
    public partial class StatusDemanda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StatusDemanda()
        {
            this.DemandaFinal_produtor = new HashSet<DemandaFinal_produtor>();
        }
    
        public int idDemandaFinal { get; set; }
        public string nome_status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DemandaFinal_produtor> DemandaFinal_produtor { get; set; }
    }
}
