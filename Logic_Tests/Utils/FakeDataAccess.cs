using DataAccess.Access;
using DataAccess.Entities;
using Logic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic_Tests.Utils
{
    /// <summary>
    /// Copy of the DataAccess that using in-memory database instead of real when performing tests
    /// </summary>
    public class FakeDataAccess : IDataAccess_Service
    {
        /// <summary>
        /// In-memory database context used in tests
        /// </summary>
        public class FakeDatabaseContext : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (optionsBuilder.IsConfigured)
                {
                    base.OnConfiguring(optionsBuilder);
                    return;
                }
                
                optionsBuilder.UseInMemoryDatabase("Test");

                base.OnConfiguring(optionsBuilder);
            }

            public DbSet<Project> Projects { get; set; }

            public DbSet<ProjectTask> Tasks { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Project>()
                    .HasMany(proj => proj.Tasks)
                    .WithOne(task => task.Project)
                    .HasForeignKey(task => task.Project_ID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            }
        }
        public FakeDataAccess()
        {
            using (var context = new FakeDatabaseContext())
            {
                context.Database.EnsureDeleted();
            }
        }
        public async Task<T> Create<T>(T newObject) where T : DataObject
        {
            using (var context = new FakeDatabaseContext())
            {
                context.Add<T>(newObject);
                await context.SaveChangesAsync();
                return newObject;
            }
        }
        public async Task<T> Read<T>(int id) where T : DataObject
        {
            using (var context = new FakeDatabaseContext())
            {
                return await context.FindAsync<T>(id);
            }
        }
        public async Task<List<T>> ReadAll<T>() where T : DataObject
        {
            using (var context = new FakeDatabaseContext())
            {
                return await context.Set<T>().ToListAsync();
            }
        }
        public async Task<T> Update<T>(T newObject, int id) where T : DataObject
        {
            using (var context = new FakeDatabaseContext())
            {
                var entry = await context.FindAsync<T>(id);
                if (entry != null)
                {
                    context.Entry<T>(entry).CurrentValues.SetValues(newObject);
                    await context.SaveChangesAsync();
                }
                return entry;
            }
        }
        public async Task<T> Delete<T>(int id) where T : DataObject
        {
            using (var context = new FakeDatabaseContext())
            {
                var entry = await context.FindAsync<T>(id);
                if (entry != null)
                {
                    context.Remove<T>(entry);
                    await context.SaveChangesAsync();
                }
                return entry;
            }
        }
        public async Task<List<T>> Read<T>(IEnumerable<int> ids) where T : DataObject
        {
            using (var context = new FakeDatabaseContext())
            {
                var result = await context.Set<T>().Where(t => ids.Contains(t.Id)).ToListAsync();
                return result;
            }
        }
        public async Task<List<ProjectTask>> GetAllTasksByProject(int id)
        {
            using (var context = new FakeDatabaseContext())
            {
                var entry = await context.Set<Project>().Include(t => t.Tasks).Where(t => t.Id == id).FirstOrDefaultAsync();
                List<ProjectTask> tasks = null;
                if (entry != null)
                {
                    tasks = entry.Tasks.ToList();
                }
                return tasks;
            }
        }
    }
}
