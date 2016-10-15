using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2
{
    public class SampleContext : DbContext
    {
        public static string ConnectionString { get; set; }

        public SampleContext(DbContextOptions<SampleContext> options)
        : base(options)
        { }

        public DbSet<SampleTable> SampleTables { get; set; }
    }

    /// <summary>
    /// Factory class for EmployeesContext
    /// </summary>
    public static class EmployeesContextFactory
    {
        public static SampleContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SampleContext>();
            optionsBuilder.UseMySQL(SampleContext.ConnectionString);

            //Ensure database creation
            var context = new SampleContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }
    }

    /// <summary>
    /// A basic class for an Employee
    /// </summary>
    public class SampleTable
    {
        public SampleTable()
        {
        }

        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
    }
}
