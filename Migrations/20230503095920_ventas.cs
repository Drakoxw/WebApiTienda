using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiTienda.Migrations
{
    /// <inheritdoc />
    public partial class ventas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Usuarios_ClienteId",
                table: "Ventas",
                column: "ClienteId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Usuarios_ClienteId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas");
        }
    }
}
