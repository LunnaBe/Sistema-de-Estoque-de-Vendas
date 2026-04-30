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
        private static readonly string pastaBase =
        Path.Combine(@"C:\Users\midor\OneDrive\Documentos",
            "ApiVendas");

        private static readonly string caminhoBanco =
            Path.Combine(pastaBase, "estoqueWpf.db");

        private static readonly string connectionString =
            $"Data Source = {caminhoBanco}";

        static Database()
        {
            // Verificar se o banco de dados e a pasta existe
            if (!Directory.Exists(pastaBase))
                Directory.CreateDirectory(pastaBase);
            if (!File.Exists(caminhoBanco))
                MessageBox.Show("Banco de dados inexistente!!!");
        }

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }
    }
}
