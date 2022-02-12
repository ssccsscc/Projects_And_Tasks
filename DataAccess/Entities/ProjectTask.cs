using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DataAccess.Entities
{
    public class ProjectTask : DataObject
    {
        public enum ProjectTaskStatuses
        {
            ToDo,
            InProgress,
            Done
        }

        [Required] //Not sure if the task may exist without project
        public int Project_ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public ProjectTaskStatuses Status { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public Project Project { get; set; }
    }
}
