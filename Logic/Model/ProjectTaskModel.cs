using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.Model
{
    // Task class used between logic and representation layers. Also contains filed validations
    /// <summary>
    /// Task class
    /// </summary>
    public class ProjectTaskModel
    {    /// <summary>
         /// Task status
         /// </summary>
        public enum ProjectTaskStatuses
        {
            ToDo,
            InProgress,
            Done
        }
        /// <summary>
        /// Task Id
        /// </summary>
        [Required]
        public int id { get; set; }

        /// <summary>
        /// Id of project to which task is assigned. Currently task must be always assigned to a project. To change this behaviour remove required attributes in the ProjectTaskModel.cs and ProjectTask.cs
        /// </summary>
        [Required]
        public int project_id { get; set; }

        /// <summary>
        /// Task name
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string name { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 0)]
        public string description { get; set; }

        /// <summary>
        /// Task status
        /// </summary>
        [Required]
        [EnumDataType(typeof(ProjectTaskStatuses))]
        public ProjectTaskStatuses status { get; set; }

        /// <summary>
        /// Project priority. Negative values also acceptable
        /// </summary>
        [Required]
        public int priority { get; set; }
    }
}
