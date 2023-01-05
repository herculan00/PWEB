using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWEB.Data.Migrations
{
    public partial class m3_reservaIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Avaliacoes_AvaliacaoId",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Entregas_EntregaId",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Recolhas_RecolhaId",
                table: "Reserva");

            migrationBuilder.AlterColumn<int>(
                name: "RecolhaId",
                table: "Reserva",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EntregaId",
                table: "Reserva",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AvaliacaoId",
                table: "Reserva",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Avaliacoes_AvaliacaoId",
                table: "Reserva",
                column: "AvaliacaoId",
                principalTable: "Avaliacoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Entregas_EntregaId",
                table: "Reserva",
                column: "EntregaId",
                principalTable: "Entregas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Recolhas_RecolhaId",
                table: "Reserva",
                column: "RecolhaId",
                principalTable: "Recolhas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Avaliacoes_AvaliacaoId",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Entregas_EntregaId",
                table: "Reserva");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Recolhas_RecolhaId",
                table: "Reserva");

            migrationBuilder.AlterColumn<int>(
                name: "RecolhaId",
                table: "Reserva",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EntregaId",
                table: "Reserva",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AvaliacaoId",
                table: "Reserva",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Avaliacoes_AvaliacaoId",
                table: "Reserva",
                column: "AvaliacaoId",
                principalTable: "Avaliacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Entregas_EntregaId",
                table: "Reserva",
                column: "EntregaId",
                principalTable: "Entregas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Recolhas_RecolhaId",
                table: "Reserva",
                column: "RecolhaId",
                principalTable: "Recolhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
