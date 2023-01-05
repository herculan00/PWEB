using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWEB.Data.Migrations
{
    public partial class m4_reserva_impresaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Empresas_EmpresaId",
                table: "Reserva");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Reserva",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Empresas_EmpresaId",
                table: "Reserva",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Empresas_EmpresaId",
                table: "Reserva");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Reserva",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Empresas_EmpresaId",
                table: "Reserva",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }
    }
}
