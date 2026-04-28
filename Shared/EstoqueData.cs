using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class EstoqueData
    {
        public int Id { get; set; }
        public string Codigo_Fornecedor { get; set; }
        public string Nome_Produto { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public DateTime Data_Entrada { get; set; }
        public DateTime Data_Saida { get; set; }
    }
}
