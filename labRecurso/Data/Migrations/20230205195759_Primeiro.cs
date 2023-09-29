using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labRecurso.Data.Migrations
{
    public partial class Primeiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    utilizadorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    saldo = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perfil_AspNetUsers_utilizadorId",
                        column: x => x.utilizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plataforma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataforma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfilCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    perfilId = table.Column<int>(type: "int", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilCategoria_Categoria_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilCategoria_Perfil_perfilId",
                        column: x => x.perfilId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<float>(type: "real", nullable: false),
                    plataformaId = table.Column<int>(type: "int", nullable: false),
                    Pontuacao = table.Column<int>(type: "int", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogos_Categoria_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jogos_Plataforma_plataformaId",
                        column: x => x.plataformaId,
                        principalTable: "Plataforma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avalicao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JogoId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pontuacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avalicao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avalicao_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JogoId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentario_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilJogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    perfilId = table.Column<int>(type: "int", nullable: false),
                    jogoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilJogos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilJogos_Jogos_jogoId",
                        column: x => x.jogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilJogos_Perfil_perfilId",
                        column: x => x.perfilId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avalicao_JogoId",
                table: "Avalicao",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_JogoId",
                table: "Comentario",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_categoriaId",
                table: "Jogos",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_plataformaId",
                table: "Jogos",
                column: "plataformaId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfil_utilizadorId",
                table: "Perfil",
                column: "utilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilCategoria_categoriaId",
                table: "PerfilCategoria",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilCategoria_perfilId",
                table: "PerfilCategoria",
                column: "perfilId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilJogos_jogoId",
                table: "PerfilJogos",
                column: "jogoId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilJogos_perfilId",
                table: "PerfilJogos",
                column: "perfilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avalicao");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "PerfilCategoria");

            migrationBuilder.DropTable(
                name: "PerfilJogos");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Perfil");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Plataforma");
        }
    }
}
