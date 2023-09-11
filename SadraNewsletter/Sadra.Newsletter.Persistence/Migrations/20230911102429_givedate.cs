using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sadra.Newsletter.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class givedate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_NewsLetters_NewsLetterId",
                table: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_NewsLetterId",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "SentDate",
                table: "NewsLetters");

            migrationBuilder.AlterColumn<int>(
                name: "NewsLetterId",
                table: "Recipients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastViewed",
                table: "Recipients",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "GiveDate",
                table: "Recipients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecivedDate",
                table: "Recipients",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiveDate",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "RecivedDate",
                table: "Recipients");

            migrationBuilder.AlterColumn<int>(
                name: "NewsLetterId",
                table: "Recipients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastViewed",
                table: "Recipients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Recipients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SentDate",
                table: "NewsLetters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_NewsLetterId",
                table: "Recipients",
                column: "NewsLetterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_NewsLetters_NewsLetterId",
                table: "Recipients",
                column: "NewsLetterId",
                principalTable: "NewsLetters",
                principalColumn: "Id");
        }
    }
}
