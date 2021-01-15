using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_stores_stroeLocationId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_stroeLocationId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "stroeLocationId",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "storeLocationID",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "storeLocationID",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "stroeLocationId",
                table: "orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_stroeLocationId",
                table: "orders",
                column: "stroeLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_stores_stroeLocationId",
                table: "orders",
                column: "stroeLocationId",
                principalTable: "stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
