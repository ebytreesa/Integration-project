using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLDB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerSource = table.Column<string>(type: "varchar(150)", nullable: true),
                    customerSourceId = table.Column<int>(nullable: false),
                    EconomicCustomerId = table.Column<int>(nullable: false),
                    InvoiceNinjaCustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(type: "varchar(150)", nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    paymentTermsNumber = table.Column<int>(nullable: false),
                    paymentTermsName = table.Column<string>(type: "varchar(150)", nullable: true),
                    paymentTermsDaysOfCredit = table.Column<int>(nullable: false),
                    paymentTermsType = table.Column<string>(type: "varchar(150)", nullable: true),
                    address = table.Column<string>(type: "varchar(150)", nullable: true),
                    balance = table.Column<float>(nullable: false),
                    dueAmount = table.Column<float>(nullable: false),
                    city = table.Column<string>(type: "varchar(150)", nullable: true),
                    state = table.Column<string>(type: "varchar(150)", nullable: true),
                    country = table.Column<string>(type: "varchar(150)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(150)", nullable: true),
                    VatNumber = table.Column<int>(nullable: false),
                    vatZoneNumber = table.Column<int>(nullable: false),
                    VatZoneName = table.Column<string>(type: "varchar(150)", nullable: true),
                    CorporateIdNumber = table.Column<string>(type: "varchar(150)", nullable: true),
                    currency = table.Column<string>(type: "varchar(150)", nullable: true),
                    email = table.Column<string>(type: "varchar(150)", nullable: true),
                    CustomerPhoneNumber = table.Column<string>(type: "varchar(150)", nullable: true),
                    Website = table.Column<string>(type: "varchar(150)", nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    customerContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerContactNumber = table.Column<int>(nullable: false),
                    email = table.Column<string>(type: "varchar(150)", nullable: true),
                    name = table.Column<string>(type: "varchar(150)", nullable: true),
                    phone = table.Column<string>(type: "varchar(150)", nullable: true),
                    isPrimary = table.Column<bool>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false),
                    customerNumber = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.customerContactId);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddresses",
                columns: table => new
                {
                    deliveryLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deliveryLocationNumber = table.Column<int>(nullable: false),
                    address = table.Column<string>(type: "varchar(150)", nullable: true),
                    postalCode = table.Column<string>(type: "varchar(150)", nullable: true),
                    city = table.Column<string>(type: "varchar(150)", nullable: true),
                    state = table.Column<string>(type: "varchar(150)", nullable: true),
                    country = table.Column<string>(type: "varchar(150)", nullable: true),
                    termsOfDelivery = table.Column<string>(type: "varchar(150)", nullable: true),
                    self = table.Column<string>(type: "varchar(150)", nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: false),
                    customerNumber = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddresses", x => x.deliveryLocationId);
                    table.ForeignKey(
                        name: "FK_ShippingAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_CustomerId",
                table: "ShippingAddresses",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "ShippingAddresses");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
