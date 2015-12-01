using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace HRM.Infrastructure.Migrations
{
    public partial class h1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "varchar(40)", nullable: false),
                    Address = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: false),
                    CustomerAdminMail = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                });
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "varchar(20)", nullable: false),
                    AdminEmail = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserCode = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CompanyID = table.Column<string>(type: "varchar(40)", nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_Department_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Profession",
                columns: table => new
                {
                    ProfessionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyID = table.Column<string>(type: "varchar(40)", nullable: false),
                    CustomerAdminMail = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    WageSchemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profession", x => x.ProfessionID);
                    table.ForeignKey(
                        name: "FK_Profession_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "CompanyUser",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "varchar(40)", nullable: false),
                    UserID = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUser", x => new { x.CompanyID, x.UserID });
                    table.ForeignKey(
                        name: "FK_CompanyUser_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyUser_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "TimeCard",
                columns: table => new
                {
                    TimeCardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(type: "varchar(20)", nullable: true),
                    timeCardEnum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCard", x => x.TimeCardID);
                    table.ForeignKey(
                        name: "FK_TimeCard_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "ProfessionUser",
                columns: table => new
                {
                    ProfessionID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionUser", x => new { x.ProfessionID, x.UserID });
                    table.ForeignKey(
                        name: "FK_ProfessionUser_Profession_ProfessionID",
                        column: x => x.ProfessionID,
                        principalTable: "Profession",
                        principalColumn: "ProfessionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessionUser_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "WageSchema",
                columns: table => new
                {
                    WageSchemaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerAdminMail = table.Column<string>(nullable: true),
                    HourlyWage = table.Column<double>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    ProfessionID = table.Column<int>(nullable: false, defaultValue: -1),
                    Title = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(type: "varchar(20)", nullable: true, defaultValue: "NotSet")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WageSchema", x => x.WageSchemaID);
                    table.ForeignKey(
                        name: "FK_WageSchema_Profession_ProfessionID",
                        column: x => x.ProfessionID,
                        principalTable: "Profession",
                        principalColumn: "ProfessionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WageSchema_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "WageSchemaDetail",
                columns: table => new
                {
                    WageSchemaDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<int>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    WageSchemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WageSchemaDetail", x => x.WageSchemaDetailID);
                    table.ForeignKey(
                        name: "FK_WageSchemaDetail_WageSchema_WageSchemaID",
                        column: x => x.WageSchemaID,
                        principalTable: "WageSchema",
                        principalColumn: "WageSchemaID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("CompanyUser");
            migrationBuilder.DropTable("Department");
            migrationBuilder.DropTable("ProfessionUser");
            migrationBuilder.DropTable("TimeCard");
            migrationBuilder.DropTable("WageSchemaDetail");
            migrationBuilder.DropTable("WageSchema");
            migrationBuilder.DropTable("Profession");
            migrationBuilder.DropTable("User");
            migrationBuilder.DropTable("Company");
        }
    }
}
