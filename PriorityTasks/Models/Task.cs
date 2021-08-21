using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriorityTasks.Models
{
    /// <summary>
    /// Types of task status' that mark their progress.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The task has not been started yet.
        /// </summary>
        [Display(Name = "Not Started")]
        NotStarted,

        /// <summary>
        /// The task is being worked on.
        /// </summary>
        [Display(Name = "In Progress")]
        InProgress,

        /// <summary>
        /// The task is currently under review.
        /// </summary>
        [Display(Name = "In Review")]
        InReview,

        /// <summary>
        /// The task has been cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The task has been completed.
        /// </summary>
        Completed
    }

    /// <summary>
    /// Todo task.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Internal id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Display name for the task.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Any additional notes or details for the task.
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// When the task is due.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Due { get; set; }

        /// <summary>
        /// When the task was completed.
        /// </summary>
        [Display(Name = "Completed On")]
        [DataType(DataType.DateTime)]
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Progress state of the task.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Initialize a default task.
        /// </summary>
        public Task()
        {
        }

        /// <summary>
        /// Initialize a task.
        /// </summary>
        /// <param name="name">Name of the task.</param>
        /// <param name="description">Description for the task.</param>
        /// <param name="due">When this task is due.</param>
        public Task(string name, string description = "", DateTime? due = null)
        {
            Name = name;
            Description = description;
            Due = due == null ? DateTime.UtcNow.AddDays(1) : due;
        }
    }
}
