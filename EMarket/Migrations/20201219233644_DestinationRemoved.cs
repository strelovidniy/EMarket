using Microsoft.EntityFrameworkCore.Migrations;

namespace EMarket.Migrations
{
    public partial class DestinationRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Destinations_DestinationId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DestinationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DestinationId",
                table: "Orders",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Destinations_DestinationId",
                table: "Orders",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
