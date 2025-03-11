using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugBunty_Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Niveau_Vulnerabilite = table.Column<int>(type: "int", nullable: false),
                    Prix_Severite = table.Column<int>(type: "int", nullable: false),
                    Statut = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projet_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetailProjet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Explication_Globale = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Regles = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Emplacement_Bug = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Vilnerabilite_Haute = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Vilnerabilite_Moyenne = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Vilnerabilite_Basse = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ProjetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailProjet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailProjet_Projet_ProjetId",
                        column: x => x.ProjetId,
                        principalTable: "Projet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rapport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRapport = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProjetId = table.Column<int>(type: "int", nullable: false),
                    UserChercheurId = table.Column<int>(type: "int", nullable: false),
                    UserAdminId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rapport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rapport_Projet_ProjetId",
                        column: x => x.ProjetId,
                        principalTable: "Projet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rapport_Users_UserAdminId",
                        column: x => x.UserAdminId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rapport_Users_UserChercheurId",
                        column: x => x.UserChercheurId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailProjet_ProjetId",
                table: "DetailProjet",
                column: "ProjetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projet_UserId",
                table: "Projet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rapport_ProjetId",
                table: "Rapport",
                column: "ProjetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rapport_UserAdminId",
                table: "Rapport",
                column: "UserAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Rapport_UserChercheurId",
                table: "Rapport",
                column: "UserChercheurId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailProjet");

            migrationBuilder.DropTable(
                name: "Rapport");

            migrationBuilder.DropTable(
                name: "Projet");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
