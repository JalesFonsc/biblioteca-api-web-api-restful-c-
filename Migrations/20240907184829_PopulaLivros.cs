using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulaLivros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO LIVROS(TITULO, AUTORID) VALUES ('Senhor dos Anéis', 1)");
            migrationBuilder.Sql("INSERT INTO LIVROS(TITULO, AUTORID) VALUES ('Os Quatro Amores', 2)");
            migrationBuilder.Sql("INSERT INTO LIVROS(TITULO, AUTORID) VALUES ('Memórias Póstumas de Brás Cubas', 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
