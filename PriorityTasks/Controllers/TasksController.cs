using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriorityTasks.Data;
using PriorityTasks.Models;

namespace PriorityTasks.Controllers
{
    public class TasksController : Controller
    {
        /// <summary>
        /// Database context for entity framework.
        /// </summary>
        private readonly TaskContext _context;

        /// <summary>
        /// Tasks constructor; dependency injection takes care of this.
        /// </summary>
        /// <param name="context"></param>
        public TasksController(TaskContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all of the tasks.
        /// </summary>
        /// <example>GET: Tasks</example>
        /// <returns>List view of tasks.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }

        /// <summary>
        /// Get the details of a task.
        /// </summary>
        /// <param name="id">Internal id of the task.</param>
        /// <example>GET: Tasks/Details/5</example>
        /// <returns>Details view for a specified task.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        /// <summary>
        /// Get the create form for a task.
        /// </summary>
        /// <example>Tasks/Create</example>
        /// <returns>Create view for a task.</returns>
        public IActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// Process creating a new task.
        /// </summary>
        /// <param name="task">New task to create.</param>
        /// <example>POST: Tasks/Create</example>
        /// <returns>Task list view on success.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Due,Status")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        /// <summary>
        /// Get the edit form for a task.
        /// </summary>
        /// <param name="id">Internal id of the task.</param>
        /// <example>GET: Tasks/Edit/5</example>
        /// <returns>Edit view for a specified task.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// Process updating a task.
        /// </summary>
        /// <param name="id">Internal id of the task.</param>
        /// <param name="task">Modified task to apply changes from.</param>
        /// <example>POST: Tasks/Edit/5</example>
        /// <returns>Task list view on success.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Due,Status")] Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // if task is set to completed
                    if (task.Status == Status.Completed)
                    {
                        // if task is previously incomplete
                        if (task.Completed == null)
                        {
                            task.Completed = DateTime.UtcNow;
                        }
                    }
                    else // reset on task status change
                    {
                        task.Completed = null;
                    }

                    _context.Update(task);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        /// <summary>
        /// Get the delete form for a task.
        /// </summary>
        /// <param name="id">Internal id of the task.</param>
        /// <example>GET: Tasks/Delete/5</example>
        /// <returns>Delete view for the specified task.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        /// <summary>
        /// Process deleting a task.
        /// </summary>
        /// <param name="id">Internal id of the task.</param>
        /// <example>POST: Tasks/Delete/5</example>
        /// <returns>Task list view on success./returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Check if a task exists.
        /// </summary>
        /// <param name="id">Internal id of the task.</param>
        /// <returns>True if a task with a matching id exists.</returns>
        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
