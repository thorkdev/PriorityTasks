using System;
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
        NotStarted,

        /// <summary>
        /// The task is being worked on.
        /// </summary>
        InProgress,

        /// <summary>
        /// The task is currently under review.
        /// </summary>
        InReview,

        /// <summary>
        /// The task has been completed.
        /// </summary>
        Completed,

        /// <summary>
        /// The task has been cancelled.
        /// </summary>
        Cancelled
    }

    /// <summary>
    /// Todo task.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Internal id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// Display name for the task.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Any additional notes or details for the task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// When the task is due.
        /// </summary>
        public DateTime Due { get; set; }

        /// <summary>
        /// Progress state of the task.
        /// </summary>
        public Status Status { get; set; }
    }
}
