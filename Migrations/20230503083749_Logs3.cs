using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiTienda.Migrations
{
    /// <inheritdoc />
    public partial class Logs3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "LogDb",
                type: "nvarchar(max)",
                maxLength: 8000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Evento",
                table: "LogDb",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "LogDb");

            migrationBuilder.DropColumn(
                name: "Evento",
                table: "LogDb");
        }
    }
}
