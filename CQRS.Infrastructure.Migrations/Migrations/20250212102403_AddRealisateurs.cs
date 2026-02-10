using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRS.Infrastructure.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddRealisateurs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Realisateurs",
                columns: new[] { "Id", "Nom", "Prenom" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Tarantino", "Quentin" },
                    { Guid.NewGuid(), "Spielberg", "Steven" },
                    { Guid.NewGuid(), "Bigelow", "Kathryn" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
