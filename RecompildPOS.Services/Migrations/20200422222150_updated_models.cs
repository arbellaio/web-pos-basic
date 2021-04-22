using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecompildPOS.Services.Migrations
{
    public partial class updated_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserSyncLogs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserSyncLogs");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "UserSyncLogs");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "UserSyncLogs");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EndOfDayReports",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "EndOfDayReports",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "EndOfDayReports",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "EndOfDayReports",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BusinessFinances",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BusinessFinances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "BusinessFinances",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "BusinessFinances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BusinessExpenses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BusinessExpenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "BusinessExpenses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "BusinessExpenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EndOfDayReports");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "EndOfDayReports");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "EndOfDayReports");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "EndOfDayReports");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BusinessFinances");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BusinessFinances");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "BusinessFinances");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "BusinessFinances");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BusinessExpenses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BusinessExpenses");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "BusinessExpenses");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "BusinessExpenses");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserSyncLogs",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserSyncLogs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "UserSyncLogs",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "UserSyncLogs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
