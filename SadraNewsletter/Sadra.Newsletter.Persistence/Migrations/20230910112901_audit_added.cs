using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sadra.Newsletter.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class audit_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "NewsLetters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "NewsLetters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "NewsLetters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedAt",
                table: "NewsLetters",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "NewsLetters");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "NewsLetters");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "NewsLetters");

            migrationBuilder.DropColumn(
                name: "RemovedAt",
                table: "NewsLetters");
        }
    }
}
