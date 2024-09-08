using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulaAutores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AUTORES(NOME, SOBRENOME) VALUES ('John', 'Tolkien')");
            migrationBuilder.Sql("INSERT INTO AUTORES(NOME, SOBRENOME) VALUES ('Clive', 'Lewis')");
            migrationBuilder.Sql("INSERT INTO AUTORES(NOME, SOBRENOME) VALUES ('Machado', 'Assis')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AUTORES");
        }
    }
}
