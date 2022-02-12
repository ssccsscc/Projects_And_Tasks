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
    public class Task_Tests
    {
        [TestMethod]
        public void GetTask_1_ReturnsProject()
        {
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(TestDB.GetTestDB());
            var result = TaskLogic_Service_Test.GetTask(1).Result;
            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void GetTasks_Returns_ValidCount()
        {
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(TestDB.GetTestDB());
            var result = TaskLogic_Service_Test.GetTasks().Result;
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void CreateTask_Works()
        {
            var testDB = TestDB.GetTestDB();
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(testDB);
            var testProj = new Logic.Model.ProjectTaskModel()
            {
                name = "abc",
                description = "test",
                priority = 1,
                project_id = 1,
                status = Logic.Model.ProjectTaskModel.ProjectTaskStatuses.Done
            };
            var result = TaskLogic_Service_Test.CreateTask(testProj).Result;
            var addedd = testDB.Read<ProjectTask>(result.id).Result;
            Assert.AreEqual(testProj.name, addedd.Name);
            Assert.AreEqual(testProj.description, addedd.Description);
            Assert.AreEqual(testProj.priority, addedd.Priority);
            Assert.AreEqual((int)testProj.status, (int)addedd.Status);
            Assert.AreEqual(testProj.project_id, addedd.Project_ID);
        }

        [TestMethod]
        public void UpdateTask_Works()
        {
            var testDB = TestDB.GetTestDB();
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(testDB);
            var testProj = new Logic.Model.ProjectTaskModel()
            {
                id = 1,
                name = "abc",
                description = "test",
                priority = 2,
                project_id = 2,
                status = Logic.Model.ProjectTaskModel.ProjectTaskStatuses.InProgress
            };
            var result = TaskLogic_Service_Test.UpdateTask(testProj).Result;
            var updated = testDB.Read<ProjectTask>(1).Result;
            Assert.AreEqual(testProj.name, updated.Name);
            Assert.AreEqual(testProj.description, updated.Description);
            Assert.AreEqual(testProj.priority, updated.Priority);
            Assert.AreEqual((int)testProj.status, (int)updated.Status);
            Assert.AreEqual(testProj.project_id, updated.Project_ID);
        }

        [TestMethod]
        public void DeleteTask_Works()
        {
            var testDB = TestDB.GetTestDB();
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(testDB);
            var result = TaskLogic_Service_Test.DeleteTask(1).Result;
            Assert.AreEqual(1, testDB.ReadAll<ProjectTask>().Result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Logic.LogicExceptionNotFound))]
        public void GetTask_WithInvalidID_ThrowsLogicError()
        {
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(TestDB.GetTestDB());
            Task.Run(async () =>
            {
                await TaskLogic_Service_Test.GetTask(3);
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        [ExpectedException(typeof(Logic.LogicExceptionNotFound))]
        public void DeleteTask_WithInvalidID_ThrowsLogicError()
        {
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(TestDB.GetTestDB());
            Task.Run(async () =>
            {
                await TaskLogic_Service_Test.DeleteTask(3);
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        [ExpectedException(typeof(Logic.LogicExceptionNotFound))]
        public void UpdateTask_WithInvalidID_ThrowsLogicError()
        {
            var testDB = TestDB.GetTestDB();
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(TestDB.GetTestDB());
            var testProj = new Logic.Model.ProjectTaskModel()
            {
                id = 3,
                name = "abc",
                description = "test",
                priority = 1,
                project_id = 1,
                status = Logic.Model.ProjectTaskModel.ProjectTaskStatuses.Done
            };
            Task.Run(async () =>
            {
                await TaskLogic_Service_Test.UpdateTask(testProj);
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        [ExpectedException(typeof(Logic.LogicExceptionNotFound))]
        public void UpdateTask_WithInvalidProjectID_ThrowsLogicError()
        {
            var testDB = TestDB.GetTestDB();
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(TestDB.GetTestDB());
            var testProj = new Logic.Model.ProjectTaskModel()
            {
                name = "abc",
                description = "test",
                priority = 1,
                project_id = 10,
                status = Logic.Model.ProjectTaskModel.ProjectTaskStatuses.Done
            };
            Task.Run(async () =>
            {
                await TaskLogic_Service_Test.UpdateTask(testProj);
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        [ExpectedException(typeof(Logic.LogicExceptionNotFound))]
        public void CreateTask_WithInvalidProjectID_ThrowsLogicError()
        {
            var testDB = TestDB.GetTestDB();
            TaskLogic_Service TaskLogic_Service_Test = new TaskLogic_Service(TestDB.GetTestDB());
            var testProj = new Logic.Model.ProjectTaskModel()
            {
                name = "abc",
                description = "test",
                priority = 1,
                project_id = 10,
                status = Logic.Model.ProjectTaskModel.ProjectTaskStatuses.Done
            };
            Task.Run(async () =>
            {
                await TaskLogic_Service_Test.CreateTask(testProj);
            }).GetAwaiter().GetResult();
        }
    }
}
