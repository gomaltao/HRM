using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using HRM.Infrastructure.Data;

namespace HRM.Infrastructure.Migrations
{
    [DbContext(typeof(HRMContext))]
    partial class HRMContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HRM.Domain.Model.Company", b =>
                {
                    b.Property<string>("CompanyID")
                        .HasAnnotation("Relational:ColumnType", "varchar(40)");

                    b.Property<string>("Address");

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<string>("CustomerAdminMail")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("CompanyID");
                });

            modelBuilder.Entity("HRM.Domain.Model.CompanyUser", b =>
                {
                    b.Property<string>("CompanyID")
                        .HasAnnotation("Relational:ColumnType", "varchar(40)");

                    b.Property<string>("UserID")
                        .HasAnnotation("Relational:ColumnType", "varchar(20)");

                    b.HasKey("CompanyID", "UserID");
                });

            modelBuilder.Entity("HRM.Domain.Model.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("CompanyID")
                        .HasAnnotation("Relational:ColumnType", "varchar(40)");

                    b.Property<string>("Description");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("DepartmentID");
                });

            modelBuilder.Entity("HRM.Domain.Model.Profession", b =>
                {
                    b.Property<int>("ProfessionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyID")
                        .IsRequired()
                        .HasAnnotation("Relational:ColumnType", "varchar(40)");

                    b.Property<string>("CustomerAdminMail");

                    b.Property<string>("Description");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("WageSchemaID");

                    b.HasKey("ProfessionID");
                });

            modelBuilder.Entity("HRM.Domain.Model.ProfessionUser", b =>
                {
                    b.Property<int>("ProfessionID");

                    b.Property<string>("UserID")
                        .HasAnnotation("Relational:ColumnType", "varchar(20)");

                    b.HasKey("ProfessionID", "UserID");
                });

            modelBuilder.Entity("HRM.Domain.Model.TimeCard", b =>
                {
                    b.Property<int>("TimeCardID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("UserID")
                        .HasAnnotation("Relational:ColumnType", "varchar(20)");

                    b.Property<int>("timeCardEnum");

                    b.HasKey("TimeCardID");
                });

            modelBuilder.Entity("HRM.Domain.Model.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasAnnotation("Relational:ColumnType", "varchar(20)");

                    b.Property<string>("AdminEmail")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("UserCode");

                    b.Property<string>("UserName");

                    b.HasKey("UserID");
                });

            modelBuilder.Entity("HRM.Domain.Model.WageSchema", b =>
                {
                    b.Property<int>("WageSchemaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerAdminMail");

                    b.Property<double>("HourlyWage");

                    b.Property<int>("Level");

                    b.Property<int?>("ProfessionID");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("UserID")
                        .HasAnnotation("Relational:ColumnType", "varchar(20)");

                    b.HasKey("WageSchemaID");
                });

            modelBuilder.Entity("HRM.Domain.Model.WageSchemaDetail", b =>
                {
                    b.Property<int>("WageSchemaDetailID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Day");

                    b.Property<TimeSpan>("EndTime");

                    b.Property<TimeSpan>("StartTime");

                    b.Property<int>("WageSchemaID");

                    b.HasKey("WageSchemaDetailID");
                });

            modelBuilder.Entity("HRM.Domain.Model.CompanyUser", b =>
                {
                    b.HasOne("HRM.Domain.Model.Company")
                        .WithMany()
                        .HasForeignKey("CompanyID");

                    b.HasOne("HRM.Domain.Model.User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("HRM.Domain.Model.Department", b =>
                {
                    b.HasOne("HRM.Domain.Model.Company")
                        .WithMany()
                        .HasForeignKey("CompanyID");
                });

            modelBuilder.Entity("HRM.Domain.Model.Profession", b =>
                {
                    b.HasOne("HRM.Domain.Model.Company")
                        .WithMany()
                        .HasForeignKey("CompanyID");
                });

            modelBuilder.Entity("HRM.Domain.Model.ProfessionUser", b =>
                {
                    b.HasOne("HRM.Domain.Model.Profession")
                        .WithMany()
                        .HasForeignKey("ProfessionID");

                    b.HasOne("HRM.Domain.Model.User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("HRM.Domain.Model.TimeCard", b =>
                {
                    b.HasOne("HRM.Domain.Model.User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("HRM.Domain.Model.WageSchema", b =>
                {
                    b.HasOne("HRM.Domain.Model.Profession")
                        .WithMany()
                        .HasForeignKey("ProfessionID");

                    b.HasOne("HRM.Domain.Model.User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("HRM.Domain.Model.WageSchemaDetail", b =>
                {
                    b.HasOne("HRM.Domain.Model.WageSchema")
                        .WithMany()
                        .HasForeignKey("WageSchemaID");
                });
        }
    }
}
