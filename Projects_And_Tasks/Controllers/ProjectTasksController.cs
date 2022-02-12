using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Model;
using Representation.Model;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Logic.Services;

namespace Projects_And_Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectTasksController : ControllerBase
    {
        private IProjectLogic_Service projectLogic;

        public ProjectTasksController(IProjectLogic_Service projectLogic)
        {
            this.projectLogic = projectLogic;
        }
        /// <summary>
        /// Gets all tasks by the project id.
        /// </summary>
        /// <returns>Array of the tasks</returns>
        /// <response code="200">Returns array of tasks</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Project not found</response>
        /// <response code="500">Service unavailable</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ProjectModel>), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 500)]
        public async Task<List<ProjectTaskModel>> GetTasks(int id)
        {
            return await projectLogic.GetTasksByProject(id);
        }
    }
}
