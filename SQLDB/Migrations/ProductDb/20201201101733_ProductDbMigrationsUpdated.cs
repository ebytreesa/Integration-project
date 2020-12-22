using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLDB.Migrations.ProductDb
{
    public partial class ProductDbMigrationsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "source",
                table: "Products",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sourceId",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "source",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "sourceId",
                table: "Products");
        }
    }
}
