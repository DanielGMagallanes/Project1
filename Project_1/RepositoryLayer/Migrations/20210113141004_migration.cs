using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<int>(type: "int", nullable: false),
                    Owner_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cart = table.Column<int>(type: "int", nullable: false),
                    amountPicked = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    favstore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addmin = table.Column<bool>(type: "bit", nullable: false),
                    ByteArrayImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Customer_Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsAtStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_TO_S = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    sale = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsAtStore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orderedItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qtyOrdered = table.Column<int>(type: "int", nullable: false),
                    pricePaid = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderedItems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    storeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalSales = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stroeLocationId = table.Column<int>(type: "int", nullable: true),
                    customerGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total = table.Column<double>(type: "float", nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_orders_stores_stroeLocationId",
                        column: x => x.stroeLocationId,
                        principalTable: "stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_stroeLocationId",
                table: "orders",
                column: "stroeLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "ItemsAtStore");

            migrationBuilder.DropTable(
                name: "orderedItems");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "stores");
        }
    }
}
