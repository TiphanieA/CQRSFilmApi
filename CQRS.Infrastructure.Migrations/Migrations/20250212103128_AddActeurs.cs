using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRS.Infrastructure.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddActeurs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Acteurs",
                columns: new[] { "Id", "Nom", "Prenom" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Yeoh", "Michelle" },
                    { Guid.NewGuid(), "Pitt", "Brad" },
                    { Guid.NewGuid(), "Ortega", "Jenna" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
