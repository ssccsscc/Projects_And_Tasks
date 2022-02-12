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
    public class TaskController : ControllerBase
    {
        private ITaskLogic_Service taskLogic;
        private Mapper taskPassMapper;

        public TaskController(ITaskLogic_Service taskLogic)
        {
            this.taskLogic = taskLogic;
            var projectUpdateMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProjectTaskModel, ProjectTaskModel_Create>().ReverseMap());
            taskPassMapper = new Mapper(projectUpdateMapperConfig);
        }
        /// <summary>
        /// Gets all tasks.
        /// </summary>
        /// <returns>Array of tasks</returns>
        /// <response code="200">Returns array of tasks</response>
        /// <response code="400">Validation failed</response>
        /// <response code="500">Service unavailable</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ProjectTaskModel>), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<List<ProjectTaskModel>> GetAll()
        {
            return await taskLogic.GetTasks();
        }
        /// <summary>
        /// Gets a task by id.
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns>Task</returns>
        /// <response code="200">Returns task</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Task not found</response>
        /// <response code="500">Service unavailable</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectTaskModel), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<ProjectTaskModel> Get(int id)
        {
            return await taskLogic.GetTask(id);
        }
        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Task
        ///     {
        ///        "project_id": 1,
        ///        "name": "My New Task",
        ///        "description": "Optional description",
        ///        "status": "ToDo",
        ///        "priority": 0
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created task</returns>
        /// <response code="200">Returns the newly created task</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Specified project doesnt exist</response>
        /// <response code="500">Service unavailable</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectTaskModel), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<ProjectTaskModel> CreateProject(ProjectTaskModel_Create model)
        {
            return await taskLogic.CreateTask(taskPassMapper.Map<ProjectTaskModel_Create, ProjectTaskModel>(model));
        }
        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /Task
        ///     {
        ///        "id": 1,
        ///        "project_id": 1,
        ///        "name": "My New Name For The Task With Id 1",
        ///        "description": "Optional description",
        ///        "status": "ToDo",
        ///        "priority": 0
        ///     }
        ///
        /// </remarks>
        /// <returns>Updated the task</returns>
        /// <response code="200">Returns the updated project</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Task or project not found</response>
        /// <response code="500">Service unavailable</response>
        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectTaskModel), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<ProjectTaskModel> UpdateTask(ProjectTaskModel model)
        {
            return await taskLogic.UpdateTask(model);
        }
        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns>Deleted project</returns>
        /// <response code="200">Returns deleted task</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Project not found</response>
        /// <response code="500">Service unavailable</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectTaskModel), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<ProjectTaskModel> DeleteProject(int id)
        {
            return await taskLogic.DeleteTask(id);
        }
    }
}
