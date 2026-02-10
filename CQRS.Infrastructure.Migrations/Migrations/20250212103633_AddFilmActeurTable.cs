using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRS.Infrastructure.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddFilmActeurTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActeurFilm",
                columns: table => new
                {
                    ActeursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActeurFilm", x => new { x.ActeursId, x.FilmsId });
                    table.ForeignKey(
                        name: "FK_ActeurFilm_Acteurs_ActeursId",
                        column: x => x.ActeursId,
                        principalTable: "Acteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActeurFilm_Films_FilmsId",
                        column: x => x.FilmsId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActeurFilm_FilmsId",
                table: "ActeurFilm",
                column: "FilmsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActeurFilm");
        }
    }
}
