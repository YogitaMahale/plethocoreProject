using Microsoft.EntityFrameworkCore.Migrations;

namespace plethocoreProject.persistence.Migrations
{
    public partial class AddMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NiNo",
                table: "PaymentRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NiNo",
                table: "PaymentRecords");
        }
    }
}
