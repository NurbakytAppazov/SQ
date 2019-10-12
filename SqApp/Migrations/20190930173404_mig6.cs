using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SqApp.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info1",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info2",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info3",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info4",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info5",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Info1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Info2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Info3",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Info4",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Info5",
                table: "Products");
        }
    }
}
