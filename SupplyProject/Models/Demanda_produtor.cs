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
    
    public partial class Demanda_produtor
    {
        public int idDemanda { get; set; }
        public int Produto_produtor_idProduto_produtor { get; set; }
        public int Produto_armazem_idProduto_armazem { get; set; }
        public int Usuario_idUsuario { get; set; }
        public int ano_pedido { get; set; }
        public int mes_pedido { get; set; }
        public int dia_pedido { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        public virtual Produto_armazem Produto_armazem { get; set; }
        public virtual Produto_produtor Produto_produtor { get; set; }
    }
}