using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EstoqueInterface.Context
{
    
    public static class Database
    {
        // Define o caminho do banco de dados
        private static readonly string dbPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "estoqueWPF.db");

        // Método que cria e abre uma conexão com o SQLite
        public static SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            return conn;
        }

        // Inicializa o banco de dados criando tabelas e indices
        public static void Initialize()
        {
            using var conn = GetConnection();
            using var cmd = conn.CreateCommand();

            try
            {
                // Script do SQLite
                cmd.CommandText = @"
            PRAGMA foreign_keys = ON;
            CREATE TABLE IF NOT EXISTS Estoque (
                Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                Codigo_Fornecedor TEXT NOT NULL,
                Nome_Produto TEXT NOT NULL,
                Quantidade INTEGER NOT NULL,
                Preco REAL NOT NULL,
                Data_Entrada TEXT NOT NULL,
                Data_Saida TEXT NOT NULL
            );";

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Log ou rethrow para o OnStartup pegar
                throw new Exception("Falha ao criar tabelas: " + ex.Message);
            }
        }
        
    }
}
