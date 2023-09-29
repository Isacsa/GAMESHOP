using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labRecurso.Data.Migrations
{
    public partial class acabando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Jogos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Jogos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
