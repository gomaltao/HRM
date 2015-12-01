using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace HRM.Infrastructure.Migrations
{
    public partial class h3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Profession_Company_CompanyID", table: "Profession");
            migrationBuilder.DropForeignKey(name: "FK_ProfessionUser_Profession_ProfessionID", table: "ProfessionUser");
            migrationBuilder.DropForeignKey(name: "FK_WageSchemaDetail_WageSchema_WageSchemaID", table: "WageSchemaDetail");
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "WageSchema",
                type: "varchar(20)",
                nullable: true);
            migrationBuilder.AlterColumn<int>(
                name: "ProfessionID",
                table: "WageSchema",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Profession_Company_CompanyID",
                table: "Profession",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionUser_Profession_ProfessionID",
                table: "ProfessionUser",
                column: "ProfessionID",
                principalTable: "Profession",
                principalColumn: "ProfessionID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_WageSchemaDetail_WageSchema_WageSchemaID",
                table: "WageSchemaDetail",
                column: "WageSchemaID",
                principalTable: "WageSchema",
                principalColumn: "WageSchemaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Profession_Company_CompanyID", table: "Profession");
            migrationBuilder.DropForeignKey(name: "FK_ProfessionUser_Profession_ProfessionID", table: "ProfessionUser");
            migrationBuilder.DropForeignKey(name: "FK_WageSchemaDetail_WageSchema_WageSchemaID", table: "WageSchemaDetail");
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "WageSchema",
                type: "varchar(20)",
                nullable: true,
                defaultValue: "NotSet");
            migrationBuilder.AlterColumn<int>(
                name: "ProfessionID",
                table: "WageSchema",
                nullable: false,
                defaultValue: -1);
            migrationBuilder.AddForeignKey(
                name: "FK_Profession_Company_CompanyID",
                table: "Profession",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionUser_Profession_ProfessionID",
                table: "ProfessionUser",
                column: "ProfessionID",
                principalTable: "Profession",
                principalColumn: "ProfessionID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_WageSchemaDetail_WageSchema_WageSchemaID",
                table: "WageSchemaDetail",
                column: "WageSchemaID",
                principalTable: "WageSchema",
                principalColumn: "WageSchemaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
