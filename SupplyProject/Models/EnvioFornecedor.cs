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
    
    public partial class EnvioFornecedor
    {
        public int idEnvio { get; set; }
        public int idPedido { get; set; }
        public int idVeiculo { get; set; }
        public int statusEnvio { get; set; }
        public int ano_envio { get; set; }
        public int mes_envio { get; set; }
        public int dia_envio { get; set; }
    
        public virtual PedidoFinal_usuario PedidoFinal_usuario { get; set; }
        public virtual StatusEnvioFornecedor StatusEnvioFornecedor { get; set; }
        public virtual Veiculo Veiculo { get; set; }
    }
}