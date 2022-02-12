using System;
using AutoMapper;
using Logic.Model;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface ITaskLogic_Service
    {
        public Task<ProjectTaskModel> CreateTask(ProjectTaskModel newTask);
        public Task<ProjectTaskModel> GetTask(int id);
        public Task<List<ProjectTaskModel>> GetTasks();
        public Task<ProjectTaskModel> UpdateTask(ProjectTaskModel newTask);
        public Task<ProjectTaskModel> DeleteTask(int id);
    }
    /// <summary>
    /// This class defines the logic of the Task object and connecting data access layer and representation. Data access layer entities converted here using automapper
    /// </summary>
    public class TaskLogic_Service : ITaskLogic_Service
    {
        private DataAccess.Access.IDataAccess_Service dataAccess;
        private Mapper taskMapper;
        public TaskLogic_Service(DataAccess.Access.IDataAccess_Service dataAccess)
        {
            this.dataAccess = dataAccess;
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTask, ProjectTaskModel>().ReverseMap());
            taskMapper = new Mapper(mapperConfig);
        }

        public async Task<ProjectTaskModel> CreateTask(ProjectTaskModel newTask)
        {
            var task = taskMapper.Map<ProjectTaskModel, ProjectTask>(newTask);

            var project = await dataAccess.Read<Project>(newTask.project_id);
            if (project == null)
            {
                throw new LogicExceptionNotFound("Project with given id does not exist");
            }

            var result = await dataAccess.Create<ProjectTask>(task);

            return taskMapper.Map<ProjectTask, ProjectTaskModel>(result);
        }

        public async Task<ProjectTaskModel> GetTask(int id)
        {
            var result = await dataAccess.Read<ProjectTask>(id);
            if (result == null)
            {
                throw new LogicExceptionNotFound("Task with given id does not exist");
            }
            return taskMapper.Map<ProjectTask, ProjectTaskModel>(result);
        }

        public async Task<List<ProjectTaskModel>> GetTasks()
        {
            var result = await dataAccess.ReadAll<ProjectTask>();
            return taskMapper.Map<List<ProjectTask>, List<ProjectTaskModel>>(result);
        }

        public async Task<ProjectTaskModel> UpdateTask(ProjectTaskModel newTask)
        {
            var task = taskMapper.Map<ProjectTaskModel, ProjectTask>(newTask);
            var result = await dataAccess.Read<ProjectTask>(task.Id);
            if (result == null)
            {
                throw new LogicExceptionNotFound("Task with given id does not exist");
            }
            var project = await dataAccess.Read<Project>(task.Project_ID);
            if (project == null)
            {
                throw new LogicExceptionNotFound("Project with given id does not exist");
            }
            var updateResult = await dataAccess.Update<ProjectTask>(task, task.Id);
            return taskMapper.Map<ProjectTask, ProjectTaskModel>(updateResult);
        }

        public async Task<ProjectTaskModel> DeleteTask(int id)
        {
            var result = await dataAccess.Delete<ProjectTask>(id);
            if (result == null)
            {
                throw new LogicExceptionNotFound("Task with given id does not exist");
            }
            return taskMapper.Map<ProjectTask, ProjectTaskModel>(result);
        }
    }
}
