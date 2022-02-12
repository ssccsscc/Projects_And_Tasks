using DataAccess.Context.Utils;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;

namespace DataAccess.Context
{
    /// <summary>
    /// Custom database context class
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// This method used to create databse. At this moment used in Program.cs
        /// </summary>
        public void _RunMigration()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                return;
            }

            optionsBuilder.UseSqlServer(DatabaseOptions.GetConnectionString());

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to many relation for ProjectTask and Project
            modelBuilder.Entity<Project>()
                .HasMany(proj => proj.Tasks)
                .WithOne(task => task.Project)
                .HasForeignKey(task => task.Project_ID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
