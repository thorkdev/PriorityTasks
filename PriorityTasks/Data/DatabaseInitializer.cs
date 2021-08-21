using PriorityTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PriorityTasks.Data
{
    public static class DatabaseInitializer
    {
        /// <summary>
        /// Initialize the default database.
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(TaskContext context)
        {
            // check if database exists -> create if not
            context.Database.EnsureCreated();

            // check if database has been seeded
            if (context.Tasks.Any())
            {
                return;
            }

            // August 29th, 2021
            DateTime dueDate = new DateTime(2021, 8, 29);

            // generate default tasks
            Task[] tasks = new Task[]
            {
                new Task(
                    "Initialize Github Repo for Priority Tasks", 
                    "Create a repository for my ASP.Net Core task management web application for the Priority1 technical interview."),
                new Task(
                    "Initialize ASP.Net Core Web Application", 
                    "Initialize the project solution for Priority Tasks."),
                new Task(
                    "Create Task Model and Context",
                    "Create the model class, and database context for Entity Framework for tasks.")
            };

            // add each task to the context
            foreach (Task task in tasks)
            {
                // set due date for each task
                task.Due = dueDate;

                context.Tasks.Add(task);
            }

            context.SaveChanges();
        }
    }
}
