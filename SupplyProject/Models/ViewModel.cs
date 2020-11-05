using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupplyProject.Models
{
    public class ViewModel
    {
        public List<NotasDto> Notas { get;set; }
        public List<Fornecedor> Fornecedor { get; set; }
    }
}