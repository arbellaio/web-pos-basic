using Microsoft.EntityFrameworkCore.Migrations;

namespace RecompildPOS.Services.Migrations
{
    public partial class synclog_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AckCount",
                table: "UserSyncLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "UserSyncLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TerminalLogId",
                table: "UserSyncLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AckCount",
                table: "UserSyncLogs");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "UserSyncLogs");

            migrationBuilder.DropColumn(
                name: "TerminalLogId",
                table: "UserSyncLogs");
        }
    }
}
