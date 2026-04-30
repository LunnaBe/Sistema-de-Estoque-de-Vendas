using ApiVendas.Models;
using EstoqueInterface.Context;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueInterface.Repositories
{
    public class EstoqueRepository
    {
        // Método para inserir dados na tabela Estoque
        public void InserirDados(EstoqueDataDTO estoqueData)
        {
            using var conn = Database.GetConnection();

            // Comando SQL para inserir dados. 
            var sql = @"INSERT INTO Estoque 
                (Codigo_Fornecedor, Nome_Produto, Quantidade, Preco, Data_Entrada, Data_Saida) 
                VALUES 
                (@codigoFornecedor, @nome, @quantidade, @preco, @dataEntrada, @dataSaida)";

            using var cmd = new SqliteCommand(sql, conn);

            // Adiciona os parâmetros para evitar SQL Injection
            cmd.Parameters.AddWithValue("@codigoFornecedor", estoqueData.Codigo_Fornecedor);
            cmd.Parameters.AddWithValue("@nome", estoqueData.Nome_Produto);
            cmd.Parameters.AddWithValue("@quantidade", estoqueData.Quantidade);
            cmd.Parameters.AddWithValue("@preco", estoqueData.Preco);
            cmd.Parameters.AddWithValue("@dataEntrada", estoqueData.Data_Entrada.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@dataSaida", estoqueData.Data_Saida.ToString("yyyy-MM-dd HH:mm:ss"));

            cmd.ExecuteNonQuery();
        }

        public List<EstoqueDataDTO> ListarDados()
        {
            var lista = new List<EstoqueDataDTO>();

            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = new SqliteCommand("SELECT codigo_fornecedor, nome_produto, quantidade, preço, data_entrada, data_saida FROM Estoque", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new EstoqueDataDTO
                {
                    Codigo_Fornecedor = reader.GetString(0),
                    Nome_Produto = reader.GetString(1),
                    Quantidade = reader.GetInt32(2),
                    Preco = reader.GetDouble(3),
                    Data_Entrada = DateTime.Parse(reader.GetString(4)),
                    Data_Saida = DateTime.Parse(reader.GetString(5))
                };
                lista.Add(item);
            }
            return lista;
        }
    }
}
