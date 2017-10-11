using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlugaLivros.Data.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    AutorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.AutorID);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    LivroID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Foto = table.Column<string>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.LivroID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "LivroAutor",
                columns: table => new
                {
                    AutorID = table.Column<int>(nullable: false),
                    LivroID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroAutor", x => new { x.AutorID, x.LivroID });
                    table.ForeignKey(
                        name: "FK_LivroAutor_Autor_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Autor",
                        principalColumn: "AutorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroAutor_Livro_LivroID",
                        column: x => x.LivroID,
                        principalTable: "Livro",
                        principalColumn: "LivroID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emprestimo",
                columns: table => new
                {
                    EmprestimoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataDevolucao = table.Column<string>(nullable: true),
                    DataFim = table.Column<string>(nullable: true),
                    DataInicio = table.Column<string>(nullable: true),
                    UsuarioID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimo", x => x.EmprestimoID);
                    table.ForeignKey(
                        name: "FK_Emprestimo_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivroEmprestimo",
                columns: table => new
                {
                    LivroID = table.Column<int>(nullable: false),
                    EmprestimoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroEmprestimo", x => new { x.LivroID, x.EmprestimoID });
                    table.ForeignKey(
                        name: "FK_LivroEmprestimo_Emprestimo_EmprestimoID",
                        column: x => x.EmprestimoID,
                        principalTable: "Emprestimo",
                        principalColumn: "EmprestimoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroEmprestimo_Livro_LivroID",
                        column: x => x.LivroID,
                        principalTable: "Livro",
                        principalColumn: "LivroID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimo_UsuarioID",
                table: "Emprestimo",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutor_AutorID",
                table: "LivroAutor",
                column: "AutorID");

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutor_LivroID",
                table: "LivroAutor",
                column: "LivroID");

            migrationBuilder.CreateIndex(
                name: "IX_LivroEmprestimo_EmprestimoID",
                table: "LivroEmprestimo",
                column: "EmprestimoID");

            migrationBuilder.CreateIndex(
                name: "IX_LivroEmprestimo_LivroID",
                table: "LivroEmprestimo",
                column: "LivroID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivroAutor");

            migrationBuilder.DropTable(
                name: "LivroEmprestimo");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Emprestimo");

            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
