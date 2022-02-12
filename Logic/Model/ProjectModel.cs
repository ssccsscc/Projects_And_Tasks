using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Logic.Model
{
    //Fluentvalidation package may be added to make better validation of fields
    //Project class used between logic and representation layers.Also contains filed validations
    /// <summary>
    /// Project class
    /// </summary>
    public class ProjectModel : IValidatableObject
    {    
        public enum ProjectStatuses
        {
            NotStarted,
            Active,
            Completed
        }

        [Required]
        public int id { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string name { get; set; }

        /// <summary>
        /// Srart time of the project
        /// </summary>
        [Required]
        public DateTime startDateTime { get; set; }

        /// <summary>
        /// Completion time of the project (should be greater than startDateTime or be null)
        /// </summary>
        public DateTime? completedDateTime { get; set; }

        /// <summary>
        /// Completion status
        /// </summary>
        [Required]
        [EnumDataType(typeof(ProjectStatuses))]
        public ProjectStatuses status { get; set; }

        /// <summary>
        /// Project priority. Negative values also acceptable
        /// </summary>
        [Required]
        public int priority { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (completedDateTime != null && startDateTime > completedDateTime)
            {
                yield return new ValidationResult("completedDateTime must be greater than startDateTime", new[] { "completedDateTime" });
            }
            //if (completedDateTime != null && status != ProjectStatuses.Completed)
            //{
            //    yield return new ValidationResult("completedDateTime is set but status is not Completed", new[] { "status" });
            //}
        }
    }
}
