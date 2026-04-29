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
        public required int Id { get; set; }
        public required string Codigo_Fornecedor { get; set; }
        public required string Nome_Produto { get; set; }
        public required int Quantidade { get; set; }
        public required double Preco { get; set; }
        public required DateTime Data_Entrada { get; set; }
        public required DateTime Data_Saida { get; set; }
    }
}
