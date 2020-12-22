using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLDB.Migrations
{
    public partial class products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "customerSourceId",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EconomicCustomerId",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    productId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ninjaProductId = table.Column<int>(nullable: false),
                    econProductId = table.Column<string>(type: "varchar(150)", nullable: true),
                    sourceId = table.Column<int>(nullable: false),
                    source = table.Column<string>(type: "varchar(150)", nullable: true),
                    name = table.Column<string>(type: "varchar(150)", nullable: true),
                    description = table.Column<string>(type: "varchar(150)", nullable: true),
                    recommendedPrice = table.Column<float>(nullable: false),
                    salesPrice = table.Column<float>(nullable: false),
                    lastUpdated = table.Column<DateTime>(nullable: false),
                    dbUpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.productId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.AlterColumn<int>(
                name: "customerSourceId",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EconomicCustomerId",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
