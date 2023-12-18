using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaTelefonicaBack.Migrations
{
    public partial class Relacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Contacto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contacto_UsuarioId",
                table: "Contacto",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacto_Usuario_UsuarioId",
                table: "Contacto",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacto_Usuario_UsuarioId",
                table: "Contacto");

            migrationBuilder.DropIndex(
                name: "IX_Contacto_UsuarioId",
                table: "Contacto");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Contacto");
        }
    }
}
