using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SqApp.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Pod_Categories_Pod_CategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Pod_CategoryId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Pod_Categories_Pod_CategoryId",
                table: "Products",
                column: "Pod_CategoryId",
                principalTable: "Pod_Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Pod_Categories_Pod_CategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Pod_CategoryId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Pod_Categories_Pod_CategoryId",
                table: "Products",
                column: "Pod_CategoryId",
                principalTable: "Pod_Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
