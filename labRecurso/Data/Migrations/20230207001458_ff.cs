using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labRecurso.Data.Migrations
{
    public partial class ff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Categoria");

            migrationBuilder.AlterColumn<float>(
                name: "Preco",
                table: "Jogos",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Jogos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Jogos");

            migrationBuilder.AlterColumn<double>(
                name: "Preco",
                table: "Jogos",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
