using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLDB.Migrations.ProductDb
{
    public partial class ProductMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ninjaProductId = table.Column<int>(nullable: false),
                    econProductId = table.Column<string>(nullable: false),
                    name = table.Column<string>(type: "varchar(150)", nullable: true),
                    description = table.Column<string>(type: "varchar(150)", nullable: true),
                    recommendedPrice = table.Column<float>(nullable: false),
                    salesPrice = table.Column<float>(nullable: false),
                    lastUpdated = table.Column<DateTime>(nullable: false),
                    dbUpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
