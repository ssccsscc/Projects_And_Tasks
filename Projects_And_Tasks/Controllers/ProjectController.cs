using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Model;
using Representation.Model;
using AutoMapper;
using Logic.Services;

namespace Projects_And_Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private IProjectLogic_Service projectLogic;
        private Mapper projectPassMapper;

        public ProjectController(IProjectLogic_Service projectLogic)
        {
            this.projectLogic = projectLogic;
            var projectUpdateMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProjectModel, ProjectModel_Create>().ReverseMap());
            projectPassMapper = new Mapper(projectUpdateMapperConfig);
        }
        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns>Array of projects</returns>
        /// <response code="200">Returns array of project</response>
        /// <response code="400">Validation failed</response>
        /// <response code="500">Service unavailable</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ProjectModel>), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<List<ProjectModel>> GetAll()
        {
            return await projectLogic.GetProjects();
        }
        /// <summary>
        /// Gets a project by id.
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Project</returns>
        /// <response code="200">Returns project</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Project not found</response>
        /// <response code="500">Service unavailable</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectModel), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<ProjectModel> Get(int id)
        {
            return await projectLogic.GetProject(id);
        }
        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Project
        ///     {
        ///        "name": "My New Project",
        ///        "startDateTime": "2022-02-11T17:45:56.633Z",
        ///        "status": "NotStarted",
        ///        "priority": 0
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created project</returns>
        /// <response code="200">Returns the newly created project</response>
        /// <response code="400">Validation failed</response>
        /// <response code="500">Service unavailable</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectModel), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<ProjectModel> CreateProject(ProjectModel_Create model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            return await projectLogic.CreateProject(projectPassMapper.Map<ProjectModel_Create, ProjectModel>(model));
        }
        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /Project
        ///     {
        ///        "id": 1,
        ///        "name": "New Project Name For The Project With Id 1",
        ///        "startDateTime": "2022-02-11T17:45:56.633Z",
        ///        "status": "NotStarted",
        ///        "priority": 0
        ///     }
        ///
        /// </remarks>
        /// <returns>Updated the project</returns>
        /// <response code="200">Returns the updated project</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Project not found</response>
        /// <response code="500">Service unavailable</response>
        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectModel), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<ProjectModel> UpdateProject(ProjectModel model)
        {
            return await projectLogic.UpdateProject(model);
        }
        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Deleted project</returns>
        /// <response code="200">Returns the deleted project</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Project not found</response>
        /// <response code="500">Service unavailable</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectModel), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<ProjectModel> DeleteProject(int id)
        {
            return await projectLogic.DeleteProject(id);
        }
    }
}
