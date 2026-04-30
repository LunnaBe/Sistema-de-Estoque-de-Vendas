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
    internal class EstoqueRepository
    {
        // Método para inserir dados na tabela Estoque
        public void InserirDados(EstoqueDataDTO estoqueData)
        {
            //pegando a conexão com o banco de dados
            using var conn = Database.GetConnection();
            conn.Open(); //Abrindo a conexão

            //passando parametros para o comando
            var cmd = new SqliteCommand(
            @"INSERT INTO Estoque (codigo_fornecedor, nome_produto, quantidade, preço, data_entrada, data_saida)", conn);

            // Parâmetros para evitar SQL Injection
            cmd.Parameters.AddWithValue("@codigoFornecedor", estoqueData.Codigo_Fornecedor);
            cmd.Parameters.AddWithValue("@nome", estoqueData.Nome_Produto);
            cmd.Parameters.AddWithValue("@quantidade", estoqueData.Quantidade);
            cmd.Parameters.AddWithValue("@preco", estoqueData.Preco);
            cmd.Parameters.AddWithValue("@dataEntrada", estoqueData.Data_Entrada.ToString("s"));
            cmd.Parameters.AddWithValue("@dataSaida", estoqueData.Data_Saida.ToString("s"));

            cmd.ExecuteNonQuery();
        }

        public List<EstoqueDataDTO> ListarDados()
        {
            var lista = new List<EstoqueDataDTO>();

            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = new SqliteCommand("SELECT id, codigo_fornecedor, nome_produto, quantidade, preço, data_entrada, data_saida FROM Estoque", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new EstoqueDataDTO
                {
                    Id = reader.GetInt32(0),
                    Codigo_Fornecedor = reader.GetString(1),
                    Nome_Produto = reader.GetString(2),
                    Quantidade = reader.GetInt32(3),
                    Preco = reader.GetDouble(4),
                    Data_Entrada = DateTime.Parse(reader.GetString(5)),
                    Data_Saida = DateTime.Parse(reader.GetString(6))
                };
                lista.Add(item);
            }
            return lista;
        }
    }
}
