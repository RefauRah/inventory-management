using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class DeleteColumnTransactionInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Inventory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Inventory",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Inventory",
                type: "integer",
                nullable: true);
        }
    }
}
