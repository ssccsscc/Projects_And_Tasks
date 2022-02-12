using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DataAccess.Entities
{
    public class Project : DataObject
    {
        public enum ProjectStatuses
        {
            NotStarted,
            Active,
            Completed
        }

        public Project()
        {
            Tasks = new List<ProjectTask>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public DateTime? CompletedDateTime { get; set; }

        [Required]
        public ProjectStatuses Status { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public List<ProjectTask> Tasks { get; set; }
    }
}
