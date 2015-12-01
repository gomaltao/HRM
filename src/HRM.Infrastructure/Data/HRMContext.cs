using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using HRM.Domain.Model;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Json;

using Microsoft.Dnx.Runtime;

    namespace HRM.Infrastructure.Data
{
    public class HRMContext : DbContext
    {

        public HRMContext( )
            //: base( )
        {
            //Database.EnsureCreated();
            //Database.Migrate(); 
    }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionbsBuilder)
        {
            OptionbsBuilder.UseSqlServer (@"Server=.\SQLEXPRESS;Database=HRM2;integrated security=True;");
        }

        public DbSet<User> User { get; set; }
        public DbSet<TimeCard>TimeCard {get; set;}
        public DbSet<Company> Company { get; set; }
        public DbSet<WageSchema>  WageSchema { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Profession> Profession { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<User>().HasKey(k => k.UserID);
            modelBuilder.Entity<User>().Property(p => p.UserID).HasColumnType("varchar(20)");
            modelBuilder.Entity<User>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.AdminEmail).IsRequired();

            //modelBuilder.Entity<TimeCard>().HasKey(k => k.TimeCardID);
            modelBuilder.Entity<TimeCard>().Property(p => p.TimeCardID).ValueGeneratedOnAdd();
            modelBuilder.Entity<TimeCard>().Property(p => p.UserID).HasColumnType("varchar(20)");
            //modelBuilder.Entity<TimeCard>().Property(p => p.UserID).IsRequired();
            //modelBuilder.Entity<TimeCard>().HasOne<User>().WithMany(c => c.TimeCards).HasForeignKey(k => k.UserID);
            //modelBuilder.Entity<TimeCard>().HasOne<User>().WithMany(c => c.TimeCards).IsRequired();

            //modelBuilder.Entity<Company>().HasKey(k => k.CompanyID);
            modelBuilder.Entity<Company>().Property(p => p.CompanyID).HasColumnType("varchar(40)");
            modelBuilder.Entity<Company>().Property(p => p.CompanyName).IsRequired();
            modelBuilder.Entity<Company>().Property(p => p.CustomerAdminMail).IsRequired();

            //modelBuilder.Entity<WageSchema>().HasKey(k => k.WageSchemaID);
            modelBuilder.Entity<WageSchema>().Property(p => p.WageSchemaID).ValueGeneratedOnAdd();
            modelBuilder.Entity<WageSchema>().Property(p => p.UserID).HasColumnType("varchar(20)");
            modelBuilder.Entity<WageSchema>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<WageSchema>().Property(p => p.HourlyWage).IsRequired();
            //modelBuilder.Entity<WageSchema>().HasOne(p => p.Profession).WithMany(w => w.WageSchemas).HasForeignKey (f => f.ProfessionID);
            //modelBuilder.Entity<WageSchema>().HasOne(p => p.Profession).WithMany(w => w.WageSchemas);
            //modelBuilder.Entity<WageSchema>().Property(p => p.ProfessionID).IsRequired();
            //modelBuilder.Entity<WageSchema>().HasOne(o => o.employee).WithMany(w => w.WageSchemas);



            //modelBuilder.Entity<WageSchemaDetail>().HasKey(k => k.WageSchemaDetailID);
            modelBuilder.Entity<WageSchemaDetail>().Property(p => p.WageSchemaDetailID).ValueGeneratedOnAdd();
            //modelBuilder.Entity<WageSchemaDetail>().HasOne(p => p.wageSchema).WithMany(w => w.WageSchemaDetails).HasForeignKey(f => f.WageSchemaID);
            //modelBuilder.Entity<WageSchemaDetail>().HasOne(p => p.wageSchema).WithMany(w => w.WageSchemaDetails).IsRequired();
            //modelBuilder.Entity<WageSchemaDetail>().Property(p => p.WageSchemaID ).IsRequired();


            //modelBuilder.Entity<Department>().HasKey(k => k.DepartmentID);
            modelBuilder.Entity<Department>().Property(p => p.DepartmentID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Department>().Property(p => p.CompanyID).HasColumnType("varchar(40)");
            //modelBuilder.Entity<Department>().HasOne<Company>().WithMany(c => c.Departments).HasForeignKey(k => k.CompanyID).IsRequired();
            //modelBuilder.Entity<Department>().HasOne<Company>().WithMany(c => c.Departments);
            modelBuilder.Entity<Department>().Property(p => p.Title).IsRequired();


            modelBuilder.Entity<Profession>().Property(p => p.CompanyID).HasColumnType("varchar(40)");
            modelBuilder.Entity<Profession>().HasKey(k => k.ProfessionID);
            modelBuilder.Entity<Profession>().Property(p => p.ProfessionID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Profession>().Property(p => p.CompanyID).IsRequired();
            modelBuilder.Entity<Profession>().HasOne(p => p.company).WithMany(w => w.Professions).HasForeignKey(f => f.CompanyID );
            modelBuilder.Entity<Profession>().Property(p => p.Title).IsRequired();

            modelBuilder.Entity<ProfessionUser>().Property(p => p.UserID).HasColumnType("varchar(20)");
            modelBuilder.Entity<ProfessionUser>().HasKey(k => new { k.ProfessionID, k.UserID });

            modelBuilder.Entity<CompanyUser>().Property(p => p.CompanyID).HasColumnType("varchar(40)" );
            modelBuilder.Entity<CompanyUser>().Property(p => p.UserID ).HasColumnType("varchar(20)");
            modelBuilder.Entity<CompanyUser>().HasKey(k => new { k.CompanyID, k.UserID }); 

        }
    }
}
