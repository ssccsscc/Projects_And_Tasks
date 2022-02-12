using System;
using AutoMapper;
using Logic.Model;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IProjectLogic_Service
    {
        public Task<ProjectModel> CreateProject(ProjectModel newProject);
        public Task<ProjectModel> GetProject(int id);
        public Task<List<ProjectModel>> GetProjects();
        public Task<ProjectModel> UpdateProject(ProjectModel newProject);
        public Task<ProjectModel> DeleteProject(int id);
        public Task<List<ProjectTaskModel>> GetTasksByProject(int id);
    }
    /// <summary>
    /// This class defines the logic of the Project object and connecting data access level and representation level. Data access layer entities converted here using automapper
    /// </summary>
    public class ProjectLogic_Service : IProjectLogic_Service
    {
        private DataAccess.Access.IDataAccess_Service dataAccess;
        private Mapper projectMapper;
        private Mapper taskMapper;
        public ProjectLogic_Service(DataAccess.Access.IDataAccess_Service dataAccess)
        {
            this.dataAccess = dataAccess;
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectModel>().ReverseMap());
            projectMapper = new Mapper(mapperConfig);
            mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTask, ProjectTaskModel>().ReverseMap());
            taskMapper = new Mapper(mapperConfig);
        }

        public async Task<ProjectModel> CreateProject(ProjectModel newProject)
        {
            var project = projectMapper.Map<ProjectModel, Project>(newProject);
            var result = await dataAccess.Create<Project>(project);
            return projectMapper.Map<Project, ProjectModel>(result);
        }

        public async Task<ProjectModel> GetProject(int id)
        {
            var result = await dataAccess.Read<Project>(id);
            if (result == null)
            {
                throw new LogicExceptionNotFound("Project with given id does not exist");
            }
            Console.WriteLine(result.Name);
            return projectMapper.Map<Project, ProjectModel>(result);
        }

        public async Task<List<ProjectModel>> GetProjects()
        {
            var result = await dataAccess.ReadAll<Project>();
            return projectMapper.Map<List<Project>, List<ProjectModel>>(result);
        }

        public async Task<ProjectModel> UpdateProject(ProjectModel newProject)
        {
            var project = projectMapper.Map<ProjectModel, Project>(newProject);
            var result = await dataAccess.Read<Project>(project.Id);
            if (result == null)
            {
                throw new LogicExceptionNotFound("Project with given id does not exist");
            }
            var updateResult = await dataAccess.Update<Project>(project, project.Id);
            return projectMapper.Map<Project, ProjectModel>(updateResult);
        }

        public async Task<ProjectModel> DeleteProject(int id)
        {
            var result = await dataAccess.Delete<Project>(id);
            if (result == null)
            {
                throw new LogicExceptionNotFound("Project with given id does not exist");
            }
            return projectMapper.Map<Project, ProjectModel>(result);
        }

        public async Task<List<ProjectTaskModel>> GetTasksByProject(int id)
        {
            var list = await dataAccess.GetAllTasksByProject(id);
            if (list == null)
            {
                throw new LogicExceptionNotFound("Project with given id does not exist");
            }
            return taskMapper.Map<List<ProjectTask>, List<ProjectTaskModel>>(list);
        }
    }
}
