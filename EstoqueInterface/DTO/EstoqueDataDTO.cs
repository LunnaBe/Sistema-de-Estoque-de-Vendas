using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVendas.Models
{
    public class EstoqueDataDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Codigo_Fornecedor { get; set; }
        public string Nome_Produto { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public DateTime Data_Entrada { get; set; }
        public DateTime Data_Saida { get; set; }
    }
}
