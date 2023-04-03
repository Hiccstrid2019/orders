using Microsoft.EntityFrameworkCore.Migrations;

namespace OrdersAPI.Migrations
{
    public partial class UniqueNumberAndProviderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Order",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Number_ProviderId",
                table: "Order",
                columns: new[] { "Number", "ProviderId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Order_Number_ProviderId",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Order",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
