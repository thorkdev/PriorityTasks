using Microsoft.EntityFrameworkCore;
using PriorityTasks.Models;

namespace PriorityTasks.Data
{
    /// <summary>
    /// Coordinates entity framework functionality for tasks.
    /// </summary>
    public class TaskContext : DbContext
    {
        /// <summary>
        /// Tasks context constructor.
        /// </summary>
        /// <param name="options">Additional constructor options.</param>
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        /// <summary>
        /// Global set of tasks.
        /// </summary>
        public DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// Create tables for models.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().ToTable("Tasks");
        }
    }
}
