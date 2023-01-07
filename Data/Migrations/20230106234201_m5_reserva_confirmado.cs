using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWEB.Data.Migrations
{
    public partial class m5_reserva_confirmado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmado",
                table: "Reserva",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmado",
                table: "Reserva");
        }
    }
}
