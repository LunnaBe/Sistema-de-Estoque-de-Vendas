using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiVendas.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Codigo_Fornecedor = table.Column<string>(type: "TEXT", nullable: false),
                    Nome_Produto = table.Column<string>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<double>(type: "REAL", nullable: false),
                    Data_Entrada = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Data_Saida = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoque");
        }
    }
}
