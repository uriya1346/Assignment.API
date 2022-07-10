using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Entities;
using Assignments.Data;

namespace AssignmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusOptionsController : ControllerBase
    {
        private readonly DataContext _context;

        public TaskStatusOptionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TaskStatusOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskStatusOption>>> GetTaskStatuses()
        {
          if (_context.TaskStatuses == null)
          {
              return NotFound();
          }
            return await _context.TaskStatuses.ToListAsync();
        }

        // GET: api/TaskStatusOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskStatusOption>> GetTaskStatusOption(int id)
        {
          if (_context.TaskStatuses == null)
          {
              return NotFound();
          }
            var taskStatusOption = await _context.TaskStatuses.FindAsync(id);

            if (taskStatusOption == null)
            {
                return NotFound();
            }

            return taskStatusOption;
        }

        // PUT: api/TaskStatusOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskStatusOption(int id, TaskStatusOption taskStatusOption)
        {
            if (id != taskStatusOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskStatusOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskStatusOptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskStatusOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskStatusOption>> PostTaskStatusOption(TaskStatusOption taskStatusOption)
        {
          if (_context.TaskStatuses == null)
          {
              return Problem("Entity set 'DataContext.TaskStatuses'  is null.");
          }
            _context.TaskStatuses.Add(taskStatusOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskStatusOption", new { id = taskStatusOption.Id }, taskStatusOption);
        }

        // DELETE: api/TaskStatusOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskStatusOption(int id)
        {
            if (_context.TaskStatuses == null)
            {
                return NotFound();
            }
            var taskStatusOption = await _context.TaskStatuses.FindAsync(id);
            if (taskStatusOption == null)
            {
                return NotFound();
            }

            _context.TaskStatuses.Remove(taskStatusOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskStatusOptionExists(int id)
        {
            return (_context.TaskStatuses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
