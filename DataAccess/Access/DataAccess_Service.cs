using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Access
{
    public interface IDataAccess_Service
    {
        public Task<T> Create<T>(T newObject) where T : DataObject;
        public Task<T> Read<T>(int id) where T : DataObject;
        public Task<List<T>> ReadAll<T>() where T : DataObject;
        public Task<T> Update<T>(T newObject, int id) where T : DataObject;
        public Task<T> Delete<T>(int id) where T : DataObject;
        public Task<List<T>> Read<T>(IEnumerable<int> ids) where T : DataObject;
        public Task<List<ProjectTask>> GetAllTasksByProject(int id);
    }
    /// <summary>
    /// Data access layer class that is used to access data from the logic level
    /// </summary>
    public class DataAccess_Service : IDataAccess_Service
    {        
        /// <summary>
        /// Create new record in the database with specified type T
        /// </summary>
        public async Task<T> Create<T>(T newObject) where T : DataObject
        {
            using (var context = new DatabaseContext())
            {
                context.Add<T>(newObject);
                await context.SaveChangesAsync();
                return newObject;
            }
        }
        /// <summary>
        /// Read record from the database
        /// </summary>
        public async Task<T> Read<T>(int id) where T : DataObject
        {
            using (var context = new DatabaseContext())
            {
                return await context.FindAsync<T>(id);
            }
        }
        /// <summary>
        /// Read all records from the database with a specified type
        /// </summary>
        public async Task<List<T>> ReadAll<T>() where T : DataObject
        {
            using (var context = new DatabaseContext())
            {
                return await context.Set<T>().ToListAsync();
            }
        }
        /// <summary>
        /// Update record in the database
        /// </summary>
        public async Task<T> Update<T>(T newObject, int id) where T : DataObject
        {
            using (var context = new DatabaseContext())
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
        /// <summary>
        /// Delete record in the database
        /// </summary>
        public async Task<T> Delete<T>(int id) where T : DataObject
        {
            using (var context = new DatabaseContext())
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
        /// <summary>
        /// Read multiple record of the specified type from the database
        /// </summary>
        public async Task<List<T>> Read<T>(IEnumerable<int> ids) where T : DataObject
        {
            using (var context = new DatabaseContext())
            {
                var result = await context.Set<T>().Where(t => ids.Contains(t.Id)).ToListAsync();
                return result;
            }
        }
        /// <summary>
        /// Get all tasks from project by id
        /// </summary>
        public async Task<List<ProjectTask>> GetAllTasksByProject(int id)
        {
            using (var context = new DatabaseContext())
            {
                var entry = await context.Set<Project>().Include(t=>t.Tasks).Where(t => t.Id == id).FirstOrDefaultAsync();
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
