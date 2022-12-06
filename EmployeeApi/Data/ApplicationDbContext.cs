using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDepartment>().
                HasKey(x => new { x.DepartmentId, x.EmployeeId });
            modelBuilder.Entity<EmployeeDepartment>()
                 .HasKey(t => new { t.EmployeeId, t.DepartmentId });

            modelBuilder.Entity<EmployeeDepartment>()
                        .HasOne(t => t.Employee)
                        .WithMany(t => t.EmployeeDepartments)
                        .HasForeignKey(t => t.EmployeeId);

            modelBuilder.Entity<EmployeeDepartment>()
                .HasOne(t => t.Department)
                .WithMany(t => t.EmployeeDepartments)
                .HasForeignKey(t => t.DepartmentId);

        }
       
    }
}
