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
    /// Class that providing test database structure for testing
    /// </summary>
    public static class TestDB
    {    /// <summary>
         /// Creates and returns new fake DataAccess with in-memory databse filled with test data
         /// </summary>
        public static FakeDataAccess GetTestDB()
        {
            FakeDataAccess dataAccess = new FakeDataAccess();
            dataAccess.Create<Project>(
                new Project() { CompletedDateTime = System.DateTime.Now, Name = "test1", Priority = 1, StartDateTime = System.DateTime.Now, Status = Project.ProjectStatuses.Active }
                ).Wait();
            dataAccess.Create<Project>(
                new Project() { CompletedDateTime = System.DateTime.Now, Name = "test2", Priority = 1, StartDateTime = System.DateTime.Now, Status = Project.ProjectStatuses.Active }
                ).Wait();
            dataAccess.Create<ProjectTask>(
                new ProjectTask() { Project_ID = 1, Name = "test1", Priority = 0, Description = "test1", Status = ProjectTask.ProjectTaskStatuses.Done }
                ).Wait();
            dataAccess.Create<ProjectTask>(
                new ProjectTask() { Project_ID = 1, Name = "test2", Priority = 0, Description = "test2", Status = ProjectTask.ProjectTaskStatuses.Done }
                ).Wait();
            return dataAccess;
        }
    }
}
