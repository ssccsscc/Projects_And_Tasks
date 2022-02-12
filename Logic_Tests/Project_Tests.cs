using DataAccess.Access;
using DataAccess.Entities;
using Logic.Services;
using Logic_Tests.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic_Tests
{
    [TestClass]
    public class Project_Tests
    {
        [TestMethod]
        public void GetProject_1_ReturnsProject()
        {
            ProjectLogic_Service ProjectLogic_Service_Test = new ProjectLogic_Service(TestDB.GetTestDB());
            var result = ProjectLogic_Service_Test.GetProject(1).Result;
            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void GetProjects_Returns_ValidCount()
        {
            ProjectLogic_Service ProjectLogic_Service_Test = new ProjectLogic_Service(TestDB.GetTestDB());
            var result = ProjectLogic_Service_Test.GetProjects().Result;
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void CreateProject_Works()
        {
            var testDB = TestDB.GetTestDB();
            ProjectLogic_Service ProjectLogic_Service_Test = new ProjectLogic_Service(testDB);
            var testProj = new Logic.Model.ProjectModel()
            {
                completedDateTime = System.DateTime.Now,
                name = "abc",
                priority = 1,
                status = Logic.Model.ProjectModel.ProjectStatuses.Active,
                startDateTime = System.DateTime.Now
            };
            var result = ProjectLogic_Service_Test.CreateProject(testProj).Result;
            var addedd = testDB.Read<Project>(result.id).Result;
            Assert.AreEqual(testProj.completedDateTime, addedd.CompletedDateTime);
            Assert.AreEqual(testProj.name, addedd.Name);
            Assert.AreEqual(testProj.priority, addedd.Priority);
            Assert.AreEqual((int)testProj.status, (int)addedd.Status);
            Assert.AreEqual(testProj.startDateTime, addedd.StartDateTime);
        }

        [TestMethod]
        public void UpdateProject_Works()
        {
            var testDB = TestDB.GetTestDB();
            ProjectLogic_Service ProjectLogic_Service_Test = new ProjectLogic_Service(testDB);
            var testProj = new Logic.Model.ProjectModel()
            {
                id = 1,
                completedDateTime = System.DateTime.Now,
                name = "abc",
                priority = 1,
                status = Logic.Model.ProjectModel.ProjectStatuses.Active,
                startDateTime = System.DateTime.Now
            };
            var result = ProjectLogic_Service_Test.UpdateProject(testProj).Result;
            var updated = testDB.Read<Project>(1).Result;
            Assert.AreEqual(testProj.completedDateTime, updated.CompletedDateTime);
            Assert.AreEqual(testProj.name, updated.Name);
            Assert.AreEqual(testProj.priority, updated.Priority);
            Assert.AreEqual((int)testProj.status, (int)updated.Status);
            Assert.AreEqual(testProj.startDateTime, updated.StartDateTime);
        }

        [TestMethod]
        public void DeleteProject_Works()
        {
            var testDB = TestDB.GetTestDB();
            ProjectLogic_Service ProjectLogic_Service_Test = new ProjectLogic_Service(testDB);
            var result = ProjectLogic_Service_Test.DeleteProject(1).Result;
            Assert.AreEqual(1, testDB.ReadAll<Project>().Result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Logic.LogicExceptionNotFound))]
        public void GetProject_WithInvalidID_ThrowsLogicError()
        {
            ProjectLogic_Service ProjectLogic_Service_Test = new ProjectLogic_Service(TestDB.GetTestDB());
            Task.Run(async () =>
            {
                await ProjectLogic_Service_Test.GetProject(3);
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        [ExpectedException(typeof(Logic.LogicExceptionNotFound))]
        public void DeleteProject_WithInvalidID_ThrowsLogicError()
        {
            ProjectLogic_Service ProjectLogic_Service_Test = new ProjectLogic_Service(TestDB.GetTestDB());
            Task.Run(async () =>
            {
                await ProjectLogic_Service_Test.DeleteProject(3);
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        [ExpectedException(typeof(Logic.LogicExceptionNotFound))]
        public void UpdateProject_WithInvalidID_ThrowsLogicError()
        {
            var testDB = TestDB.GetTestDB();
            ProjectLogic_Service ProjectLogic_Service_Test = new ProjectLogic_Service(testDB);
            var testProj = new Logic.Model.ProjectModel()
            {
                id = 3,
                completedDateTime = System.DateTime.Now,
                name = "abc",
                priority = 1,
                status = Logic.Model.ProjectModel.ProjectStatuses.Active,
                startDateTime = System.DateTime.Now
            };
            Task.Run(async () =>
            {
                await ProjectLogic_Service_Test.UpdateProject(testProj);
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetTasksByProject_Works()
        {
            var testDB = TestDB.GetTestDB();
            ProjectLogic_Service ProjectLogic_Service_Test = new ProjectLogic_Service(testDB);
            var result = ProjectLogic_Service_Test.GetTasksByProject(1).Result;
            Assert.AreEqual(2, result.Count);
            result = ProjectLogic_Service_Test.GetTasksByProject(2).Result;
            Assert.AreEqual(0, result.Count);
        }
    }
}
